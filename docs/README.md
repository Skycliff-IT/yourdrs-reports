# Vertical-Slice-Architecture

[![.NET Build & Test](https://github.com/Amitpnk/Vertical-Slice-Architecture/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Amitpnk/Vertical-Slice-Architecture/actions/workflows/dotnet.yml)

A clean, modular .NET 9 template using **Vertical Slice Architecture**—ideal for building scalable, maintainable APIs with CQRS, Mediator, FluentValidation, and EF Core/Dapper.

## ✅ Features

- Vertical slice structure per use case (CQRS)
- Minimal API with Carter for clean endpoint definitions
- Mediator Design Pattern for command/query dispatching
- FluentValidation for request validation
- EF Core-based persistence layer
- Docker and Docker Compose support for easy deployment
- Testable, decoupled design

## 🚀 Getting Started

Follow these steps to set up and run the project:

### 1. Clone the Repository

```bash
git clone https://github.com/Amitpnk/Vertical-Slice-Architecture.git
cd Vertical-Slice-Architecture
```

### 2. Build and Run with Docker

To build and start the containers:

```bash
docker-compose up --build
```

To run the containers in detached mode:

```bash
docker-compose up -d
```

> **Note:**  
> Building the Docker image manually is not usually required, but if needed, use:
>
> ```bash
> docker build -f src/VA.API/Dockerfile -t va-api .
> ```

### 3. Rebuild and Restart Containers

If you need to rebuild and restart the containers (for example, after making changes):

```bash
docker-compose down -v
docker-compose up --build
```

### 4. Open the Application in Your Browser

Once the containers are running, open your browser and navigate to:

```
http://localhost:5000
```

You should see the API running. Adjust the port if you have changed it in the Docker configuration.

## 📄 License

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/Amitpnk/Vertical-Slice-Architecture/blob/main/LICENSE)

See the LICENSE file for details.

## 📬 Contact

Having issues or need help getting started? Email amit.naik8103@gmail.com or [raise a bug or feature request](https://github.com/Amitpnk/Vertical-Slice-Architecture/issues/new). Always happy to help.
