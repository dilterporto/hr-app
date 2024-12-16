# HR-API

## Technical References

The following architectural patterns were approached on this solution:: 

- [CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Event Sourcing](https://learn.microsoft.com/en-us/azure/architecture/patterns/event-sourcing) 
- [Domain Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)

Following the **layered architecture** from Domain Driven Design the solution contains the layers:

- Presentation (Rest API)
- Application (Command and Query Handlers representing the *use cases*)
- Domain (Agreggates, Repositories, Entities ...)
- Infrastructure (Persistence, Reading, Caching)

## Solution features

The features requested were implemented following the definition of *use cases* on the *Application Layer* and exposed via HTTP throught a REST API.

*Employee API*
- Create employee
- Get all employees
- Get employee by id
- Update employee
- Delete employee

*Department API*
- Get all departments
- Create department

Using the *writing* and *reading* segregation sides the features that represents the change of the state's application were represented by *commands* and the actions without changes by *queries* and [exposed by respective HTTP methods via a REST API](https://www.infoq.com/articles/rest-api-on-cqrs/).


## Running the solution

To run the solution will be necessary to have a **git** SCM to clone from Bitbucket and Docker to execute in a containerized environment.

### Requirements

- git
- docker

### Instructions

- clone the code from Giuthub repository
- on the root folder run the **docker-compose** command to start services on development (local) environment


    ```shell
    > docker-compose -f ./docker-compose.dev-env.yml  up
    ```
- to run the app 

    ```
    > dotnet run --project src/HR.Api/HR.Api.csproj 
    ```

Once the app is running locally via **Docker** the following links will available:

- [**Swagger**](http://localhost:5186/swagger)


