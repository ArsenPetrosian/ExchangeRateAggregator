# ExchangeRateAggregator

## Description
ExchangeRateAggregator is a project designed to aggregate and manage currency exchange rates from various banks. It provides functionalities to view, update, edit, and search currency exchange rates. The application is built using .NET Core and follows a three-layered architecture (DAL-BLL-API).

## Technologies Used
BankExchangeRateAggregator utilizes the following technologies:
1. .NET Core: .NET Core is used as the framework for building cross-platform applications.
2. ASP.NET Core: ASP.NET Core is used for building web applications and APIs.
3. Entity Framework Core: Entity Framework Core is used as the Object-Relational Mapping (ORM) framework for database interaction.
4. PostgreSQL: PostgreSQL is used as the relational database management system for storing application data.
5. Docker: Docker is used for containerization, allowing the application to be deployed in lightweight, portable containers.
6. Swagger: Swagger is used for API documentation, providing an interactive interface for exploring and testing API endpoints.
7. ExchangeRate-API: ExchangeRate-API is used to fetch and update currency exchange rates automatically. It provides a simple and reliable way to access real-time currency data.

## Getting Started
To use the BankExchangeRateAggregator web application:
1. Clone the repository to your local machine.
2. Run docker-compose up to start the application in a Docker container.
3. Run dotnet ef database update to apply the database migrations and create the required tables in PostgreSQL.
4. Access the application using the provided URL.

## Main Currency
The main currency for ExchangeRateAggregator is AMD (Armenian Dram).

## Usage
Once the application is up and running, you can perform the following actions:
1. View currency exchange rates from various banks.
2. Update currency exchange rates automatically using the ExchangeRate-API.
3. Edit existing exchange rates.
4. Search for specific currencies by name.

## Contributors
- Arsen Petrosian

License
This project is licensed under the MIT License (see LICENSE).
