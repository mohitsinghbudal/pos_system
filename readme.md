# POS System

A Point of Sale (POS) backend system built using **ASP.NET Core Web API, Entity Framework Core, SQL Server, and Dapper**.

The project follows a layered architecture to separate API endpoints, business logic, and database-related operations.

---

## 🚀 Tech Stack

* **C#**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **Dapper**
* **Swagger / OpenAPI**
* **JWT Authentication**
* **Git / GitHub**

---

## 📁 Project Structure

```text
POS.System/
│
├── POS.system/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Program.cs
│   ├── appsettings.json
│   └── POS.system.csproj
│
├── POS.DataLayer/
│   ├── Models/
│   │   ├── User.cs
│   │   ├── Role.cs
│   │   ├── Category.cs
│   │   ├── SubCategory.cs
│   │   ├── InventoryItem.cs
│   │   ├── Bill.cs
│   │   └── PaymentType.cs
│   │
│   ├── Migrations/
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

## 🏗️ Architecture

The application follows a layered architecture:

```text
Client
   │
   ▼
Controller
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

### API Layer

Responsible for:

* HTTP requests and responses
* Controllers
* DTOs
* API validation
* Swagger/OpenAPI

### Service Layer

Responsible for:

* Business logic
* Processing requests
* Calling the data layer
* Keeping controllers lightweight

### Data Layer

Responsible for:

* Database entities
* `DbContext`
* Entity Framework Core configuration
* Database migrations
* Stored procedures
* Database access

---

## 🗄️ Database

The project uses **Microsoft SQL Server** as the database.

### Current Entities

#### Role

Stores application roles.

```text
Role
├── Id
└── RoleName
```

#### User

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
└── RoleId
```

`RoleId` is a foreign key referencing the `Role` table.

Relationship:

```text
Role 1 ─────────── * User
```

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

Audit fields reference users who created or updated the record.

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

Stores products/items available in the POS system.

The inventory design also supports units such as:

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

The system supports different payment methods, including:

* Cash
* Khalti
* eSewa

Online payment integration is planned to support request, response, and callback information.

---

## 🔐 Authentication

Authentication is being implemented using **JWT (JSON Web Tokens)**.

The authentication flow is planned around:

```text
Login
   │
   ▼
Validate Credentials
   │
   ▼
Generate JWT
   │
   ▼
Return Token
```

The system also uses role-based authorization.

Example roles:

```text
Admin
Waiter
Cashier
```

---

## 📝 DTOs

DTOs are kept separate from database entities.

For example:

```text
SignupDto
├── Email
├── Password
└── PhoneNo
```

The API receives the user's password through the DTO, while the database stores a hashed password through the `User` entity.

This prevents database entities from becoming the direct API contract.

---

## ⚙️ Stored Procedures

The project uses SQL Server stored procedures for selected database operations.

Stored procedures are maintained as separate `.sql` files:

```text
POS.DataLayer/
└── StoredProcedures/
    ├── pos_login.sql
    └── pos_signup.sql
```

### Current procedures

#### `pos_signup`

Creates a new user in the `Users` table.

#### `pos_login`

Handles user login-related database operations.

Stored procedures are deployed through **Entity Framework Core migrations**.

Example migration approach:

```csharp
var sqlScript = File.ReadAllText(
    @"..\POS.DataLayer\StoredProcedures\pos_signup.sql"
);

migrationBuilder.Sql(sqlScript);
```

This allows the stored procedures to be version-controlled together with the application.

---

## 🔄 Database Migrations

Entity Framework Core migrations are used to manage database schema changes.

Typical commands:

```bash
dotnet ef migrations add MigrationName \
    --project POS.DataLayer \
    --startup-project POS.system \
    --context POSDbContext
```

Update the database:

```bash
dotnet ef database update \
    --project POS.DataLayer \
    --startup-project POS.system \
    --context POSDbContext
```

Migrations are committed to Git because they are part of the database version history.

---

## 🔗 Entity Relationships

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

---

## 🧪 API Documentation

Swagger/OpenAPI is configured for API testing and documentation.

When the application is running in development mode, Swagger can be used to:

* View available endpoints
* Send HTTP requests
* Inspect request/response models
* Test authentication-protected APIs

---

## 🔒 Configuration & Security

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
* JWT secrets
* API keys
* Other environment-specific configuration

An example configuration can be provided using:

```text
appsettings.Example.json
```

---

## 📦 Git

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

## 🛠️ Getting Started

### 1. Clone the repository

```bash
git clone <repository-url>
```

### 2. Open the solution

Open:

```text
POS.system.slnx
```

using Visual Studio.

### 3. Configure the database

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

### 4. Apply migrations

```bash
dotnet ef database update \
    --project POS.DataLayer \
    --startup-project POS.system \
    --context POSDbContext
```

### 5. Run the API

```bash
dotnet run
```

### 6. Open Swagger

Use the Swagger URL displayed by ASP.NET Core when the application starts.

---

## 📌 Current Development Status

### Completed / In Progress

* [x] ASP.NET Core Web API setup
* [x] Layered project structure
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
* [x] DTO structure
* [x] EF Core migrations
* [x] Stored procedure integration
* [x] `pos_login` procedure
* [x] `pos_signup` procedure
* [x] Git repository setup
* [ ] Complete authentication flow
* [ ] Complete role-based authorization
* [ ] Complete inventory management
* [ ] Complete billing workflow
* [ ] Complete payment integration
* [ ] Complete POS transaction workflow
* [ ] Unit and integration testing

---

## 🔮 Future Improvements

Planned improvements include:

* Complete JWT authentication
* Role-based authorization
* Product management
* Inventory management
* Stock tracking
* Billing and invoice generation
* Payment processing
* Khalti integration
* eSewa integration
* Transaction management
* Validation and error handling
* Logging
* Unit testing
* Integration testing
* API versioning
* Production deployment

---

## 👨‍💻 Author

**Mohit Singh Budal**

Computer Engineering Graduate
.NET Backend Developer



## 📄 License

This project is licensed under the **MIT License**.

See the [LICENSE](LICENSE) file for the full license text.
