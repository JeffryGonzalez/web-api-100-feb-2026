# Course Overview

## Services 

- Some code that owns some data (state) and all the process around that data.
  - roughly the same definition that Dr. Alan Kay gave when someone asked him what he meant by this new term called "object oriented"
- Service Oriented Architecture
  - "Microservices are just SOA done right"
  - Services don't (shouldn't? ) share databases.

## APIs

- The mechanisms through which we enable others in the company or outside to write software 
to do the things we want them to do.

- HTTP Apis - 
  - Not a lot of creativity is required.
  - 

## HTTP-Based APIs

- Tim Berners Lee
- RFC 2616
- Roy T. Fielding - "Chief Scientist at Adobe"
-  "REST", "RESTful", "Web API" 
  - 

## .NET and Web API 

- 1989 - T. Berners Lee creates WWW. (MS had "Windows 2" then...)
- "Vendor Lock In"
- 2001 - .NET with ASP.NET Web Forms and XML Web Services
  - "The next generation tools for building XML Web Services"
    - Anti-Vendor Lock In.
    - SOAP - XML (cross-platform data and data structure representation!)
      - WCF (2008)
- 2004 - Ruby On Rails Released (MVC-like web framework)
- 2009 - ScottGu - ASP.NET MVC
  - M - Model "business"
  - V - View "display"
  - C - Controller "input control"
- 2012 - Web API 1.0
  - Originally part of the WCF Platform
  - Moved to ASP.NET at Release
  - Built upon the conventions of ASP.NET MVC, but not the code.
- 2016 - .NET "Core"
  - Cross Platform
  - Open Source
  - ASP.NET "Core" MVC merged the code bases for ASP.NET MVC and Web API

- "Real" 4.x            (5)
- Core - 1... 2... 3... (5)

## We are learning ASP.NET MVC 10

- C# 14
- See [.NET Support Lifecycle](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- Using the `Microsoft.AspNetCore.App` `10.x` Runtime

### Endpoints / Operations

- "Projecting" our business domain into the simple vocabulary of HTTP [[10-http]]
- The "MVC" style with controllers will be called (here) "Controller Model".
  - *Almost* ready to declare this as "legacy" for reasons I will explain, but include, at a non-technical level:
    -  Forces you to organize your code around technical implementation instead of "domain" or business-facing concerns.
    -  Controllers, almost by definition, are a single class with multiple responsibilities. That is, as they say in advanced computer science, "bad".
 -  The "Minimal API" model, introduced in .NET 6, I think - slid into your DMs all sly, like.
    -  Seems to be the future.
 -  We will cover both.

#### Exposing Endpoints

#### Validating Inputs

- Routes / Route Params
- QueryStrings
- Entities/Bodies
  - **note** Big change here with .NET 10 that will be contrary to advice I've given in the past.

### Developer Testing

- Unit and Unit Integration Testing
- System Testing
- System Integration Testing

### Documentation

- Documenting our API with OpenAPI v. 3.x
- Why?
- When?
- Generating Clients/Types

### Hosting 

- Making our environment declarative
  - Docker Compose / Manifests / Etc.
- Configuration
  - Dependencies - Early Bound
  - Dependencies - Late Bound

