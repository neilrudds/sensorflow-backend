# Welcome to SensorFlow!

A project developed in partial fulfilment of the requirement for the degree of MASTER OF SCIENCE in Software Development in The Queens University of Belfast.

**Neil Rutherford - 40351466**

This is the backend solution for the SensorFlow IoT Platform.

This application provides the backend services for the application and has been developed in **ASP.NET Core**  following the Clean Architecture and Domain-Driven Design Principles.

The application is a bespoke IoT platform enabling users to create there own dynamic, configurable and bespoke set of workspaces and dashboard through a dynamic grid and widget style layout.

## The application contains the following components:

- API - ASP.NET 7 REST API with Swagger support
- Database - SQL Server database intergartion via Entity Framework Core
- Migrations - Code-First database migrations managed using Package Manager and EFCore.

## IDE

Visual Studio 2022

Visual Studio 2022 is recommended for opening the main solution for this project - `sensorflow-backend.sln`.

## Solution Project Structure
The solution consists of the following projects

SensorFlow.WebApi - ASP.NET 7 Web API with Swagger support
SensorFlow.Infrastructure - Infrastructure layer for all external integration e.g. database
SensorFlow.Domain - Domain layer containing Entities 
SensorFlow.Application - Application layer containing Commands/Queries/Behaviour Validations

## Nuget Libraries
The following Nuget libraries are used across the solution:
Entity Framework Core (EF Core) - ORM for interacting with Database. SQL Server provider used by default
Microsoft Identity Core EF - Microsoft Identity Management (Including ASPNetCore.Authentication & ASPNetCore.Authorization)
Swagger/Swashbuckle - UI for interacting with API
AutoMapper - Used to map from Entities in Domain to DTOs in Application
MediatR - Mediator implementation for implementing CQRS
XUnit - Test runner for Unit and API tests
FluentAssertions - Fluent extension methods for running assertions in tests
FluentValidation - for building strongly-typed validation rules.
ErrorOr - Used for packaging function result types along with error messages and responses

## Entity Framework Core
Entity Framework Core (EF) is used as the database ORM for this proect.

Install the Entity Framework CLI

>dotnet tool install --global dotnet-ef

## Docker
Docker is used to build and deploy the code for this project. It is also used to spin-up services for local development such as the database.

## Docker Desktop
Docker Desktop is recommended for running Docker if you are using a Windows development machine.

## CI/CD
GitHub Actions is used to run deployments to Amazon EC2 instances running the docker engine.
