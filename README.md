# CS-KMS-Backend

## Description

This is the backend project for CS-KMS.

### Features
* **User Roles & Permissions** - Super Admin, Department Admin, Staff (with multi-department enrollment). 
* **Department & Category Management** - Super Admin creates departments; Department Admins define knowledge categories.
* **Knowledge Creation** - Staff can post knowledge with: WYSIWYG editor, file attachments, and visibility settings (Public/Private/Department-Restricted).
* **Search & Access Control** - Global search displays knowledge titles/descriptions; users can request access to private content.
* **Document Management** - Supports multiple file uploads (PDFs, Word, Excel, etc.).
* **Audit & Logging** - Tracks knowledge creation, edits, and access requests.

[Provide a brief description of your .NET project here. What does it do? What are its main features?]

## Getting Started

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
      ConnectionStrings__DefaultConnection=”Server=...;Database=...;User Id=...;Password=..." // Example for SQL Server
        ```
	    Or for user secrets:
       ```json
        "ConnectionStrings": {
          "DefaultConnection": "Server=...;Database=...;User Id=...;Password=..." // Example for SQL Server
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
    dotnet tool run dotnet-ef database update -p <prefix>.Infrastructure -s <prefix>.Presentation
    ```
    This command applies any pending Entity Framework Core migrations to your database, creating the database schema.  This will create the tables and relationships defined by the code.

6.  **Seed the Database (Optional):**

    ```bash
    dotnet run --seed
    ```
    If this doesn't work try this
    ```bash
    dotnet run --seed --project <prefix>.Presentattion
    ``` 
    This command runs the application with the `--seed` argument. This project has a seeding mechanism implemented (e.g., in `Program.cs` and a dedicated `SeedData` class) to populate the database with initial data (e.g., default users, configuration data).  If the application does not have a seed option, omit this step, or check the application's documentation.  If there is no seed option, the command may result in an error, or the application running normally.

7.  **Run the Application:**

    ```bash
    dotnet run
    ```
    This command builds and runs the .NET application.  You should then be able to access the application in your web browser at the address specified in the application's configuration (usually `http://localhost:xxxx`).



