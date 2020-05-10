# Imageboard
Imageboard API+UI example with using React + ASP.NET Core (with CQRS).

This project was heavenly inspired by [Jason Taylor](https://github.com/jasontaylordev) and his [Clean Architecture project](https://github.com/jasontaylordev/CleanArchitecture).

# Technologies
## API
- .NET Core 3.1
- ASP .NET Core 3.1
- Entity Framework Core 3.1
- MediatR
- AutoMapper
- FluentValidation
- NUnit, FluentAssertions

## UI
- React 16.13
- Material UI 4.9
- Formik 2.1.4
- Yup 0.28.5
- React MDE 10.0
- React Markdown 4.3
- Material UI Dropzone 3.0
- React FSLightbox 1.4

# Database
This project uses SQlite as DB (just for easy setting up).

## Database Migrations
If you wan to create a migration - use helper script `.\src\AddMigration.ps1` - it calls `dotnet ef` under the hood with all apropriate parameters. Only one thing you need - specify the name, PS script will request it on running.

# Overview

## api/Imageboard.Domain

This project contains only entities, related to the domain area of application. It has no dependencies to other projects.

## api/Imageboard.Application

This project contains application layer logic - it has dependencies on `Imageboard.Domain` project, but do not have any other dependecies. All application logic is presented here and divided into commands and queries using CQRS. This project also contains all abstractions, related to the infrastructure layer, but no implementations. 

## api/Imageboard.Infrastructure

This project contains implementations of DAL and application services, which are related to the system functions - like `DateTimeService` and `FileService`

## api/Imageboard.Application.IntegrationTests

This project contains integration tests for all commands and queries with re-creation of real SQlite database for every run. Also, some tests which are manipulating with files, are presented here.

## ui/
VS code project for UI using React as main UI library. To launch the project, run the following commands:

Install all dependencies:
```
npm install
```

Run the project:
```
npm start
```

# License

This project is licensed with the [MIT license](LICENSE).
