# Online Learning Platform API

A REST API developed with .NET 9, featuring JWT-based authentication and role-based authorization.

##  Project Description

This project is a REST API developed for an online learning platform. Users can enroll in courses, and instructors can create courses and lessons.

### Features

- ✅ JWT Authentication (Token-based authentication)
- ✅ Role-based Authorization (Admin/User roles)
- ✅ Soft Delete (Records are not physically deleted)
- ✅ Serilog Logging (Console + File)
- ✅ Swagger/OpenAPI Documentation
- ✅ Minimal API + Controller-based API
- ✅ Repository Pattern + Service Layer

### Technologies

- .NET 9
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- Serilog
- Swagger/OpenAPI

---

##  Mimari Diagram

<img width="1024" height="559" alt="image" src="https://github.com/user-attachments/assets/0521daba-e7f4-4944-8061-2f243989c5c4" />

##  Endpoint List

### 🔐 Auth (Authentication)

| Method | Endpoint          | Description                  | Authorization |
| ------ | ----------------- | ---------------------------- | ------------- |
| POST   | `/api/auth/login` | User login and get JWT token | Public        |

###  Users

| Method | Endpoint          | Description               | Authorization |
| ------ | ----------------- | ------------------------- | ------------- |
| GET    | `/api/users`      | List all users            | Auth          |
| GET    | `/api/users/{id}` | Get user details          | Auth          |
| POST   | `/api/users`      | Create new user           | Auth          |
| PUT    | `/api/users/{id}` | Update user               | Auth          |
| DELETE | `/api/users/{id}` | Delete user (soft delete) | Auth          |

###  Courses

| Method | Endpoint            | Description        | Authorization |
| ------ | ------------------- | ------------------ | ------------- |
| GET    | `/api/courses`      | List all courses   | Public        |
| GET    | `/api/courses/{id}` | Get course details | Public        |
| POST   | `/api/courses`      | Create new course  | Admin         |
| PUT    | `/api/courses/{id}` | Update course      | Admin         |
| DELETE | `/api/courses/{id}` | Delete course      | Admin         |

###  Lessons

| Method | Endpoint                         | Description           | Authorization |
| ------ | -------------------------------- | --------------------- | ------------- |
| GET    | `/api/lessons`                   | List all lessons      | Public        |
| GET    | `/api/lessons/{id}`              | Get lesson details    | Public        |
| GET    | `/api/lessons/course/{courseId}` | Get lessons by course | Public        |
| POST   | `/api/lessons`                   | Create new lesson     | Auth          |
| PUT    | `/api/lessons/{id}`              | Update lesson         | Auth          |
| DELETE | `/api/lessons/{id}`              | Delete lesson         | Auth          |

###  Enrollments

| Method | Endpoint                | Description            | Authorization |
| ------ | ----------------------- | ---------------------- | ------------- |
| GET    | `/api/enrollments`      | List all enrollments   | Auth          |
| GET    | `/api/enrollments/{id}` | Get enrollment details | Auth          |
| POST   | `/api/enrollments`      | Enroll in a course     | Auth          |
| DELETE | `/api/enrollments/{id}` | Delete enrollment      | Auth          |

###  Minimal API (v2)

All endpoints above are also available with `/api/v2/...` prefix.

---

##  API Response Examples

### Successful Login Response

```json
{
  "success": true,
  "message": "Login successful",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "email": "admin@gmail.com",
    "fullName": "Admin User",
    "role": "Admin",
    "expiresAt": "2024-12-31T23:00:00Z"
  }
}
```

### Course List Response

```json
{
  "success": true,
  "message": "Operation successful",
  "data": [
    {
      "id": 1,
      "title": "C# Programming",
      "description": "Beginner level C# course",
      "price": 199.99,
      "instructorId": 1,
      "createdAt": "2024-01-01T00:00:00Z"
    }
  ]
}
```

### Error Response

```json
{
  "success": false,
  "message": "Invalid email or password",
  "data": null
}
```

### Not Found Response

```json
{
  "success": false,
  "message": "User not found",
  "data": null
}
```

---

##  Installation Instructions

### Requirements

- .NET 9 SDK
- SQL Server (LocalDB or Express)
- Visual Studio 2022 / VS Code

### Step 1: Clone the Project

```bash
git clone https://github.com/UmitKaradeniz/online-learning-dotnet-project.git
cd online-learning-dotnet-project
```

### Step 2: Configure Database Connection

Edit the connection string in `dotnet-project/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=OnlineLearningDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Step 3: Create Database

```bash
cd dotnet-project
dotnet ef database update
```

### Step 4: Run the Application

```bash
dotnet run
```

### Step 5: Access Swagger

Open in your browser: `http://localhost:5181/swagger`

---

## 🔑 Default Users

| Email             | Password      | Role  |
| ----------------- | ------------- | ----- |
| admin@gmail.com   | yalovauni     | Admin |
| student@gmail.com | yalovastudent | User  |

---

## 📁 Project Structure

```
dotnet-project/
├── Controllers/           # API Controllers
├── Data/
│   ├── AppDbContext.cs    # EF Core DbContext
│   └── Repositories/      # Repository Pattern
├── DTOs/                  # Data Transfer Objects
├── Entities/              # Domain Entities
├── Extensions/            # Minimal API Endpoints
├── Services/              # Business Logic
│   └── Interfaces/
├── Migrations/            # EF Core Migrations
└── logs/                  # Serilog Log Files
```

---

##  License

This project was developed for educational purposes.
