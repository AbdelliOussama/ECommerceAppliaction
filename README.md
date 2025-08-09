# ECommerceAppliaction

ECommerceApp is a modern, scalable e-commerce backend built with ASP.NET Core (.NET 8, C# 12). It provides robust APIs for customer management, product catalog, shopping cart, order processing, payments, and more.
The architecture is designed for maintainability, security, and extensibility, following clean coding principles.

Tech Stack
Backend: ASP.NET Core (.NET 8, C# 12)

ORM: Entity Framework Core

Database: SQL Server (configurable)

Mapping: AutoMapper

API: RESTful (JSON)

Validation: Data Annotations

Features
Customer registration, authentication, and profile management

Product catalog with categories

Shopping cart and checkout

Order creation, cancellation, and refund

Payment processing



| Endpoint                               | Method | Description                | Request Body / Params     | Response Type                          |
| -------------------------------------- | ------ | -------------------------- | ------------------------- | -------------------------------------- |
| `/api/Customers/CreateCustomer`        | POST   | Register a new customer    | `CustomerRegistrationDTO` | `ApiResponse<CustomerResponseDTO>`     |
| `/api/Customers/LoginCustomer`         | POST   | Customer login             | `LoginDTO`                | `ApiResponse<LoginResponseDTO>`        |
| `/api/Customers/GetCustomerDetails`    | GET    | Get customer details by ID | `customerId` (query)      | `ApiResponse<CustomerResponseDTO>`     |
| `/api/Customers/UpdateCustomerDetails` | PUT    | Update customer profile    | `CustomerUpdateDTO`       | `ApiResponse<ConfirmationResponseDTO>` |
| `/api/Customers/DeleteCustomer`        | DELETE | Delete customer by ID      | `customerId` (query)      | `ApiResponse<ConfirmationResponseDTO>` |
| `/api/Customers/ChangePassword`        | POST   | Change customer password   | `ChangePasswordDTO`       | `ApiResponse<ConfirmationResponseDTO>` |

DTOs
CustomerRegistrationDTO: FirstName, LastName, Email, PhoneNumber, DateOfBirth, Password

LoginDTO: Email, Password

CustomerUpdateDTO: CustomerId, FirstName, LastName, Email, PhoneNumber, DateOfBirth

ChangePasswordDTO: CustomerId, CurrentPassword, NewPassword, ConfirmNewPassword


Standardized API Response
json
Copy
Edit
{
  "statusCode": 200,
  "success": true,
  "data": {},
  "errors": []
}

## Setup & Installation

# 1. Clone repository
git clone https://github.com/yourusername/ECommerceApp.git

# 2. Update connection string in appsettings.json

# 3. Restore dependencies
dotnet restore

# 4. Apply migrations
dotnet ef database update

# 5. Run application
dotnet run

# Contributing 
Contributions are welcome! Fork the repo, create a new branch, and submit a pull request.


Feedback and reviews

Secure password management
