# Restaurant Management System

## Setup Instructions

1. **Install dependencies**: 
   Ensure that you have the required dependencies installed by running: dotnet restore
2. **Database Setup**
   The project uses Entity Framework Core to interact with the database.
   Make sure you have a valid SQL Server instance available.
   Update the connection strings in appsettings.json and appsettings.Development.json to match your database setup.
        Set DefaultConnection using a valid SQL Server connection string.
   
3. **Run the application**
   Set RestaurantManagement.Api as Startup Project.
   Running RestaurantManagement.App will also run migrations (including the creation of the DB)
   To start the application, use: dotnet run


## Architectural Explanation and Trade-offs

**Architecture Overview**:
This system is based on a clean architecture pattern with clear separation of concerns into multiple layers:

1. API Layer: This layer handles incoming HTTP requests, routes them to appropriate endpoints, and returns responses. It is lightweight and serves as the entry point to the system.
2. Service Layer: Contains the core business logic. It interacts with repositories to retrieve or manipulate data. The services encapsulate the application's rules and act as intermediaries between the API and database layers.
3. Database Layer (EF Core): Uses Entity Framework Core for data modeling, query generation, and database interactions. This layer is responsible for storing and retrieving data from the database.

**Trade-offs**:
1. Separation of concerns: The layered approach ensures that each component has a single responsibility. However, this can introduce complexity in terms of managing dependencies across layers.
2. Database abstraction: Using Entity Framework Core allows for easy database interactions and migrations, but it may introduce some overhead in terms of performance for complex queries.
3. Testing: The architecture allows for unit testing of each layer separately, ensuring that business logic can be tested without hitting the database.

## Assumptions and Constraints

**Assumptions**:
1. The system assumes that a SQL Server database is available and configured, though this could be easily extended to support other databases.
2. It is assumed that users will interact with the system via the API, and no frontend interface is provided in this repo.
3. The application is built to scale horizontally, although optimizations for performance might be necessary for larger systems.

**Constraints**:
1. Database dependencies: The system relies heavily on the database for storing critical business data like orders, menu items, and customer information.
2. Order Status: The system tracks and logs order status changes, but the historical data is stored in a separate table (OrderLog), which may increase database complexity.

## Future Improvement Ideas

***Performance Optimization***:
1. If the application handles large volumes of orders, consider optimizing database queries and introducing caching mechanisms for frequent read operations.
2. User Authentication & Authorization:
    Implement role-based authentication to manage access levels for customers, delivery staff, and admins.
3. Order Notification System:
    Implement a notification system to alert customers when their order status changes (e.g., when it's out for delivery or ready for pickup).
4. Real-time Order Tracking:
    Allow customers to track their orders in real-time using WebSockets or long-polling.
