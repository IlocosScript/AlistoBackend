# Development Quick Reference

## 🚨 Critical Rules (Never Break)
1. **NO .NET version changes** without explicit approval
2. **Use ApiResponse<T>** format for all endpoints
3. **Follow existing patterns** in the codebase
4. **Create migrations** for model changes

## 📁 File Locations
- **Models**: `Alisto.Api/Models/`
- **DTOs**: `Alisto.Api/DTO/`
- **Controllers**: `Alisto.Api/Controllers/`
- **TypeScript**: `types/`

## 🔧 Common Commands
```bash
# Create migration (suggest only, don't run automatically)
dotnet ef migrations add MigrationName

# Update database (suggest only, don't run automatically)
dotnet ef database update

# Validate API responses
./scripts/validate-api-response-rules.ps1

# Update changelog
./scripts/manage-changelog.ps1 -Action add -Type added -Message "Description"
```

## 🚨 Migration Rules
- **NEVER run migrations automatically**
- Only suggest migration commands
- Always ask for user approval before running migrations
- Provide migration commands for user to run manually

## 📝 Code Patterns
- **C#**: PascalCase for properties/methods
- **TypeScript**: camelCase for properties/methods
- **API**: Always use ApiResponse<T> wrapper
- **Errors**: Include meaningful error messages

## 🔗 Important Files
- [PROJECT_RULES.md](./PROJECT_RULES.md) - Complete project rules
- [.cursorrules](./.cursorrules) - Cursor-specific rules
- [API_RESPONSE_RULES.md](./API_RESPONSE_RULES.md) - API standards
- [CHANGELOG.md](./CHANGELOG.md) - Project changes

## ⚠️ Before Committing
- [ ] Code follows project patterns
- [ ] API responses use ApiResponse<T>
- [ ] TypeScript types are updated
- [ ] Migrations are suggested (if needed) - **don't run automatically**
- [ ] Changelog is updated
- [ ] No .NET version changes 