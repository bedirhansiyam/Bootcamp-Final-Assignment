# .Net Bootcamp Final Assignment - Web API

This project has been developed as part of the final assignment for the .Net Bootcamp. The platform allows the sale of digital products and product licenses through various channels including mobile and web applications. The API design offers endpoints for user registration, authentication, product management, coupon handling, order management and more. For detailed API documentation, please click [here](https://documenter.getpostman.com/view/26555042/2s93z6ejJy#d3a29b10-faf6-49d4-a60e-e2fb5cca0ea8).
## Contents

- [Database Setup](#database-setup)
- [Features](#features)
- [Tech Stack](#tech-stack)

## Database Setup

To create the required database for the project, please follow these steps:

1. Navigate to the directory containing the `.sln` file in your terminal.
2. Run the following command to apply initial migrations and create the database:
   
   ```bash
   dotnet ef database update --project "./WebApi.Data" --startup-project "./WebApi"

3. Upon database creation, an admin account is automatically generated:

- **Username**: admin
- **Password**: admin123

## Features

- **User Authentication and Roles**: Microsoft Identity is integrated for user management. The platform distinguishes between two user roles: Regular Users and Admin Users.
- **User Operations**: Regular Users can register, log in, and make purchases. They earn points with each purchase for discounts on future orders.
- **Admin Control**: Admin Users have full system control. They can manage products, define coupons, perform category operations and manage users.
- **Product Management**: Admin Users can manage products, including adding new products, updating existing ones and managing stock information.
- **Coupon System**: Admin Users can generate and manage coupons. Users can apply valid coupons during payment for discounts.
- **Loyalty Points System**: Users earn loyalty points with each purchase. The points are used to redeem discounts on future purchases.
- **Category Operations**: Admins can define and manage product categories. Each product can belong to multiple categories.
- **Order Management**: Users can place orders for multiple products. Each order is assigned a unique order number. Order history can be viewed by users, while admins can access detailed order reports.



## Tech Stack

The project utilizes the following technologies:

- **Database**: MS SQL Server
- **Authorization**: JWT Token
- **ORM**: Entity Framework (EF) Repository and Unit of Work patterns
- **API Documentation**: Postman
- **Unit Test**: xUnit
- **Logging**: SeriLog
