# JiraApp

# 🚀 Task Manager API (.NET Core)

A simple **Task Management REST API** built using **.NET Core**, following **Clean Architecture principles**.
This project demonstrates how to design scalable, testable backend systems similar to Jira/Trello (simplified).

---

## 📌 Features

* ✅ Create tasks
* ✅ Get all tasks
* ✅ Get task by ID
* ✅ Update tasks
* ✅ Delete tasks
* ✅ Service-level validation
* ✅ Swagger API testing
* ✅ Unit testing with NUnit & Moq

---

## 🧱 Architecture

This project follows **Clean Architecture**, separating concerns into multiple layers:

```
TaskManager
│
├── TaskManager.API           → API Layer (Controllers, Swagger)
├── TaskManager.Application   → Business Logic, Services, Interfaces
├── TaskManager.Domain        → Core Entities
├── TaskManager.Infrastructure→ Data Access (In-Memory / Azure-ready)
├── TaskManager.Tests         → Unit Tests
```

---

## 🧠 Design Principles

* Separation of concerns
* Dependency inversion
* Testable business logic
* Scalable architecture

---

## 📊 Task Model

Each task contains:

* `Id` (GUID)
* `Name` (string)
* `Description` (string)
* `Deadline` (DateTime)
* `Status` (Todo / InProgress / Done)
* `CreatedAt` (DateTime)

---

## ⚙️ Tech Stack

* **.NET Core (Web API)**
* **Azure SDK (Table Storage - optional)**
* **NUnit** (Unit Testing)
* **Moq** (Mocking)
* **Swagger (OpenAPI)**

---

## ▶️ Getting Started

### 1️⃣ Clone Repo

```bash
git clone <your-repo-url>
cd TaskManager
```

---

### 2️⃣ Build Solution

```bash
dotnet build
```

---

### 3️⃣ Run API

```bash
dotnet run --project TaskManager.API
```

---

### 4️⃣ Open Swagger

```
http://localhost:5034/swagger
```

---

## 🧪 API Endpoints

| Method | Endpoint          | Description     |
| ------ | ----------------- | --------------- |
| GET    | `/api/tasks`      | Get all tasks   |
| GET    | `/api/tasks/{id}` | Get task by ID  |
| POST   | `/api/tasks`      | Create new task |
| PUT    | `/api/tasks`      | Update task     |
| DELETE | `/api/tasks/{id}` | Delete task     |

---

## 🧪 Sample Request

### Create Task

```json
{
  "name": "Build Task Manager",
  "description": "Implement CRUD operations",
  "deadline": "2026-04-10T10:00:00Z",
  "status": "Todo"
}
```

---

## 🧪 Running Tests

```bash
dotnet test
```

---

## ✅ Validation Rules

* Task name is required
* Deadline must be in the future
* Status must be one of:

  * Todo
  * InProgress
  * Done

---

## 🧠 What This Project Demonstrates

* Clean Architecture implementation
* Service-layer validation
* Repository pattern
* Unit testing with mocking
* REST API design

---

## ⚠️ Current Limitations

* Uses in-memory storage (data resets on restart)
* No authentication/authorization
* Domain models exposed directly (DTOs can be added for production)

---

## 🚀 Future Enhancements

* Add DTO layer for API contracts
* Integrate database (SQL/Azure)
* Add JWT authentication
* Add logging (Serilog)
* Add global exception handling
* Add CI/CD pipeline

---

## 👨‍💻 Author

**Swati Jain**


