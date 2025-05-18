# Vertical-Slice-Architecture

A clean and modular .NET 9 template using **Vertical Slice Architecture**. Ideal for building scalable, maintainable APIs with CQRS, Mediator DP, FluentValidation, and EF Core/Dapper.

## 🧱 Project Structure
```
src/
├── VA.Api           // API project (Minimal API)
├── VA.Application   // Commands, Queries, Handlers
├── VA.Domain        // Entities, ValueObjects
├── VA.Infrastructure// Data access, external integrations
└── VA.Shared        // Shared models/utilities
tests/
└── VA.Tests         // Unit & integration tests
```

## ✅ Features
- Vertical slice structure per use case (CQRS)
- Mediator Design Pattern for command/query dispatching
- FluentValidation for request validation
- EF Core-based persistence layer
- Testable, decoupled design

## 🚀 Getting Started
<!-- ```bash
dotnet restore
cd src/VA.Api
dotnet run
```

## 🧪 Run Tests
```bash
cd tests/VA.Tests
dotnet test
``` -->

## 🔧 Roadmap
<!-- - [ ] Add more slices (e.g., GetUser, UpdateUser)
- [ ] Add integration tests
- [ ] Add Dockerfile & CI pipeline -->

## 📄 License
MIT