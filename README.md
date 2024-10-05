<img src="BiodataManagementApp/src/BiodataManagement.Web/wwwroot/images/hero-img.png" alt="Banner Image for Biodata Management App" width="1000">

# Biodata Management App

Biodata Management App is an ASP.NET Core MVC Web Application that allows user to create and edit their biodata.

Bassically a CRUD App with authentication and authorization feature to make normal user can only see and edit their own biodata and only admin user can view and edit all data.

## Table of Contents

- [Overview](#Biodata-Management-App)
- [Architecture](#Architecture)
- [Technologies](#Technologies)
- [Usage](#Usage)

## Architecture

- Mainly following clean architecture to separate code by technical layer, and emphasize inward dependency.
- Repository Pattern
- Result Pattern

## Technologies

Some of the technology i used to build this app :

- ASP.NET Core

        The framework for this Biodata Management Web App.

- JQuery

        For client-side interactivity e.g sweet alert and calling some action method.

- Fluent Validation

        For validating incoming request

- Dapper

        Micro ORM for talking and map entity from database

- Entity Framework Core

        ORM i used to integrate with Authentication and authorization of user data.

- Asp.Net Core Identity

        Library for managing user identity, authentication and authorization

- SQL Server

        The database i use for storing and retrieving data.

- ErrorOr

        A simple, fluent discriminated union of an error or a result. A library for implementing Result pattern i use in the App.

- DbUp

        A library for managing database migration.

## Usage

- Clone this repo

```bash
git clone https://github.com/m13dans/BiodataManagementApp.git
```

- Download and install Dotnet 8 SDK
- Make sure you have SQL Server installed on your machine

- CD into project and run it.

```bash
cd BiodataManagement.Web
dotnet run
```

## Todo

pagination feature, export to pdf, Unit Test
