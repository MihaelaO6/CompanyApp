# CompanyApp
# CompanyApp API Documentation
This project provides a RESTful API for managing `Company`, `Country`, and `Contact` entities. It supports CRUD operations, with services and controllers unit-tested using Moq and XUnit.

---
## Setup

-- Before you start, ensure you have the following installed:
.net8, SQLServer or LocalDB, Visual Studio

-- Add necessary NuGet packages:
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

-- For testing you need to install this test dependencies:
dotnet add package Moq
dotnet add package Xunit
dotnet add package Microsoft.AspNetCore.Mvc.Testing

-- Logging is configured through Microsoft.Extensions.Logging and you can modify the logging level in appsettings.json

-- Connection string with db is added in appsettings.json

-- After cloning the repository, restore the necessary packages:
dotnet restore

-- Run migration to create db and update-database
dotnet ef migrations add InitialMigration
dotnet ef database update

-- To run the application locally use:
dotnet run

## API Endpoints
-- Access the API: 
The API will be available at https://localhost:xxxx (the port you have configured)
### Company API
- GET /api/companies: 
  Retrieve all companies
- GET /api/companies/{id}:
  Retrieve a company by its ID
- POST /api/companies: 
  Add a new company
- PUT /api/companies/{id}:  
  Update an existing company
- DELETE /api/companies/{id}: 
  Delete a company by its ID

### Country API
- GET /api/countries: 
  Retrieve all countries
- GET /api/countries/{id}: 
  Retrieve a country by its ID
- POST /api/countries: 
  Add a new country
- PUT /api/countries/{id}: 
  Update an existing country
- DELETE /api/countries/{id}: 
  Delete a country

### Contact API
- GET /api/contacts: 
  Retrieve all contacts
- GET /api/contacts/{id}: 
  Retrieve a contact by its ID
- GET /api/contacts/paged?{pageNumber}:
  Get paginated contacts for a specific page number
- POST /api/contacts:
  Add a new contact
- PUT /api/contacts/{id}: 
  Update an existing contact
- DELETE /api/contacts/{id}: 
  Delete a contact
- GET /api/contacts/details:
  Get all contacts with additional details
- GET /api/contacts/filter?countryId={countryId}&companyId={companyId}: 
  Filter contacts based on optional countryId and companyId query parameters

## Project Structure
The project is organized into several layers to maintain clean separation of concerns:

/CompanyApp
  /API/Controllers       - Contains the API controllers for handling HTTP requests
  /Domain                - Contains the models
  /Infrastructure        - Contains the EF Core database context, repository interfaces and implementations
  /Application/Services  - Contains business logic services for interacting with the database
  /Tests                 - Contains Unit tests for services and controllers using Moq and XUnit
  /Migrations            - Contains EF Core database migrations for schema changes
  appsettings.json       - Contains database connection strings, logging etc.
  Program.cs             - Application entry point, configuration and setup of dependency injection


## Running tests

The project includes unit tests for the services and controllers using Moq for mocking dependencies and XUnit for running the tests

-- To run the unit tests, use:
dotnet test

-- The test results will be displayed in the console 


