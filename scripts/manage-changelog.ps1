#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Manages the CHANGELOG.md file for the Alisto Backend project.

.DESCRIPTION
    This script helps developers add entries to the changelog and prepare releases.

.PARAMETER Action
    The action to perform: "add", "release", "list"

.PARAMETER Type
    The type of change: "added", "changed", "fixed", "security", "deprecated", "removed"

.PARAMETER Message
    The change message to add.

.PARAMETER Version
    The version number for the release (e.g., "1.3.0").

.EXAMPLE
    .\manage-changelog.ps1 -Action "add" -Type "added" -Message "New user profile endpoint"

.EXAMPLE
    .\manage-changelog.ps1 -Action "release" -Version "1.3.0"

.EXAMPLE
    .\manage-changelog.ps1 -Action "list"
#>

param(
    [Parameter(Mandatory = $true)]
    [ValidateSet("add", "release", "list")]
    [string]$Action,
    
    [Parameter(Mandatory = $false)]
    [ValidateSet("added", "changed", "fixed", "security", "deprecated", "removed")]
    [string]$Type,
    
    [Parameter(Mandatory = $false)]
    [string]$Message,
    
    [Parameter(Mandatory = $false)]
    [string]$Version
)

$ChangelogPath = "CHANGELOG.md"

function Add-ChangelogEntry {
    param([string]$Type, [string]$Message)
    
    if (-not (Test-Path $ChangelogPath)) {
        Write-Host "❌ CHANGELOG.md not found!" -ForegroundColor Red
        exit 1
    }
    
    $content = Get-Content $ChangelogPath -Raw
    
    # Find the Unreleased section
    $unreleasedPattern = '## \[Unreleased\]\s*\n\s*### Added\s*\n'
    if ($content -match $unreleasedPattern) {
        # Add entry to the appropriate section
        $entry = "  - $Message`n"
        
        if ($Type -eq "added") {
            $content = $content -replace '### Added\s*\n', "### Added`n$entry"
        } elseif ($Type -eq "changed") {
            # Check if Changed section exists
            if ($content -match '### Changed\s*\n') {
                $content = $content -replace '### Changed\s*\n', "### Changed`n$entry"
            } else {
                # Add Changed section after Added
                $content = $content -replace '### Added\s*\n', "### Added`n$entry### Changed`n"
            }
        } elseif ($Type -eq "fixed") {
            # Check if Fixed section exists
            if ($content -match '### Fixed\s*\n') {
                $content = $content -replace '### Fixed\s*\n', "### Fixed`n$entry"
            } else {
                # Add Fixed section after Added
                $content = $content -replace '### Added\s*\n', "### Added`n$entry### Fixed`n"
            }
        } elseif ($Type -eq "security") {
            # Check if Security section exists
            if ($content -match '### Security\s*\n') {
                $content = $content -replace '### Security\s*\n', "### Security`n$entry"
            } else {
                # Add Security section after Added
                $content = $content -replace '### Added\s*\n', "### Added`n$entry### Security`n"
            }
        }
        
        Set-Content $ChangelogPath $content -NoNewline
        Write-Host "✅ Added '$Type' entry: $Message" -ForegroundColor Green
    } else {
        Write-Host "❌ Could not find [Unreleased] section in CHANGELOG.md" -ForegroundColor Red
        exit 1
    }
}

function New-Release {
    param([string]$Version)
    
    if (-not (Test-Path $ChangelogPath)) {
        Write-Host "❌ CHANGELOG.md not found!" -ForegroundColor Red
        exit 1
    }
    
    $content = Get-Content $ChangelogPath -Raw
    $date = Get-Date -Format "yyyy-MM-dd"
    
    # Check if there are any entries in Unreleased
    $unreleasedPattern = '## \[Unreleased\]\s*\n\s*### Added\s*\n\s*-\s*'
    if ($content -notmatch $unreleasedPattern) {
        Write-Host "⚠️  No changes found in [Unreleased] section" -ForegroundColor Yellow
        Write-Host "Consider adding some changes before creating a release." -ForegroundColor Yellow
        return
    }
    
    # Replace [Unreleased] with new version
    $newReleasePattern = "## [$Version] - $date`n"
    $content = $content -replace '## \[Unreleased\]', $newReleasePattern
    
    # Add new Unreleased section at the top
    $newUnreleased = @"
## [Unreleased]

### Added
- New features in development

### Changed
- Breaking changes or improvements

### Fixed
- Bug fixes

### Security
- Security-related changes

"@
    
    $content = $content -replace '# Changelog', "# Changelog`n`n$newUnreleased"
    
    Set-Content $ChangelogPath $content -NoNewline
    Write-Host "✅ Created release $Version" -ForegroundColor Green
    Write-Host "📝 Don't forget to:" -ForegroundColor Yellow
    Write-Host "   1. Commit the changelog changes" -ForegroundColor Yellow
    Write-Host "   2. Create a git tag: git tag -a v$Version -m 'Release version $Version'" -ForegroundColor Yellow
    Write-Host "   3. Push the tag: git push origin v$Version" -ForegroundColor Yellow
}

function Show-Changelog {
    if (-not (Test-Path $ChangelogPath)) {
        Write-Host "❌ CHANGELOG.md not found!" -ForegroundColor Red
        exit 1
    }
    
    $content = Get-Content $ChangelogPath
    $inUnreleased = $false
    $inSection = $false
    
    Write-Host "📋 Current Changelog Status:" -ForegroundColor Cyan
    Write-Host "=============================" -ForegroundColor Cyan
    
    foreach ($line in $content) {
        if ($line -match '## \[Unreleased\]') {
            $inUnreleased = $true
            Write-Host "`n🔴 [Unreleased]" -ForegroundColor Red
            continue
        } elseif ($line -match '## \[(\d+\.\d+\.\d+)\]') {
            $inUnreleased = $false
            $version = $matches[1]
            Write-Host "`n🟢 [$version]" -ForegroundColor Green
            continue
        }
        
        if ($inUnreleased) {
            if ($line -match '### (\w+)') {
                $section = $matches[1]
                Write-Host "  📝 $section" -ForegroundColor Yellow
                $inSection = $true
            } elseif ($line -match '^\s*-\s*(.+)') {
                $entry = $matches[1]
                Write-Host "    • $entry" -ForegroundColor White
            } elseif ($line -match '^\s*$') {
                $inSection = $false
            }
        }
    }
    
    if (-not $inUnreleased) {
        Write-Host "`n📝 No unreleased changes found." -ForegroundColor Yellow
    }
}

# Main execution
switch ($Action) {
    "add" {
        if (-not $Type -or -not $Message) {
            Write-Host "❌ Both -Type and -Message are required for 'add' action" -ForegroundColor Red
            exit 1
        }
        Add-ChangelogEntry -Type $Type -Message $Message
    }
    "release" {
        if (-not $Version) {
            Write-Host "❌ -Version is required for 'release' action" -ForegroundColor Red
            exit 1
        }
        New-Release -Version $Version
    }
    "list" {
        Show-Changelog
    }
} 