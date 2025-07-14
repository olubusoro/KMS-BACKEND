# CS-KMS-Backend

## Description

This is the backend project for CS-KMS.

## 📖 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Environment Variables](#environment-variables) <!-- - [API Documentation](#api-documentation) --> <!-- - [Testing](#testing) -->
- [Folder Structure](#folder-structure)
<!-- - [Deployment](#deployment) -->
<!-- - [Contributing](#contributing)
- [License](#license)
- [Contact](#contact) -->

## 🔍 Overview

> CS-KMS-Backend is a RESTful API for managing knowledge within an organization. It supports user authentication, role-based access control, knowledge categorization, document management, and audit logging.

## ✨ Features
* **User Roles & Permissions** - Super Admin, Department Admin, Staff (with multi-department enrollment). 
* **Department & Category Management** - Super Admin creates departments; Department Admins define knowledge categories.
* **Knowledge Creation** - Staff can post knowledge with: WYSIWYG editor, file attachments, and visibility settings (Public/Private/Department-Restricted).
* **Search & Access Control** - Global search displays knowledge titles/descriptions; users can request access to private content.
* **Document Management** - Supports multiple file uploads (PDFs, Word, Excel, etc.).
* **Audit & Logging** - Tracks knowledge creation, edits, and access requests.

## 🛠 Tech Stack
- Backend: .NET 8, ASP.NET Core Web API
- Database: SQL Server / PostgreSQL / SQLite (configurable)
- Authentication: JWT Bearer Tokens
- ORM: Entity Framework Core

## 🚀 Getting Started

These instructions will guide you through setting up the project on your local machine for development.

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download) (version 8.X)
* [A database system](https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-7.0#set-up-the-connection-with-sql-server) (e.g., SQL Server, PostgreSQL, SQLite).  Install and configure it.

### Installation Steps

1.  **Clone the Repository:**

    ```bash
    git clone <repository_url>
    cd <project_directory>
    ```

    (Replace `<repository_url>` with the actual URL of the Git repository and `<project_directory>` with the name of the directory where the project is cloned.)

2.  **Restore .NET Tools:**

    ```bash
    dotnet tool restore
    ```
    This command restores the tools specified in the `.config/dotnet-tools.json` file.  These are tools used by the project, and may include Entity Framework Core tools.

3.  **Restore NuGet Packages:**

    ```bash
    dotnet restore
    ```
    This command restores the NuGet packages required by the project, as specified in the project's `.csproj` file(s).

4.  **Configure the Database Connection String:**

    * Create a `.env` file in the project or use the user secrets feature of VS.
    * Modify the connection string to match your local database setup.  The connection string key will be similar to this:
        ```env file
      ConnectionStrings__SqlConnection="Server=...;Database=...;User Id=...;Password=..." // Example for SQL Server
      ConnectionStrings__PgSqlConnection="Host=...;Port=...;Database=...;Username=...;Password=..." // Example for Postgres Sql
        ```
	    Or for user secrets:
       ```json
        "ConnectionStrings": {
          "SqlConnection": "Server=...;Database=...;User Id=...;Password=..." // Example for SQL Server
          // Or for PostgreSQL:
          //"DefaultConnection": "Host=...;Port=...;Database=...;Username=...;Password=..."
          // Or for SQLite
          //"DefaultConnection": "Data Source=...;"
        }
       ```
    * **Important:** Ensure the database server is running and that the specified database exists.  If the database does not exist, and EF Core is set to create it, the next step will attempt creation. Also get other environment variables from others working on the project.

6.  **Apply Database Migrations:**

    ```bash
    dotnet tool run dotnet-ef database update
    ```
    If this doesn't work try this
    ```bash 
    dotnet tool run dotnet-ef database update -p CsKmsBackend.Infrastructure -s CsKmsBackend.Presentation
    ```
    This command applies any pending Entity Framework Core migrations to your database, creating the database schema.  This will create the tables and relationships defined by the code.

6.  **Seed the Database (Optional):**

    ```bash
    dotnet run --seed
    ```
    If this doesn't work try this
    ```bash
    dotnet run --seed --project CsKmsBackend.Presentattion
    ``` 
    This command runs the application with the `--seed` argument. This project has a seeding mechanism implemented (e.g., in `Program.cs` and a dedicated `SeedData` class) to populate the database with initial data (e.g., default users, configuration data).  If the application does not have a seed option, omit this step, or check the application's documentation.  If there is no seed option, the command may result in an error, or the application running normally.

7.  **Run the Application:**

    ```bash
    dotnet run
    ```
    If this doesn't work try this
    ```bash
    dotnet run --project CsKmsbackend.Presentation
    ```
    This command builds and runs the .NET application.  You should then be able to access the application in your web browser at the address specified in the application's configuration (usually `http://localhost:xxxx`).

## ⚙️ Environment Variables

```env
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__SqlConnection="your_db_connection_string"
Authentication__Key=64bit_secret_key
Authentication__Issuer=your_api_domain
Authentication__Audience=your_consuming_domain
Authentication__TokenLifeTimeMinutes=token_expiration_in_minutes
EMAILSETTINGS__SMTPSERVER=your_smtp_server
EMAILSETTINGS__SMTPPORT=587
EMAILSETTINGS__SENDERNAME=your_app_name
EMAILSETTINGS__SENDEREMAIL=your_domain_mail
BREVO__APIKEY=your_brevo_api_key
```

## 📁 Folder Structure
```plaintext
CS-KMS-Backend/
├── CsKmsBackend.Application/                # Business logic layer
│   ├── DependencyInjection/                 # Registers services for DI container
│   ├── DTOs/                                # Data Transfer Objects for API contracts
│   │   └── Conversions/                     # Custom mapping logic
│   ├── Interfaces/                          # Application service interfaces
│   └── Services/                            # Core business logic implementations
├── CsKmsBackend.Domain/                     # Domain models and entities
│   └── Models/                              # Core domain entities (User, Department, etc.)
│       └── Enums/                           # Enum types used across the domain
├── CsKmsBackend.Infrastructure/             # Data access, EF Core, repositories
│   ├── Data/                                # DbContext and EF Core configurations
│   │   ├── Migrations/                      # EF Core migration files
│   │   └── Seed/                            # Database seeding logic
│   ├── DependencyInjection/                 # Registers services for DI container
│   ├── Middlewares/                         # Custom middleware (e.g., error handling)
│   └── Repositories/                        # Repository implementations
├── CsKmsBackend.Presentation/               # API project (controllers, startup)
│   ├── Controllers/                         # API controllers (REST endpoints)
│   ├── Properties/                          # LaunchSettings and project configs
│   ├── appsettings.json                     # Main configuration file
│   └── .env                                 # Environment variables file
└── README.md                                # Project documentation
```

**Key Folders Explained:**
- `DependencyInjection/`: Registers application services and dependencies for the DI container.
- `DTOs/`: Contains objects used for data transfer between layers, especially for API requests/responses.
- `Conversions/`: Contains custom mapping logic for transfroming DTOs to Model Entities.
- `Models/`: Represent the core domain model following DDD principles.
- `Data/`, `Migrations/`, `Repositories/`: Handle database context, schema migrations, and data access logic.
- `Controllers/`, `Middlewares/`: Define API endpoints, request/response processing, and cross-cutting concerns.
- `Seed/`: Contains logic for populating the database with initial or test data.


