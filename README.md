# Alisto Backend

A comprehensive city management API backend built with ASP.NET Core, providing services for user management, appointments, news, issue reporting, and various city services.

## 🚀 Features

- **User Management**: Complete user lifecycle with external authentication support
- **Appointment System**: Booking and management of city services
- **News Management**: Article publishing with categories and trending features
- **Issue Reporting**: Citizen issue reporting with photo uploads
- **Tourist Spots**: Location-based tourist information
- **Business Services**: Permit requests and business registration
- **Civil Registry**: Document requests and civil services
- **Public Projects**: Infrastructure project tracking
- **City Services**: Comprehensive service catalog

## 📋 Project Rules & Standards

### Development Rules
- **.NET Version Management**: Never upgrade or downgrade .NET SDK, runtime, or NuGet package versions without explicit approval
- **API Response Standards**: All endpoints must use standardized `ApiResponse<T>` format
- **Code Patterns**: Follow existing naming conventions and patterns in the codebase
- **TypeScript Sync**: Keep TypeScript types in sync with C# models

For complete project rules, see [PROJECT_RULES.md](./PROJECT_RULES.md) and [.cursorrules](./.cursorrules).

### API Response Standards

All API endpoints follow a standardized `ApiResponse<T>` format for consistency and predictability.

### Response Format
```json
{
  "success": true,
  "data": {
    // Response data here
  },
  "message": "Operation completed successfully",
  "errors": null,
  "timestamp": "2024-01-01T00:00:00Z",
  "requestId": null
}
```

### Validation
- **Automated**: CI/CD pipeline validates all endpoints
- **Manual**: Use the validation script: `scripts/validate-api-response-rules.ps1`
- **Rules**: See [API_RESPONSE_RULES.md](./API_RESPONSE_RULES.md) for detailed guidelines

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: Entity Framework Core with SQL Server
- **Authentication**: External provider integration
- **Documentation**: Swagger/OpenAPI
- **Containerization**: Docker support
- **CI/CD**: GitHub Actions

## 📦 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (or SQL Server Express)
- Docker (optional)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-org/alisto-backend.git
   cd alisto-backend
   ```

2. **Configure the database**
   ```bash
   # Update connection string in appsettings.json
   # Run migrations
   dotnet ef database update
   ```

3. **Run the application**
   ```bash
   dotnet run --project Alisto.Api
   ```

4. **Access the API**
   - API: http://localhost:5000
   - Swagger UI: http://localhost:5000/swagger

### Docker
```bash
# Build and run with Docker Compose
docker-compose up -d
```

## 📚 API Documentation

### Base URL
```
https://api.alisto.com/api
```

### Authentication
Most endpoints require authentication. Include your token in the Authorization header:
```
Authorization: Bearer <your-token>
```

### Common Endpoints

#### Users
- `GET /user` - Get all users (paginated)
- `GET /user/{id}` - Get user by ID
- `POST /user/from-auth` - Create user from external auth
- `PUT /user/{id}` - Update user
- `DELETE /user/{id}` - Delete user

#### Appointments
- `GET /appointment` - Get all appointments
- `POST /appointment` - Create appointment
- `PUT /appointment/{id}` - Update appointment
- `PATCH /appointment/{id}/status` - Update appointment status

#### News
- `GET /news` - Get all news articles
- `GET /news/{id}` - Get news article by ID
- `POST /news` - Create news article
- `PATCH /news/{id}/publish` - Publish news article

#### Tourist Spots
- `GET /touristspot` - Get all tourist spots
- `GET /touristspot/{id}` - Get tourist spot by ID
- `POST /touristspot` - Create tourist spot
- `PATCH /touristspot/{id}/activate` - Activate tourist spot

### Pagination
List endpoints support pagination with these query parameters:
- `page` (default: 1)
- `pageSize` (default: 10)

Response headers include:
- `X-Total-Count`: Total number of records
- `X-Total-Pages`: Total number of pages
- `X-Current-Page`: Current page number

## 🔧 Development

### Project Structure
```
AlistoBackend/
├── Alisto.Api/                 # Main API project
│   ├── Controllers/            # API controllers
│   ├── Models/                 # Entity models
│   ├── DTO/                    # Data transfer objects
│   ├── Data/                   # Database context
│   └── Enums/                  # Enumerations
├── scripts/                    # Utility scripts
├── .github/workflows/          # CI/CD workflows
└── docs/                       # Documentation
```

### Code Standards
- Follow the [API Response Rules](./API_RESPONSE_RULES.md)
- Use conventional commit messages
- Include unit tests for new features
- Update documentation for API changes

### Validation
```bash
# Validate API response rules
./scripts/validate-api-response-rules.ps1

# Check changelog status
./scripts/manage-changelog.ps1 -Action list
```

## 📝 Changelog

We maintain a detailed changelog to track all changes and releases.

### Viewing Changes
- **Current**: [CHANGELOG.md](./CHANGELOG.md)
- **Releases**: [GitHub Releases](https://github.com/your-org/alisto-backend/releases)

### Adding Changes
```bash
# Add a new feature
./scripts/manage-changelog.ps1 -Action add -Type added -Message "New user profile endpoint"

# Add a bug fix
./scripts/manage-changelog.ps1 -Action add -Type fixed -Message "Fix appointment date validation"

# Create a release
./scripts/manage-changelog.ps1 -Action release -Version "1.3.0"
```

### Release Process
1. **Add changes** to the `[Unreleased]` section
2. **Create release** with version number
3. **Tag and push** to trigger automated release
4. **GitHub Actions** will create the release automatically

## 🧪 Testing

### Running Tests
```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test Alisto.Api.Tests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### API Testing
- **Postman Collection**: [postman/Alisto_API.postman_collection.json](./postman/Alisto_API.postman_collection.json)
- **Environment**: [postman/Alisto_API_Environment.postman_environment.json](./postman/Alisto_API_Environment.postman_environment.json)

## 🚀 Deployment

### Environment Configuration
- **Development**: `appsettings.Development.json`
- **Production**: `appsettings.Production.json`
- **Docker**: `compose.yml`

### CI/CD Pipeline
- **Validation**: API response rules validation
- **Testing**: Unit and integration tests
- **Build**: Docker image creation
- **Deploy**: Automatic deployment to staging/production

## 🤝 Contributing

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/amazing-feature`)
3. **Follow** the [API Response Rules](./API_RESPONSE_RULES.md)
4. **Add** changes to the changelog
5. **Commit** your changes (`git commit -m 'feat: add amazing feature'`)
6. **Push** to the branch (`git push origin feature/amazing-feature`)
7. **Open** a Pull Request

### Development Guidelines
- Follow the established API response format
- Include comprehensive error handling
- Add appropriate logging
- Write unit tests for new functionality
- Update documentation for API changes

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

- **Documentation**: [API Documentation](./docs/)
- **Issues**: [GitHub Issues](https://github.com/your-org/alisto-backend/issues)
- **Discussions**: [GitHub Discussions](https://github.com/your-org/alisto-backend/discussions)

## 🔗 Links

- [API Response Rules](./API_RESPONSE_RULES.md)
- [Changelog](./CHANGELOG.md)
- [Postman Collection](./postman/)
- [TypeScript Types](./types/)

---

**Last Updated**: January 2024  
**Version**: 1.2.0  
**Maintainer**: Development Team 