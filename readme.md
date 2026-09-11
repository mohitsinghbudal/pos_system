# POS System

A Point of Sale (POS) backend system built using **ASP.NET Core Web API, Entity Framework Core, SQL Server, and Dapper**.

The project follows a layered architecture to separate API endpoints, business logic, middleware, and database-related operations.

---

## 🚀 Tech Stack

* **C#**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **Dapper**
* **Swagger / OpenAPI**
* **JWT Authentication**
* **BCrypt**
* **Git / GitHub**

---

## 📁 Project Structure

```text
POS.System/
│
├── POS.system/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Middleware/
│   │   └── GlobalExceptionMiddleware.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── POS.system.csproj
│
├── POS.DataLayer/
│   ├── Models/
│   │   ├── User.cs
│   │   ├── Role.cs
│   │   ├── RefreshToken.cs
│   │   ├── Category.cs
│   │   ├── SubCategory.cs
│   │   ├── InventoryItem.cs
│   │   ├── Bill.cs
│   │   └── PaymentType.cs
│   │
│   ├── DataAccess/
│   │   └── JwtDAL/
│   │       └── JwtDll.cs
│   │
│   ├── Migrations/
│   │
│   ├── StoredProcedures/
│   │   ├── pos_login.sql
│   │   └── pos_signup.sql
│   │
│   ├── POSDbContext.cs
│   └── POS.DataLayer.csproj
│
├── Service.Solution/
│   ├── User/
│   └── Service.Solution.csproj
│
├── .gitignore
└── POS.system.slnx
```

---

# 🏗️ Architecture

The application follows a layered architecture with an ASP.NET Core middleware pipeline:

```text
Client
   │
   ▼
Middleware Pipeline
   │
   ├── Global Exception Handling
   ├── Authentication
   └── Authorization
   │
   ▼
Controller / API Layer
   │
   ▼
Service Layer
   │
   ├── Business Logic
   │
   ▼
Data Layer
   │
   ├── Entity Framework Core
   ├── Dapper
   └── Stored Procedures
   │
   ▼
SQL Server
```

---

# ⚙️ Middleware

ASP.NET Core middleware is used to handle cross-cutting concerns before requests reach the controllers.

### Current middleware responsibilities

* Global exception handling
* JWT authentication
* Authorization pipeline
* Centralized request processing

### Global Exception Handling

The application uses a centralized exception-handling approach instead of placing repetitive `try/catch` blocks inside every controller or service method.

For example, service-layer exceptions such as:

```csharp
throw new Exception("Invalid or expired refresh token");
```

are allowed to propagate through the request pipeline.

The global exception middleware catches the exception and converts it into an appropriate API response.

This keeps controllers and services focused on their actual responsibilities.

### JWT Authentication Middleware

ASP.NET Core's JWT Bearer authentication middleware validates access tokens supplied through the HTTP `Authorization` header.

The general flow is:

```text
HTTP Request
     │
     ▼
JWT Authentication Middleware
     │
     ├── Read Bearer Token
     ├── Validate Token
     ├── Validate Signature
     ├── Validate Expiration
     └── Create User Claims
     │
     ▼
Authorization
     │
     ▼
Controller
```

Protected endpoints can then use authorization requirements such as:

```csharp
[Authorize]
```

---

# 🔌 API Layer

The API layer is responsible for:

* HTTP requests and responses
* Controllers
* DTOs
* API endpoint definitions
* Swagger/OpenAPI integration
* Receiving and returning API data

Controllers are kept lightweight and delegate business operations to the service layer.

---

# 🧠 Service Layer

The service layer is responsible for:

* Business logic
* Processing requests
* Authentication logic
* Password verification
* Token generation
* Refresh-token rotation
* User status validation
* Calling the data layer
* Keeping controllers lightweight

---

# 🗄️ Data Layer

The data layer is responsible for:

* Database entities
* `DbContext`
* Entity Framework Core configuration
* Database migrations
* Stored procedures
* Database access
* Refresh-token persistence

The project uses both **Entity Framework Core** and **Dapper/stored procedures** depending on the database operation.

---

# 🗄️ Database

The project uses **Microsoft SQL Server** as the database.

## Current Entities

### Role

Stores application roles.

```text
Role
├── Id
├── RoleName
├── IsActive
├── AddedBy
├── AddedOn
├── DeletedBy
└── DeletedOn
```

---

### User

Stores system users.

```text
User
├── Id
├── Name
├── Email
├── PasswordHash
├── PhoneNo
├── CreatedAt
├── IsActive
├── RoleId
├── RoleUpdatedAt
├── RoleUpdatedBy
├── DeletedAt
└── DeletedBy
```

`RoleId` is a foreign key referencing the `Role` table.

Relationship:

```text
Role 1 ─────────── * User
```

The relationship uses restricted delete behavior to prevent unintended cascading deletes.

---

### RefreshToken

Stores refresh tokens used for JWT token renewal.

```text
RefreshToken
├── Id
├── Token
├── UserId
├── CreatedAt
├── ExpiresAt
├── IsRevoked
├── RevokedAt
└── ReplacedByToken
```

Relationship:

```text
User 1 ─────────── * RefreshToken
```

A user can have multiple active refresh tokens, allowing multiple sessions/devices.

Refresh tokens contain expiration and revocation information so that expired or revoked tokens cannot be reused.

---

### Category

Stores product categories.

```text
Category
├── Id
├── CategoryName
├── CreatedAt
├── CreatedBy
├── UpdatedAt
└── UpdatedBy
```

---

### SubCategory

Stores subcategories belonging to a category.

```text
SubCategory
├── Id
├── SubCategoryName
├── CategoryId
├── CreatedAt
├── CreatedBy
├── UpdatedAt
└── UpdatedBy
```

Relationship:

```text
Category 1 ─────────── * SubCategory
```

---

### Inventory Item

Stores products/items intended to be available in the POS system.

The inventory design supports units such as:

```text
Box
Piece
Kg
Liter
```

For example:

```text
1 Box = 24 Pieces
```

---

### Bill

Stores billing information.

```text
Bill
├── Id
├── BillNumber
├── BillDate
├── CustomerId
├── SubTotal
├── Discount
├── Tax
├── TotalAmount
├── CreatedAt
└── CreatedBy
```

---

### Payment Type

The system is designed to support different payment methods, including:

* Cash
* Khalti
* eSewa

Online payment integration is planned to support request, response, and callback information.

---

# 🔐 Authentication

The authentication system uses **JWT access tokens and refresh tokens**.

The current authentication flow is:

```text
Login
   │
   ▼
Validate Credentials
   │
   ▼
Check User Exists
   │
   ▼
Check User Is Active
   │
   ▼
Verify BCrypt Password
   │
   ▼
Generate Access Token
   │
   ▼
Generate Refresh Token
   │
   ▼
Store Refresh Token
   │
   ▼
Return Access Token + Refresh Token
```

The authentication service currently supports:

* User signup
* BCrypt password hashing
* User login
* JWT access-token generation
* Refresh-token generation
* Refresh-token validation
* Refresh-token rotation
* Refresh-token revocation
* Logout
* Multiple active sessions
* Active/inactive user validation

---

# 🔑 Password Hashing

Passwords are never stored as plain text.

The application uses **BCrypt** for password hashing.

During signup:

```csharp
var hashedPassword = BCrypt.Net.BCrypt.HashPassword(
    dto.Password,
    workFactor: 10
);
```

During login, the supplied password is verified against the stored hash:

```csharp
var isPasswordValid =
    BCrypt.Net.BCrypt.Verify(
        dto.Password,
        user.PasswordHash
    );
```

---

# 🎟️ JWT Access Token

After successful authentication, the server generates a JWT access token.

The access token contains claims representing information about the authenticated user, including:

* User ID
* Name
* Email
* Role
* Phone number

JWT configuration is stored in application configuration.

Example:

```json
{
  "Jwt": {
    "AccessTokenMinutes": "...",
    "RefreshTokenDays": "..."
  }
}
```

The JWT secret key is kept in configuration and should never be committed to the repository.

---

# 🔄 Refresh Tokens

Refresh tokens are generated using a cryptographically secure random number generator.

```csharp
var refreshTokenValue = Convert.ToBase64String(
    RandomNumberGenerator.GetBytes(64)
);
```

The generated refresh token is stored in SQL Server and associated with the authenticated user.

---

## Refresh Token Validation

A refresh token is considered valid only when:

```text
Token exists
     AND
Token is not revoked
     AND
Token has not expired
```

The current data-layer query performs these checks:

```csharp
.Where(x =>
    x.Token == token &&
    !x.IsRevoked &&
    x.ExpiresAt > DateTime.UtcNow)
```

If the refresh token is missing, expired, or revoked, the refresh request is rejected.

---

# 🔁 Refresh Token Rotation

The system implements **refresh-token rotation**.

When a valid refresh token is used:

```text
Old Refresh Token
       │
       ▼
Validate Token
       │
       ▼
Check User
       │
       ▼
Generate New Access Token
       │
       ▼
Generate New Refresh Token
       │
       ▼
Revoke Old Refresh Token
       │
       ├── IsRevoked = true
       ├── RevokedAt = current UTC time
       └── ReplacedByToken = new refresh token
       │
       ▼
Store New Refresh Token
       │
       ▼
Return New Access Token + Refresh Token
```

The old refresh token becomes invalid after rotation.

The `ReplacedByToken` field keeps track of the token that replaced the old token.

---

# 🚪 Logout

Logout uses the refresh token supplied by the client.

The system:

1. Finds the refresh token.
2. Checks whether it exists.
3. Checks whether it has already been revoked.
4. Marks the token as revoked.
5. Stores the revocation timestamp.

Example:

```csharp
refreshToken.IsRevoked = true;
refreshToken.RevokedAt = DateTime.UtcNow;
```

After revocation, the refresh token cannot be used to obtain another access token.

---

# 👥 Multiple Login Sessions

The current implementation allows a user to have **multiple active refresh tokens**.

For example:

```text
User
 ├── Refresh Token A → PC
 ├── Refresh Token B → Mobile
 └── Refresh Token C → Laptop
```

Each refresh token can be independently revoked.

A global login limit or device/session management system has not yet been implemented.

---

# 🛡️ User Status Validation

The authentication flow checks whether a user account is active.

An inactive user cannot:

* Log in
* Refresh an existing session

Example:

```csharp
if (!user.IsActive)
    throw new Exception("User is not active");
```

---

# 📝 DTOs

DTOs are kept separate from database entities.

Current authentication DTOs include:

```text
SignupDTO
LoginReqDTO
LoginResDTO
RefreshTokenDto
RefreshTokenRequestDto
RefreshTokenResponseDto
```

For example:

```text
SignupDTO
├── Name
├── Email
├── Password
└── PhoneNo
```

The API receives the user's password through the DTO, while the database stores a BCrypt password hash through the `User` entity.

This keeps database entities separate from the API contract.

---

# ⚙️ Stored Procedures

The project uses SQL Server stored procedures for selected database operations.

Stored procedures are maintained as separate `.sql` files:

```text
POS.DataLayer/
└── StoredProcedures/
    ├── pos_login.sql
    └── pos_signup.sql
```

### Current Procedures

#### `pos_signup`

Creates a new user in the database.

#### `pos_login`

Handles user login-related database operations.

Stored procedures are version-controlled and integrated with the database deployment process through Entity Framework Core migrations.

Example migration approach:

```csharp
var sqlScript = File.ReadAllText(
    @"..\POS.DataLayer\StoredProcedures\pos_signup.sql"
);

migrationBuilder.Sql(sqlScript);
```

This allows stored procedures to be maintained alongside the application's database version history.

---

# 🔄 Database Migrations

Entity Framework Core migrations are used to manage database schema changes.

Create a migration:

```bash
dotnet ef migrations add MigrationName --project POS.DataLayer --startup-project POS.system --context POSDbContext
```

Update the database:

```bash
dotnet ef database update --project POS.DataLayer --startup-project POS.system --context POSDbContext
```

Migrations are committed to Git because they are part of the database version history.

---

# 🔗 Entity Relationships

The project uses Entity Framework Core relationships such as:

```csharp
entity.HasOne(u => u.Role)
      .WithMany()
      .HasForeignKey(u => u.RoleId)
      .OnDelete(DeleteBehavior.Restrict);
```

This defines:

* `User` has one `Role`
* `Role` can have many `User` records
* `RoleId` is the foreign key
* Deleting a role is restricted when users reference it

Refresh tokens are also associated with users:

```text
User 1 ─────────── * RefreshToken
```

---

# 🧪 API Documentation

Swagger/OpenAPI is configured for API testing and documentation.

Swagger can be used to:

* View available endpoints
* Send HTTP requests
* Inspect request/response models
* Test authentication-protected APIs
* Authorize requests using JWT access tokens

---

# 🔒 Configuration & Security

Sensitive configuration should **not** be committed to Git.

The following files are ignored:

```text
appsettings.json
appsettings.*.json
.env
.env.*
```

These files may contain:

* SQL Server connection strings
* JWT secret keys
* API keys
* Other environment-specific configuration

An example configuration can be provided using:

```text
appsettings.Example.json
```

---

# 📦 Git

The project uses Git for version control.

The repository ignores common .NET and Visual Studio generated files:

```text
.vs/
.vscode/
bin/
obj/
.idea/
*.user
*.suo
```

Database migrations and stored procedures are intentionally tracked.

---

# 🛠️ Getting Started

## 1. Clone the repository

```bash
git clone <repository-url>
```

## 2. Open the solution

Open:

```text
POS.system.slnx
```

using Visual Studio.

## 3. Configure the database

Create your local:

```text
appsettings.json
```

and configure the SQL Server connection string.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

Configure JWT settings as well:

```json
{
  "Jwt": {
    "Key": "YOUR_JWT_SECRET",
    "AccessTokenMinutes": 15,
    "RefreshTokenDays": 7
  }
}
```

**Do not commit real secrets to Git.**

## 4. Apply migrations

```bash
dotnet ef database update --project POS.DataLayer --startup-project POS.system --context POSDbContext
```

## 5. Run the API

```bash
dotnet run
```

## 6. Open Swagger

Use the Swagger URL displayed by ASP.NET Core when the application starts.

---

# 🔄 Authentication API Flow

The current authentication workflow is:

```text
                    ┌──────────────┐
                    │    Signup    │
                    └──────┬───────┘
                           │
                           ▼
                    Hash Password
                           │
                           ▼
                    Store User
                           │
                           ▼
                    ┌──────────────┐
                    │    Login     │
                    └──────┬───────┘
                           │
                    Validate Credentials
                           │
                           ▼
              ┌──────────────────────────┐
              │ Access Token             │
              │ +                        │
              │ Refresh Token            │
              └────────────┬─────────────┘
                           │
                           ▼
                    Access Protected API
                           │
                           ▼
                    Access Token Expires
                           │
                           ▼
                    Refresh Endpoint
                           │
                           ▼
                 Validate Refresh Token
                           │
                           ▼
                  Revoke Old Token
                           │
                           ▼
                  Create New Token
                           │
                           ▼
             New Access + Refresh Token
                           │
                           ▼
                         Logout
                           │
                           ▼
                  Revoke Refresh Token
```

---

# 📌 Current Development Status

### Completed

* [x] ASP.NET Core Web API setup
* [x] Layered project structure
* [x] Middleware pipeline configuration
* [x] Global exception-handling middleware
* [x] JWT authentication middleware configuration
* [x] SQL Server integration
* [x] Entity Framework Core setup
* [x] `POSDbContext`
* [x] Role entity
* [x] User entity
* [x] Category entity
* [x] SubCategory entity
* [x] Inventory entity design
* [x] Bill entity design
* [x] Payment type design
* [x] Refresh Token entity
* [x] User–RefreshToken relationship
* [x] DTO structure
* [x] EF Core migrations
* [x] Stored procedure integration
* [x] `pos_login` procedure
* [x] `pos_signup` procedure
* [x] Git repository setup
* [x] User signup
* [x] BCrypt password hashing
* [x] User login
* [x] User active/inactive validation
* [x] JWT access-token generation
* [x] JWT claims
* [x] Secure refresh-token generation
* [x] Refresh-token database storage
* [x] Refresh-token validation
* [x] Refresh-token expiration check
* [x] Refresh-token revocation
* [x] Refresh-token rotation
* [x] Refresh-token replacement tracking
* [x] Logout using refresh-token revocation
* [x] Multiple active login sessions
* [x] Swagger/OpenAPI authentication testing

### In Progress / Planned

* [ ] Complete role-based authorization
* [ ] Complete inventory management
* [ ] Complete billing workflow
* [ ] Complete payment integration
* [ ] Complete POS transaction workflow
* [ ] Unit testing
* [ ] Integration testing

---

# 🔮 Future Improvements

Planned improvements include:

* Role-based authorization
* Permission-based authorization
* Product management
* Inventory management
* Stock tracking
* Billing and invoice generation
* Payment processing
* Khalti integration
* eSewa integration
* Transaction management
* Improved validation and error handling
* Structured exception types
* Structured logging
* Unit testing
* Integration testing
* API versioning
* Device/session management
* Logout from all devices
* Refresh-token reuse detection
* Production deployment

---

# 👨‍💻 Author

**Mohit Singh Budal**

Computer Engineering Graduate
.NET Backend Developer

---

# 📄 License

This project is licensed under the **MIT License**.

See the [LICENSE](LICENSE) file for the full license text.
