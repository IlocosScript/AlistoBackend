#!/usr/bin/env pwsh

<#
.SYNOPSIS
    Validates that all API endpoints follow the ApiResponse<T> format rules.

.DESCRIPTION
    This script scans all controller files in the Alisto backend to ensure compliance
    with the API response rules defined in API_RESPONSE_RULES.md.

.PARAMETER ProjectPath
    The path to the Alisto.Api project directory. Defaults to current directory.

.PARAMETER FailOnViolation
    Whether to exit with error code 1 if violations are found. Defaults to true.

.EXAMPLE
    .\validate-api-response-rules.ps1 -ProjectPath ".\Alisto.Api"

.EXAMPLE
    .\validate-api-response-rules.ps1 -FailOnViolation $false
#>

param(
    [string]$ProjectPath = ".",
    [bool]$FailOnViolation = $true
)

# Colors for output
$Red = "Red"
$Green = "Green"
$Yellow = "Yellow"
$White = "White"

# Initialize counters
$totalEndpoints = 0
$compliantEndpoints = 0
$violations = @()
$warnings = @()

Write-Host "🔍 API Response Rules Validation" -ForegroundColor $White
Write-Host "=================================" -ForegroundColor $White
Write-Host ""

# Find all controller files
$controllerFiles = Get-ChildItem -Path $ProjectPath -Recurse -Filter "*Controller.cs" | Where-Object { $_.FullName -like "*Controllers*" }

if (-not $controllerFiles) {
    Write-Host "❌ No controller files found in $ProjectPath" -ForegroundColor $Red
    exit 1
}

Write-Host "📁 Found $($controllerFiles.Count) controller files" -ForegroundColor $White
Write-Host ""

foreach ($file in $controllerFiles) {
    Write-Host "🔍 Analyzing: $($file.Name)" -ForegroundColor $Yellow
    
    $content = Get-Content $file.FullName -Raw
    $lines = Get-Content $file.FullName
    
    # Find all public methods that return IActionResult
    $methodPattern = 'public\s+async\s+Task<IActionResult>\s+(\w+)\s*\('
    $methods = [regex]::Matches($content, $methodPattern)
    
    foreach ($method in $methods) {
        $methodName = $method.Groups[1].Value
        $totalEndpoints++
        
        Write-Host "  📋 Method: $methodName" -ForegroundColor $White
        
        # Find the method body
        $methodStart = $method.Index
        $methodEnd = $content.IndexOf('}', $methodStart)
        $methodBody = $content.Substring($methodStart, $methodEnd - $methodStart + 1)
        
        # Check for violations
        $hasViolations = $false
        $methodViolations = @()
        
        # 1. Check for direct returns without ApiResponse
        $directReturnPatterns = @(
            'return\s+Ok\([^A]',           # return Ok(something) but not ApiResponse
            'return\s+NotFound\([^A]',     # return NotFound(something) but not ApiResponse
            'return\s+BadRequest\([^A]',   # return BadRequest(something) but not ApiResponse
            'return\s+CreatedAtAction\([^A]', # return CreatedAtAction(...) but not ApiResponse
            'return\s+StatusCode\([^A]'    # return StatusCode(...) but not ApiResponse
        )
        
        foreach ($pattern in $directReturnPatterns) {
            if ($methodBody -match $pattern) {
                $hasViolations = $true
                $methodViolations += "Direct return without ApiResponse wrapper"
                break
            }
        }
        
        # 2. Check for missing try-catch blocks
        if ($methodBody -notmatch 'try\s*\{' -or $methodBody -notmatch 'catch\s*\{') {
            $warnings += "Method '$methodName' in $($file.Name) may be missing try-catch block"
        }
        
        # 3. Check for proper ApiResponse usage
        $apiResponsePatterns = @(
            'new\s+ApiResponse<[^>]+>\s*\{',
            'Success\s*=\s*(true|false)',
            'Message\s*='
        )
        
        $hasApiResponse = $true
        foreach ($pattern in $apiResponsePatterns) {
            if ($methodBody -notmatch $pattern) {
                $hasApiResponse = $false
                break
            }
        }
        
        if (-not $hasApiResponse) {
            $hasViolations = $true
            $methodViolations += "Missing proper ApiResponse structure"
        }
        
        # 4. Check for pagination headers in list methods
        if ($methodName -match '^Get.*s$' -or $methodName -match 'GetAll') {
            if ($methodBody -notmatch 'X-Total-Count' -or $methodBody -notmatch 'X-Total-Pages') {
                $warnings += "List method '$methodName' in $($file.Name) may be missing pagination headers"
            }
        }
        
        # 5. Check for proper error handling
        if ($methodBody -match 'catch\s*\{' -and $methodBody -notmatch 'StatusCode\(500,\s*new\s+ApiResponse') {
            $warnings += "Method '$methodName' in $($file.Name) may have incomplete error handling"
        }
        
        if ($hasViolations) {
            $violations += "Method '$methodName' in $($file.Name): $($methodViolations -join ', ')"
            Write-Host "    ❌ Violations found" -ForegroundColor $Red
        } else {
            $compliantEndpoints++
            Write-Host "    ✅ Compliant" -ForegroundColor $Green
        }
    }
    
    Write-Host ""
}

# Summary
Write-Host "📊 Validation Summary" -ForegroundColor $White
Write-Host "===================" -ForegroundColor $White
Write-Host "Total endpoints analyzed: $totalEndpoints" -ForegroundColor $White
Write-Host "Compliant endpoints: $compliantEndpoints" -ForegroundColor $Green
Write-Host "Non-compliant endpoints: $($totalEndpoints - $compliantEndpoints)" -ForegroundColor $Red

if ($violations.Count -gt 0) {
    Write-Host ""
    Write-Host "❌ VIOLATIONS FOUND:" -ForegroundColor $Red
    foreach ($violation in $violations) {
        Write-Host "  • $violation" -ForegroundColor $Red
    }
}

if ($warnings.Count -gt 0) {
    Write-Host ""
    Write-Host "⚠️  WARNINGS:" -ForegroundColor $Yellow
    foreach ($warning in $warnings) {
        Write-Host "  • $warning" -ForegroundColor $Yellow
    }
}

Write-Host ""

# Compliance percentage
$compliancePercentage = if ($totalEndpoints -gt 0) { [math]::Round(($compliantEndpoints / $totalEndpoints) * 100, 2) } else { 0 }

if ($compliancePercentage -eq 100) {
    Write-Host "🎉 100% COMPLIANCE ACHIEVED!" -ForegroundColor $Green
} elseif ($compliancePercentage -ge 90) {
    Write-Host "✅ $compliancePercentage% compliance - Good job!" -ForegroundColor $Green
} elseif ($compliancePercentage -ge 75) {
    Write-Host "⚠️  $compliancePercentage% compliance - Needs improvement" -ForegroundColor $Yellow
} else {
    Write-Host "❌ $compliancePercentage% compliance - Critical issues found" -ForegroundColor $Red
}

Write-Host ""

# Exit with appropriate code
if ($FailOnViolation -and $violations.Count -gt 0) {
    Write-Host "❌ Validation failed due to violations" -ForegroundColor $Red
    exit 1
} elseif ($violations.Count -gt 0) {
    Write-Host "⚠️  Validation completed with violations (not failing build)" -ForegroundColor $Yellow
    exit 0
} else {
    Write-Host "✅ Validation passed successfully" -ForegroundColor $Green
    exit 0
} 