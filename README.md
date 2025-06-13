# 🎵 Eventourismo - Street Music Map App

A complete, downloadable .NET solution for discovering and managing street music events with Clean Architecture principles.

## 📋 Overview

Eventourismo is a comprehensive mobile-first application designed to help users discover, create, and manage street music events. Built with .NET 8 and following Clean Architecture patterns, this solution provides a solid foundation for a production-ready application.

## 🏗️ Solution Structure

```
Eventourismo/
├── Eventourismo.sln
├── README.md
├── .gitignore
├── src/
│   ├── Eventourismo.Domain/          # Domain entities, value objects, enums
│   ├── Eventourismo.Application/     # DTOs, use cases, services interfaces
│   ├── Eventourismo.Infrastructure/  # Data access, repositories, external services
│   ├── Eventourismo.WebAPI/         # REST API, controllers, SignalR hubs
│   └── Eventourismo.Mobile/         # Mobile app architecture (MVVM)
├── tests/
│   ├── Eventourismo.Domain.Tests/
│   ├── Eventourismo.Application.Tests/
│   └── Eventourismo.WebAPI.Tests/
└── docs/
    └── API.md
```

## 🎯 Key Features

### 🌐 Web API
- ✅ **RESTful API** with clean endpoints
- ✅ **JWT Authentication** for secure access
- ✅ **SignalR Integration** for real-time updates
- ✅ **Entity Framework Core** with SQL Server
- ✅ **Swagger Documentation** for API testing
- ✅ **CORS Configuration** for mobile app support

### 📱 Mobile Architecture
- ✅ **MVVM Pattern** with CommunityToolkit.Mvvm
- ✅ **Dependency Injection** for services
- ✅ **API Communication** layer
- ✅ **Local Storage** service abstraction
- ✅ **Location Services** for geolocation
- ✅ **Clean ViewModels** with proper separation

### 🏢 Clean Architecture
- ✅ **Domain Layer** - Pure business logic and entities
- ✅ **Application Layer** - Use cases and DTOs
- ✅ **Infrastructure Layer** - Data access and external services
- ✅ **Presentation Layer** - API controllers and mobile UI

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code
- SQL Server (LocalDB or full instance)
- For mobile development: MAUI workload

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/FairHead/Eventourismo.git
   cd Eventourismo
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

4. **Update database connection:**
   Update the connection string in `src/Eventourismo.WebAPI/appsettings.json`

5. **Run database migrations:**
   ```bash
   cd src/Eventourismo.Infrastructure
   dotnet ef database update
   ```

6. **Start the API:**
   ```bash
   cd src/Eventourismo.WebAPI
   dotnet run
   ```

The API will be available at `https://localhost:7000` with Swagger UI at `https://localhost:7000/swagger`

## 📱 Mobile Application

The mobile application is structured with MVVM architecture and includes:

### Core Services
- **IApiService** - HTTP communication with the backend
- **ILocationService** - Geolocation and location permissions
- **IStorageService** - Secure local storage

### ViewModels
- **LoginViewModel** - User authentication
- **MapViewModel** - Interactive map with event pins
- **EventsViewModel** - Event list and management
- **ProfileViewModel** - User profile and settings

### Key Features
- 🗺️ **Map Integration** - View events on an interactive map
- 📍 **Location-based Discovery** - Find events near you
- 🎵 **Event Management** - Create, edit, and manage music events
- 👤 **User Profiles** - Personal accounts with event history
- ❤️ **Favorites & Likes** - Save and interact with events
- 💬 **Comments** - Community engagement features

## 🛠️ Technology Stack

### Backend
- **.NET 8** - Core framework
- **ASP.NET Core** - Web API framework
- **Entity Framework Core** - ORM for data access
- **SQL Server** - Primary database
- **SignalR** - Real-time communication
- **AutoMapper** - Object mapping
- **MediatR** - CQRS pattern implementation
- **FluentValidation** - Input validation

### Mobile
- **.NET MAUI** - Cross-platform mobile framework
- **CommunityToolkit.Mvvm** - MVVM helpers
- **Microsoft.Maui.Controls.Maps** - Map integration
- **System.Text.Json** - JSON serialization

### Testing
- **xUnit** - Testing framework
- **Moq** - Mocking framework (planned)

## 📊 Domain Model

### Core Entities
- **User** - Application users with roles
- **Event** - Street music events with location and timing
- **Venue** - Physical locations with opening hours
- **Comment** - User comments on events
- **Like** - User likes on events
- **Favorite** - User favorites

### Value Objects
- **Location** - Geographical coordinates with address
- **OpeningHours** - Venue operating hours

### Enums
- **UserRole** - User, Moderator, Admin
- **EventType** - StreetMusic, Concert, Festival, etc.
- **Platform** - Web, Android, iOS, Desktop

## 🔧 Configuration

### Database Configuration
Update `appsettings.json` with your database connection:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EventourismoDb;Trusted_Connection=true;"
  }
}
```

### JWT Configuration
Configure JWT settings:
```json
{
  "Jwt": {
    "Key": "YourSecretKeyHere123456789012345678901234567890",
    "Issuer": "Eventourismo.WebAPI",
    "Audience": "Eventourismo.Mobile"
  }
}
```

## 🧪 Testing

Run the test suite:
```bash
dotnet test
```

## 📚 API Documentation

The API includes comprehensive Swagger documentation. After starting the API, visit:
- **Swagger UI**: `https://localhost:7000/swagger`
- **OpenAPI Spec**: `https://localhost:7000/swagger/v1/swagger.json`

### Key Endpoints
- `POST /api/auth/login` - User authentication
- `POST /api/auth/register` - User registration
- `GET /api/events` - Get events (with location filtering)
- `POST /api/events` - Create new event
- `GET /api/events/{id}` - Get event details

## 🔄 Real-time Features

The application uses SignalR for real-time updates:
- **Event Hub** - Live event updates and notifications
- **Connection Groups** - Event-specific channels
- **Live Comments** - Real-time comment updates

## 🏆 Best Practices Implemented

- ✅ **Clean Architecture** - Clear separation of concerns
- ✅ **SOLID Principles** - Maintainable and extensible code
- ✅ **Repository Pattern** - Data access abstraction
- ✅ **Unit of Work** - Transaction management
- ✅ **CQRS Pattern** - Command/Query separation
- ✅ **Dependency Injection** - Loose coupling
- ✅ **Value Objects** - Domain modeling
- ✅ **Domain Events** - Business rule enforcement

## 🚀 Deployment

### API Deployment
The API can be deployed to:
- **Azure App Service**
- **Docker containers**
- **IIS**
- **Linux servers with Kestrel**

### Mobile Deployment
- **Android**: Google Play Store
- **iOS**: Apple App Store
- **Windows**: Microsoft Store

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For support, please:
1. Check the documentation
2. Search existing issues
3. Create a new issue with detailed information

## 🎯 Roadmap

- [ ] Push notifications
- [ ] Offline mode with synchronization
- [ ] Social features (friend following)
- [ ] Event recommendations
- [ ] Payment integration
- [ ] Multi-language support
- [ ] Dark theme
- [ ] Performance optimizations

---

**Built with ❤️ for the street music community**
