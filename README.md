# Online-Bookstore-API
The Online Bookstore API is a web service that allows users to interact with a virtual bookstore. It provides various functionalities for users to browse, search, and purchase books. The API supports both regular users and admin users with different sets of permissions.

Regular users can browse books by category, search for books using different criteria, and view detailed information about a book. They can add books to their shopping cart and place orders by providing shipping information. Regular users can also view their order history to track their purchases.

Admin users have additional capabilities to manage the bookstore inventory. They can add new books, update existing book information, and remove books from the inventory. Admin users can also view and manage customer orders, allowing them to track and process orders efficiently.


To ensure security, the API implements authentication and authorization mechanisms. Regular users and admin users have different access levels and permissions, ensuring that only authorized users can perform specific actions.


## Technologies And Libraries
1) .net 7
2) Swashbuckle AspNetCore 6.5.0
3) AutoMapper Extensions Microsoft DependencyInjection 12.0.1
4) System IdentityModel Tokens Jwt 6.32.1
5) Microsoft AspNetCore Authentication JwtBearer 7.0.9
6) Microsoft VisualStudio Web CodeGeneration Design 7.0.8
7) Microsoft Identity Web 2.13.2
8) Microsoft EntityFrameworkCore 7.0.9
9) Microsoft EntityFrameworkCore SqlServer 7.0.9
10) Microsoft EntityFrameworkCore Tools 7.0.9
11) Microsoft EntityFrameworkCore Design 7.0.9
12) Microsoft AspNetCore OpenApi 7.0.9
13) Microsoft AspNetCore Authentication OpenIdConnect 7.0.9
14) Serilog AspNetCore 7.0.0
15) Serilog Settings Configuration 7.0.1
16) Serilog Enrichers
      1) Environment 2.2.0
      2) Process 2.0.2
      3) Thread 3.1.0


## Features
1) Authentication 
      1) Register
      2) Login
      3) Add User To Role (Admin Or User) 
      4) Get Refresh Token
      5) Revoke Token


2) User (FirstName, LastName, Address, PhoneNumber, Password, Email, IsAvailable)
      1) Change Password
      2) Update Personal Info
      3) Delete User By User Name (Admins Only)
      4) Get User Info By User Name

        
3) Book (Title, Description, Genre, Author, Price, Copies, IsAvailable)
      1) Create a New Book
      2) Update Book
      3) Delete Book
      4) Get all Book
      5) Get Book By Title
      6) Get Book By Genre
      7) Get Book By Author


 4) Shopping Cart (UserId, TotalPrice, cart items, IsOrdered)
      1) Create a new Shopping Cart 
      2) Update User's Shopping Cart
      3) Delete User Shopping Cart By ID
      4) Get User Shopping Cart By UserName


  5) Orders (UserId, Created Date, Delivery Date, ShoppingCart)
      1) Create a new Order
      4) Get All User Orders
      5) Get All Orders (Admins Only)
      6) Get Order By ID (Admins Only)



## Built With
1) Visual Code (VS) 2022
2) Microsoft SQL Server Management Studio (SSMS)
3) Postman For Test Apis


   
## Run and Screenshots

### Auth Controller
![Screenshot (38)](https://github.com/EssamSheriff/Online-Bookstore-API/assets/72581790/97651c46-bd4a-4c99-8a54-8ddf14deb81d)


### User Controller
![Screenshot (42)](https://github.com/EssamSheriff/Online-Bookstore-API/assets/72581790/190bbce2-b15a-422d-8a56-c33aa73042ac)



### Book Controller
![Screenshot (39)](https://github.com/EssamSheriff/Online-Bookstore-API/assets/72581790/36bb16af-5c87-4f1d-a53f-12930e4961ed)


### Shopping cart Controller
![Screenshot (40)](https://github.com/EssamSheriff/Online-Bookstore-API/assets/72581790/71dfcf77-91cd-452b-bfcf-f1ef8c883c91)

 
### Orders Controller
![Screenshot (41)](https://github.com/EssamSheriff/Online-Bookstore-API/assets/72581790/1c9803f9-78e8-49f2-9abe-85b7af456ea4)


