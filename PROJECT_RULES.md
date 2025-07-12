# Alisto Backend - Project Rules

## .NET Version Management
- **DO NOT** upgrade or downgrade .NET SDK, runtime, or NuGet package versions without explicit approval
- All code, migrations, and instructions must work with current project versions
- You may suggest upgrades if necessary, but require explicit user approval before applying
- No automatic changes to .NET or package versions

## API Response Standards
- All endpoints must use standardized `ApiResponse<T>` format
- Follow the rules defined in `API_RESPONSE_RULES.md`
- Include proper error handling and logging

## Code Style & Patterns
- Use consistent naming conventions (PascalCase for C# properties, camelCase for TypeScript)
- Follow existing patterns in the codebase
- Include proper XML documentation for public APIs

## Database & Migrations
- **NEVER run migrations automatically** - only suggest them
- Always create migrations for model changes when needed
- Use meaningful migration names
- Test migrations before committing
- Ask for user approval before running any migration commands

## Security
- Never hardcode sensitive information
- Use configuration files for environment-specific settings
- Validate all user inputs

## Testing
- Include unit tests for new functionality
- Follow existing test patterns in the project

## Documentation
- Update relevant documentation when making changes
- Include examples in API documentation
- Update changelog for significant changes

## File Organization
- Follow existing folder structure
- Place new models in `Models/` folder
- Place new DTOs in `DTO/` folder
- Place new controllers in `Controllers/` folder

## TypeScript Types
- Keep TypeScript types in sync with C# models
- Update `types/` folder when changing models
- Use proper TypeScript interfaces and types

## Error Handling
- Use consistent error response format
- Include meaningful error messages
- Log errors appropriately

## Performance
- Consider performance implications of changes
- Use async/await patterns consistently
- Optimize database queries where possible 