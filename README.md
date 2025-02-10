# AssignmentGapster

This solution implements Clean Architecture with CQRS, SQL LINQ Queries, Rate Limiting, Exception Handling, Unit Testing, Unit of Work, Generic Repository, Seril Logs,Caching (Redis), and Pagination.


Before running the application, ensure you have the following installed:

.NET 9.0 SDK

SQL Server or PostgreSQ

Postman or Curl (for API testing)


1. Clone the Repository
2. Configure Environment Variables
3. Create Databse and Run below sql Query that will create two table in DB
   
   CREATE TABLE Shows (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Language NVARCHAR(100) NOT NULL,
    Premiered DATE NOT NULL,
    Summary NVARCHAR(MAX) NULL
   );

   CREATE TABLE ShowGenres (
       Id INT PRIMARY KEY IDENTITY(1,1),
       ShowId INT NOT NULL,
       Genre NVARCHAR(100) NOT NULL,
       FOREIGN KEY (ShowId) REFERENCES Shows(Id) ON DELETE CASCADE
   );

4. Start the Application
5. API Endpoints 

GET

/api/shows?page=1

Get paginated list of shows

GET

/api/shows/{id}

Get show by ID

POST

/api/shows

Create a new show

PUT

/api/shows/{id}

Update a show

DELETE

/api/shows/{id}

Delete a show


