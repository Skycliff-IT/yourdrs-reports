# Vertical-Slice-Architecture

A clean and modular .NET 9 template using **Vertical Slice Architecture**. Ideal for building scalable, maintainable APIs with CQRS, Mediator DP, FluentValidation, and EF Core/Dapper.


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

1. **Clone the repository**
    ```bash
    git clone https://github.com/Amitpnk/Vertical-Slice-Architecture.git
    cd Vertical-Slice-Architecture
    ```

2. **Run with Docker**

    Build the Docker image and start the containers:

    ```bash
    docker build -f VA.API/Dockerfile -t va-api .
    docker-compose up --build
    ```

    To run the containers in detached mode, use:

    ```bash
    docker-compose up -d
    ```

3. **Open your browser**

    Once the containers are running, open your browser and navigate to:

    ```
    http://localhost:5000
    ```

    You should see the API running. Adjust the port if you have changed it in the Docker configuration.


## 📄 Licence Used

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/Amitpnk/Vertical-Slice-Architecture/blob/main/LICENSE)

See the contents of the LICENSE file for details

## 📬 Contact

Having any issues or troubles getting started? Drop a mail to amit.naik8103@gmail.com or [Raise a Bug or Feature Request](https://github.com/Amitpnk/Vertical-Slice-Architecture/issues/new). Always happy to help.
