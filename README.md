# UserAuthentication

A full-stack authentication system built with ASP.NET Core 8.0 and Angular 18, demonstrating modern web development practices with secure user registration, login, and profile management.

## Overview

This project demonstrates a complete authentication workflow with:
- **Backend API**: RESTful API built with ASP.NET Core and Microsoft Identity Framework
- **Database**: SQL Server with Entity Framework Core ORM
- **Frontend**: Single-page application (SPA) built with Angular 18
- **Security**: User registration, authentication, and password management
- **Documentation**: Swagger/OpenAPI for API exploration

## Tech Stack

### Backend (AuthECAPI)
- **.NET 8.0** - Latest .NET framework
- **ASP.NET Core** - Web framework
- **Entity Framework Core** - ORM for database operations
- **Microsoft Identity Framework** - Authentication and authorization
- **SQL Server** - Relational database
- **Swagger/OpenAPI** - API documentation

### Frontend (AuthECClient)
- **Angular 18** - Modern web framework
- **TypeScript** - Type-safe programming
- **RxJS** - Reactive programming library
- **Angular Router** - Client-side routing
- **Bootstrap/CSS** - Styling

## Project Structure

```
UserAuthentication/
├── AuthECAPI/                          # Backend API Project
│   ├── AuthECAPI.sln                   # Solution file
│   ├── AuthECAPI/
│   │   ├── Program.cs                  # Application entry point
│   │   ├── appsettings.json            # Configuration
│   │   ├── Controllers/                # API endpoints
│   │   ├── Models/                     # Data models
│   │   │   ├── AppUser.cs              # User entity
│   │   │   └── AppDbContext.cs         # EF Core context
│   │   └── Migrations/                 # Database migrations
│   └── AuthECAPI.csproj               # Project file
│
└── AuthECClient/                       # Angular Frontend
    ├── package.json                    # Dependencies
    ├── angular.json                    # Angular configuration
    └── src/
        ├── main.ts                     # Application bootstrap
        ├── index.html                  # HTML template
        └── app/
            ├── app.component.ts        # Root component
            ├── app.routes.ts           # Routing configuration
            ├── user/                   # User features
            │   ├── user.component.ts
            │   ├── login/              # Login component
            │   └── registration/       # Registration component
            └── shared/
                ├── pipes/              # Custom pipes
                │   └── first-key.pipe.ts
                └── service/            # Services
                    └── auth.service.ts # Authentication service
```

## Prerequisites

- **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download)
- **Node.js 20+** - [Download](https://nodejs.org/)
- **npm 10+** - Comes with Node.js
- **SQL Server** - Local or cloud instance
- **Visual Studio** or **VS Code** - Code editor

## Installation & Setup

### Backend Setup (AuthECAPI)

1. **Navigate to backend directory:**
   ```bash
   cd AuthECAPI/AuthECAPI
   ```

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Update database connection string** in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DevDB": "Server=YOUR_SERVER;Database=AuthEC_DB;User Id=sa;Password=YOUR_PASSWORD;"
   }
   ```

4. **Apply database migrations:**
   ```bash
   dotnet ef database update
   ```

5. **Run the API:**
   ```bash
   dotnet run
   ```
   API will be available at `https://localhost:5001` (or configured port)

### Frontend Setup (AuthECClient)

1. **Navigate to frontend directory:**
   ```bash
   cd AuthECClient
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Start development server:**
   ```bash
   npm start
   ```
   or
   ```bash
   ng serve
   ```
   Application will be available at `http://localhost:4200`

## Available Scripts

### Backend (AuthECAPI)
```bash
dotnet run                    # Run the API
dotnet build                  # Build the solution
dotnet ef migrations add      # Create new migration
dotnet ef database update     # Apply migrations
dotnet test                   # Run tests
```

### Frontend (AuthECClient)
```bash
npm start                     # Start dev server
npm run build                 # Build for production
npm run watch                 # Watch mode
npm test                      # Run unit tests
npm run e2e                   # Run end-to-end tests
ng generate component name    # Generate new component
```

## Features

### Authentication
- User registration with email validation
- User login with secure password handling
- Password strength requirements (configurable)
- Unique email constraint
- JWT token-based authentication

### User Management
- User profile with full name storage
- User data persistence in SQL Server
- Entity Framework Core ORM integration
- Database migrations for schema management

### API
- RESTful endpoints for authentication
- Swagger UI for API exploration
- CORS support for frontend integration
- Error handling and validation

### Frontend
- Angular routing for navigation
- Reactive forms with validation
- Shared services for state management
- Custom pipes for data transformation
- TypeScript for type safety

## API Endpoints

### Authentication Endpoints
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user
- `POST /api/auth/logout` - Logout user
- `GET /api/auth/profile` - Get user profile

### Documentation
- Swagger UI available at: `https://localhost:5001/swagger`

## Database Schema

### AppUser Table
```sql
- Id (PK)
- Email (Unique)
- PasswordHash
- FullName
- CreatedAt
- UpdatedAt
```

## Configuration

### Backend Configuration (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DevDB": "Server=localhost;Database=AuthEC;..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Password Policy
- Digit requirement: Disabled
- Lowercase requirement: Disabled
- Uppercase requirement: Disabled
- Unique email: Required

## Development Workflow

1. **Clone repository**
   ```bash
   git clone <repository-url>
   cd UserAuthentication
   ```

2. **Setup backend** (see Backend Setup above)

3. **Setup frontend** (see Frontend Setup above)

4. **Make changes** to components, services, or controllers

5. **Test changes** in respective applications

6. **Commit and push** changes

## Troubleshooting

### Backend Issues
- **Connection String Error**: Verify SQL Server is running and credentials are correct
- **Migration Error**: Run `dotnet ef database update` to apply pending migrations
- **Port Already in Use**: Change port in `launchSettings.json`

### Frontend Issues
- **ng serve fails**: Run `npm install` and clear node_modules cache
- **Build errors**: Check Angular CLI version matches project requirements
- **CORS issues**: Verify backend API URL in `environment.ts`

## Build & Deployment

### Backend Deployment
```bash
dotnet publish -c Release
# Deploy the published output to your hosting environment
```

### Frontend Deployment
```bash
ng build --configuration production
# Deploy the dist/ folder to your web server
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contact & Support

For issues, questions, or suggestions, please open an issue on the repository.

## Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Angular Documentation](https://angular.dev)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Microsoft Identity Framework](https://docs.microsoft.com/aspnet/core/security/authentication)

---

**Last Updated**: January 2026
