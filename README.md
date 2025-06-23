# Life Insurance App

Aplicación web que permite registrar y gestionar personas, incluyendo datos personales y de salud. Implementa autenticación, autorización, validaciones de negocio, y pruebas unitarias. Incluye un frontend en Angular y un backend en ASP.NET Core 8.


## Tecnologías utilizadas

### Backend (.NET 8)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Identity con JWT
- Clean Architecture (Infrastructure, API, Application, Domain)
- CQRS + MediatR
- FluentValidation
- AutoMapper
- xUnit + Moq + FluentAssertions

### Frontend (Angular 16)
- Angular CLI
- Angular Material
- Reactive Forms
- Interceptores HTTP
- RxJS


## Estructura del proyecto

```text
/life-insurance-app
│
├── /backend
│ ├── LifeInsurance.API # API con controladores, DTOs, mappers y validadores
│ ├── LifeInsurance.Application # Servicios, CQRS, interfaces de negocio
│ ├── LifeInsurance.Domain # Entidades y contratos de dominio
│ ├── LifeInsurance.Infrastructure # Persistencia, migraciones, repositorios, auth
│ ├── LifeInsurance.Tests # Pruebas unitarias
│ └── LifeInsurance.sln # Solución .NET
│
├── /frontend
│ └── life-insurance-app # Proyecto Angular
│
└── README.md
```


## Instrucciones de instalación

### Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js (v18+ recomendado)](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- SQL Server (local)


## Levantar el proyecto

### Backend

1. Ir a la carpeta `/backend/LifeInsurance.API`:

   ```bash
   cd backend/LifeInsurance.API
   ```

2. Restaurar dependencias:

    ```bash
    dotnet restore
    ```

3. Aplicar migraciones y crear la base de datos:

    ```bash
    dotnet ef database update
    ```

4. Ejecutar la API:

    ```bash
    dotnet run
    ```

La API quedará disponible en https://localhost:7016


### Frontend

1. Ir a la carpeta del proyecto Angular:

    ```bash
    cd frontend/life-insurance-app
    ```

2. Instalar dependencias:

    ```bash
    npm install
    ```

3. Ejecutar la aplicación:

    ```bash
    ng serve -o
    ```

La aplicación estará disponible en http://localhost:4200

### Pruebas
Desde la raíz del proyecto de tests:

    ```bash
    dotnet test
    ```