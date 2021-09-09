# burger_backend
Burger Backend

To test the services:
- on each project, start it with a different command prompt and "dotnet run"
    * BurgerBackend.Review.Service> dotnet run
    * BurgerBackend.Finder.Service> dotnet run
    * BurgerBackend.Identity.Service> dotnet run

First step is to create a user on the Identity.Service
    - navigate to https://localhost:44324/swagger
    - use the /api/account/register to create a new user
    - use the /api/account/authenticate to generate a temporary JWT token
    - don't stop the service

Now you can test any of the other two services:
    - navigate to https://localhost:44347/swagger to access the Finder service
    - navigate to https://localhost:44309/swagger to access the Review service
    - before using the endpoints, please paste the JWT token on Swagger Authorize (button)
        * click on Authorize
        * type Bearer [space] "your jwt token", eg. (Bearer "ABCDEFGHI")