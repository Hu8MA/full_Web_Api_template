# Full Web API Template

This project is a comprehensive template for building a full-featured Web API using DotNet 6. It includes JWT authentication, RESTful API architectural design, Entity Framework, Identity User (IdentityDbContext) with SQL Server DB, global error handling, and the repository pattern design.

## Features

- **JWT Authentication:** Secure your API with JSON Web Token (JWT) authentication.
- **RESTful API Design:** Follow best practices for designing RESTful APIs.
- **Entity Framework:** Use Entity Framework for database interactions.
- **Identity User (IdentityDbContext):** Manage user authentication and authorization with IdentityDbContext and SQL Server DB.
- **Global Error Handling:** Implement global error handling to manage exceptions consistently.
- **Repository Pattern Design:** Use the repository pattern for cleaner and more maintainable code.

## Getting Started

### Prerequisites

- .NET 6 SDK
- SQL Server

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/Hu8MA/full_Web_Api_template.git
    ```

2. Navigate to the project directory:
    ```bash
    cd full_Web_Api_template
    ```

3. Restore the project dependencies:
    ```bash
    dotnet restore
    ```

4. Update the connection string in `appsettings.json` to point to your SQL Server instance.

5. Apply the database migrations:
    ```bash
    dotnet ef database update
    ```

6. Run the project:
    ```bash
    dotnet run
    ```

## Usage

This template provides a foundation for building robust and secure Web APIs. You can extend it by adding your own controllers, services, and repositories.

## Contributing

Contributions are welcome! Please fork the repository, create a new branch, and submit a pull request.

1. Fork the repository.
2. Create a new branch:
    ```bash
    git checkout -b feature-name
    ```
3. Make your changes and commit them:
    ```bash
    git commit -m 'Add some feature'
    ```
4. Push to the branch:
    ```bash
    git push origin feature-name
    ```
5. Open a pull request.

Please ensure your code adheres to the project's coding standards and includes appropriate tests.

## License

This project is licensed under the MIT License

## Acknowledgments

Developed by Hu8MA(Hussein Mahdi) . Special thanks to all contributors and the open-source community for their support and collaboration.
 
