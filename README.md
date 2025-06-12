
# 🐾 Pet Insurance Quoting Platform

A minimal multi-project ASP.NET 8 solution to generate real-time pet insurance quotes using Vue 3 and REST APIs.

---

## 🧩 Solution Structure

| Project                | Description                                                                 |
|------------------------|-----------------------------------------------------------------------------|
| `PetInsurance.API`     | ASP.NET Web API project for quote calculation and policy handling            |
| `PetInsurance.Shared`  | Shared models and utilities (used across API, Web, and Tests)                |
| `PetInsurance.Tests`   | Unit tests for services and calculators                                      |
| `PetInsurance.Web`     | ASP.NET MVC front-end with Vue 3 for quoting                                |

---

## 💡 Features

- MVC-based UI for quote form and policy listing
- Vue 3 component for reactive quote calculation
- REST API endpoints (e.g., `POST /api/quote`)
- In-memory policy storage
- Docker support for the full solution
- Clean architecture: Controllers → Services → Models (in `Shared`)
- JWT Auth support (optional)
- Swagger for API testing

---

## 🚀 Getting Started

### 🔧 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js](https://nodejs.org/) (for building Vue component)
- [Docker](https://www.docker.com/) (optional, for containerized deployment)

### 🛠️ Build & Run

```bash
git clone https://github.com/pet-insurer/pet-quote-mvc-test.git
cd pet-quote-mvc-test

# Build all projects
dotnet build

# Run API and Web projects in parallel
dotnet run --project PetInsurance.API
# In a new terminal
dotnet run --project PetInsurance.Web
```

Make sure that `PetInsurance.Web` is configured to call the correct API base URL (e.g., `https://localhost:44370`).

---

## 🧪 Running Tests

```bash
dotnet test PetInsurance.Tests
```

---
