# Alisto Backend API - Postman Collection

This directory contains a comprehensive Postman collection for testing the Alisto Backend API, along with a pre-configured environment for easy setup.

## 📁 Files

- `Alisto_API.postman_collection.json` - Complete API collection
- `Alisto_API_Environment.postman_environment.json` - Environment variables
- `README.md` - This setup guide

## 🚀 Quick Setup

### 1. Import Collection
1. Open Postman
2. Click **Import** button
3. Select `Alisto_API.postman_collection.json`
4. The collection will be imported with all endpoints organized by feature

### 2. Import Environment
1. Click **Import** button again
2. Select `Alisto_API_Environment.postman_environment.json`
3. Select the environment from the dropdown in the top-right corner

### 3. Configure Base URL
1. Open the environment settings (gear icon)
2. Update the `baseUrl` variable to match your backend URL:
   - **Local Development**: `https://localhost:7001`
   - **Staging**: `https://staging.alisto-api.com`
   - **Production**: `https://api.alisto.com`

## 📋 Collection Structure

### 🔐 Authentication
- **Login** - Authenticate user and get tokens
- **Register** - Create new user account
- **Logout** - End user session
- **Refresh Token** - Renew access token
- **Get Current User** - Get authenticated user info

### 👤 User Management
- **Get Users** - List all users (paginated)
- **Get User by ID** - Get specific user details
- **Create User** - Register new user
- **Update User** - Modify user information
- **Delete User** - Remove user account
- **Activate User** - Reactivate user account

### 📅 Appointments
- **Get Appointments** - List appointments with filters
- **Get Appointment by ID** - Get specific appointment
- **Create Appointment** - Book new appointment
- **Update Appointment** - Modify appointment details
- **Cancel Appointment** - Cancel appointment
- **Update Appointment Status** - Change appointment status
- **Get Appointment Statuses** - List available statuses
- **Get Payment Statuses** - List payment statuses

### 🚨 Issue Reports
- **Get Issue Reports** - List public issue reports
- **Get Issue Report by ID** - Get specific issue details
- **Create Issue Report** - Submit new issue report
- **Update Issue Report** - Modify issue report
- **Delete Issue Report** - Remove issue report
- **Update Issue Status** - Change issue status
- **Get Issue Categories** - List issue categories
- **Get Issue Statuses** - List issue statuses

### 📰 News
- **Get News** - List news articles with filters
- **Get News Article by ID** - Get specific article
- **Create News Article** - Create new article
- **Update News Article** - Modify article
- **Delete News Article** - Remove article
- **Publish News Article** - Publish draft article
- **Get Featured News** - Get featured articles
- **Get Trending News** - Get trending articles

### 💬 Feedback
- **Create App Feedback** - Submit app feedback
- **Create Service Feedback** - Submit service feedback

### 🏛️ City Services
- **Get City Services** - List available services
- **Get Service Categories** - List service categories

### 📊 Dashboard
- **Get Dashboard Stats** - Get system statistics

## 🔧 Environment Variables

### Authentication Variables
- `accessToken` - JWT access token (auto-populated)
- `sessionId` - User session ID (auto-populated)
- `userId` - Current user ID (auto-populated)

### User Variables
- `email` - User email for testing
- `password` - User password for testing
- `firstName`, `lastName`, `middleName` - User names
- `phoneNumber` - Contact number
- `address` - User address
- `dateOfBirth` - Birth date
- `emergencyContactName`, `emergencyContactNumber` - Emergency contact

### Appointment Variables
- `appointmentId` - Appointment ID for testing
- `serviceId` - Service ID for booking
- `appointmentDate`, `appointmentTime` - Appointment scheduling
- `notes` - Appointment notes
- `applicantFirstName`, `applicantLastName`, etc. - Applicant details
- `documentType`, `purpose` - Service-specific data

### Issue Report Variables
- `issueReportId` - Issue report ID for testing
- `issueCategory` - Issue category (Infrastructure, RoadIssues, etc.)
- `urgencyLevel` - Urgency level (Low, Medium, High, Critical)
- `issueTitle`, `issueDescription` - Issue details
- `issueLocation`, `coordinates` - Location information
- `priority` - Priority level
- `publiclyVisible` - Visibility setting

### News Variables
- `newsId` - News article ID for testing
- `newsTitle`, `newsSummary`, `newsFullContent` - Article content
- `newsImageUrl` - Article image
- `publishedDate`, `publishedTime` - Publication details
- `newsLocation` - Article location
- `newsCategory` - Article category
- `author` - Article author
- `tag1`, `tag2` - Article tags

### General Variables
- `page`, `pageSize` - Pagination settings
- `status`, `paymentStatus` - Filter options
- `rating`, `feedbackComment` - Feedback data
- `autoSetDates` - Auto-set current dates (true/false)

## 🔄 Auto-Setup Features

### Automatic Token Extraction
The collection includes scripts that automatically extract tokens from login/register responses:
- `accessToken` - JWT token for API calls
- `sessionId` - Session identifier
- `userId` - User identifier

### Date Auto-Setup
When `autoSetDates` is enabled, the collection automatically sets:
- `currentDate` - Today's date in YYYY-MM-DD format
- `currentTime` - Current time in HH:MM:SS format

## 🧪 Testing Workflow

### 1. Authentication Flow
1. Run **Login** or **Register** request
2. Tokens are automatically extracted and stored
3. Subsequent requests will use the stored tokens

### 2. User Management Flow
1. Create a user with **Create User**
2. Use the returned user ID for other operations
3. Test update and delete operations

### 3. Appointment Flow
1. Create an appointment with **Create Appointment**
2. Use the returned appointment ID for status updates
3. Test different appointment statuses

### 4. Issue Report Flow
1. Create an issue report with **Create Issue Report**
2. Use the returned issue ID for updates
3. Test status changes and assignments

### 5. News Flow
1. Create a news article with **Create News Article**
2. Test publishing and updating
3. Verify public access to published articles

## 🔒 Security Notes

### Sensitive Variables
The following variables are marked as "secret" and will be hidden in Postman:
- `accessToken`
- `sessionId`
- `password`

### Environment Switching
Create multiple environments for different stages:
- **Local** - `https://localhost:7001`
- **Development** - `https://dev-api.alisto.com`
- **Staging** - `https://staging-api.alisto.com`
- **Production** - `https://api.alisto.com`

## 🛠️ Customization

### Adding New Variables
1. Open environment settings
2. Add new variable with appropriate type
3. Update collection requests to use the variable

### Modifying Requests
1. Edit request body/headers as needed
2. Use environment variables with `{{variableName}}` syntax
3. Test the modified request

### Creating Test Scripts
Add JavaScript in the **Tests** tab for:
- Response validation
- Data extraction
- Environment variable updates
- Custom assertions

## 📚 Best Practices

1. **Always use environment variables** instead of hardcoded values
2. **Test authentication flow first** before other operations
3. **Use appropriate HTTP methods** (GET, POST, PUT, DELETE, PATCH)
4. **Validate responses** with test scripts
5. **Keep sensitive data secure** using secret variables
6. **Document custom modifications** for team sharing

## 🔗 Related Resources

- [Postman Documentation](https://learning.postman.com/)
- [REST API Best Practices](https://restfulapi.net/)
- [JWT Token Authentication](https://jwt.io/)
- [HTTP Status Codes](https://httpstatuses.com/)

## 🆘 Troubleshooting

### Common Issues

**401 Unauthorized**
- Check if `accessToken` is set
- Verify token hasn't expired
- Re-run login request

**404 Not Found**
- Verify `baseUrl` is correct
- Check if resource ID exists
- Ensure endpoint path is correct

**422 Validation Error**
- Check request body format
- Verify required fields are provided
- Validate data types and formats

**500 Internal Server Error**
- Check server logs
- Verify database connection
- Contact backend team

### Getting Help
1. Check the API documentation
2. Review request/response examples
3. Test with different data values
4. Contact the development team 