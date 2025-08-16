CleanShop

CleanShop is a modern e-commerce application built using .NET 8, Clean Architecture, and CQRS principles. It demonstrates best practices for building scalable and maintainable enterprise-grade applications.

🚀 Features

    Clean Architecture: Separation of concerns into Domain, Application, Infrastructure, and API layers.
    
    CQRS: Command Query Responsibility Segregation for handling commands and queries separately.
    
    Entity Framework Core: ORM for database interactions.
    
    JWT Authentication: Secure API endpoints with JSON Web Tokens.
    
    Swagger UI: Interactive API documentation for testing endpoints.
    
    Unit & Integration Tests: Comprehensive test coverage for reliable code.

🛠️ Technologies

    Backend: ASP.NET Core Web API
    
    Frontend: ReactJS + Redux (planned for future development)
    
    Database: SQL Server (via Entity Framework Core)
    
    Authentication: JWT Bearer Tokens
    
    API Documentation: Swagger/OpenAPI
    
    Testing: xUnit, FluentAssertions, Moq

📂 Project Structure

    CleanShop
    
    ├── Api               # API layer (Controllers, Swagger)
    
    ├── Application      # Application layer (Commands, Queries, Handlers)
    
    ├── Domain           # Domain layer (Entities, Value Objects, Aggregates)
    
    ├── Infrastructure   # Infrastructure layer (EF Core, Repositories, Services)
    
    └── Test             # Unit & Integration Tests


⚙️ Getting Started

    Prerequisites
    
    .NET 8 SDK
    
    SQL Server or SQL Server Express


    Setup
    
    Clone the repository:
    
    git clone https://github.com/reza-kh/CleanShop.git
    cd CleanShop
    
    
    Restore backend dependencies:
    
    dotnet restore
    
    
    Apply database migrations:
    
    dotnet ef database update
    
    
    Run the application:
    
    dotnet run --project Api


    Access the API at https://localhost:7194.

🧪 Running Tests

    To run unit and integration tests:

    dotnet test

📄 API Documentation

    Once the application is running, you can access the Swagger UI at:
    
    https://localhost:7194/swagger
    
Docker

    You can run the CleanShop API using Docker. The application exposes both HTTP and HTTPS ports:
    EXPOSE 5031
    EXPOSE 7194
    To build and run the Docker container:
    docker build -t cleanshop-api .
    docker run -d -p 5031:5031 -p 7194:7194 cleanshop-api


Here, you can explore and test API endpoints interactively.

📌 Notes

    The frontend is planned for future development using ReactJS and Redux.
    
    Contributions are welcome! Please fork the repository and submit pull requests.

📄 License

    This project is licensed under the MIT License - see the LICENSE file for details.
    
    Feel free to customize this README further based on your project's specifics and future plans.
