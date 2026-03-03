# ASP.NET Laboratory Work

## Project Overview

This repository contains the implementation of laboratory works completed within the course **"ASP.NET"** by `Denis Andriiuk`.

This repository contains all laboratory works for the ASP.NET course.
It includes both:

* a main course project (TicketBooking solution with MVC + Web API + Client + Data projects), and
* separate lab folder for Lab 1 with focused experiments (middleware, CLI scaffolding, basic MVC, etc.).

> Runtime / SDK: .NET 10 (Target Framework: net10.0)
> 
> Language: C# (version supported by .NET 10 SDK)

---

## Solution Structure

```
ASP.NET/
│
│   LICENSE
│   README.md
│
├───TicketBookingSln
│   ├───TicketBooking
│   │   ├───Components
│   │   ├───Controllers
│   │   ├───Infrastructure
│   │   ├───Migrations
│   │   ├───Models
│   │   │   └───ViewModels
│   │   ├───Properties
│   │   └───Views
│   │       ├───Account
│   │       ├───Booking
│   │       ├───Cart
│   │       ├───Event
│   │       ├───Home
│   │       └───Shared
│   │
│   ├───TicketBooking.Api
│   │   ├───Controllers
│   │   ├───DTOs
│   │   ├───Mapping
│   │   └───Properties
│   │
│   ├───TicketBooking.Client
│   │   ├───Infrastructure
│   │   ├───Layout
│   │   ├───Models
│   │   ├───Pages
│   │   ├───Properties
│   │   └───Services
│   │
│   └───TicketBooking.Data
│       ├───Contexts
│       ├───Infrastructure
│       ├───Models
│       ├───Repositories
│       └───Seed
│
├───lab1
│   ├───ConsoleToWeb
│   ├───MiddlewareSandbox
│   ├───MVC
│   └───WebFromCli
│
└───reports
        Andriiuk_Lab1.pdf
        Andriiuk_Lab2.pdf
        Andriiuk_Lab3.pdf
        Andriiuk_Lab4.pdf
        Andriiuk_Lab5.pdf
        Andriiuk_Lab6.pdf

```

---
## Main Solution: TicketBookingSln

`TicketBookingSln` is the primary “course project” solution and acts as an integration point for the topics studied during the course.

### High-level responsibilities

* TicketBooking (MVC): server-rendered UI, view models, and user interaction logic.
* TicketBooking.Api: REST endpoints, DTOs, mapping, and API-specific logic.
* TicketBooking.Client: client UI that communicates with TicketBooking.Api and handles JWT-based auth on the client side.
* TicketBooking.Data: database layer (DbContext/entities/repositories/seed).

---

## Reports

All reports are stored in the `reports/` folder in PDF format:

* [Andriiuk_Lab1.pdf](https://github.com/SalTarrae/ASP.NET/blob/master/reports/Andriiuk_Lab1.pdf)
* [Andriiuk_Lab2.pdf](https://github.com/SalTarrae/ASP.NET/blob/master/reports/Andriiuk_Lab2.pdf)
* [Andriiuk_Lab3.pdf](https://github.com/SalTarrae/ASP.NET/blob/master/reports/Andriiuk_Lab3.pdf)
* [Andriiuk_Lab4.pdf](https://github.com/SalTarrae/ASP.NET/blob/master/reports/Andriiuk_Lab4.pdf)
* [Andriiuk_Lab5.pdf](https://github.com/SalTarrae/ASP.NET/blob/master/reports/Andriiuk_Lab5.pdf)
* [Andriiuk_Lab6.pdf](https://github.com/SalTarrae/ASP.NET/blob/master/reports/Andriiuk_Lab6.pdf)

---

## Tech Stack

* .NET: 10 (`net10.0`)
* ASP.NET Core: 10
* C#: version provided by .NET 10 SDK
* Database: SQL Server / LocalDB (depending on environment)
* ORM: Entity Framework Core
* Auth: JWT (JSON Web Tokens)

---

## Commonly used packages (depending on the lab/project)

Exact package list may differ per project. Typical choices for this solution include:

* `Microsoft.EntityFrameworkCore`
* `Microsoft.EntityFrameworkCore.SqlServer`
* `Microsoft.EntityFrameworkCore.Tools`
* `Microsoft.AspNetCore.Authentication.JwtBearer`
* API documentation `Swashbuckle.AspNetCore`

---

## Why .NET 10?

This repository uses .NET 10 to follow the modern ASP.NET Core stack:

* access to the newest platform features and improvements,
* better performance and runtime optimizations,
* up-to-date security patches and recommended defaults,
* alignment with modern tooling (Visual Studio 2026, latest SDK templates).

---

## How to Run

### Prerequisites

* .NET SDK 10
* SQL Server (LocalDB)

### Run a specific project

From the solution root:

```bash
dotnet restore
dotnet build
dotnet run --project TicketBookingSln/TicketBooking.Api
dotnet run --project TicketBookingSln/TicketBooking.Client
```

If EF Core migrations are used:

```bash
dotnet ef migrations add InitialCreate --project TicketBookingSln/TicketBooking.Data
dotnet ef database update --project TicketBookingSln/TicketBooking.Data
```

---

## License

See [MIT LICENSE]([https://github.com/SalTarrae/ASP.NET/blob/master/LICENSE](https://github.com/SalTarrae/ASP.NET?tab=MIT-1-ov-file)) in the repository root.

---

## Author

Denys Andriiuk

ASP.NET course — 2026
