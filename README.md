# KandM Clothes - E-commerce Web Application

KandM Clothes is a full-featured e-commerce web application built with ASP.NET MVC, Entity Framework, and Bootstrap. This application provides a complete online shopping platform for clothing products.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Database Structure](#database-structure)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Admin Panel](#admin-panel)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Features

### Customer Features
- **Product Browsing**: Browse products by categories with filtering and search capabilities
- **Product Details**: View detailed product information, images, and specifications
- **Shopping Cart**: Add products to cart, update quantities, and manage cart items
- **Checkout Process**: Complete purchases with various payment options
- **User Accounts**: Register, login, and manage personal information
- **Order History**: View past orders and track order status
- **Responsive Design**: Works on desktop, tablet, and mobile devices

### Admin Features
- **Dashboard**: Overview of sales, orders, and inventory
- **Product Management**: Add, edit, delete products and categories
- **Order Management**: View, process, and manage customer orders
- **Category Management**: Create and manage product categories
- **Customer Management**: View and manage customer accounts
- **Content Management**: Manage website content including banners and promotions

## Technologies

- **Backend**:
  - ASP.NET MVC 5
  - Entity Framework 6
  - C# 
  - Dapper
  - Identity Framework for authentication

- **Frontend**:
  - HTML5, CSS3, JavaScript
  - Bootstrap 5
  - jQuery
  - Summernote (rich text editor)

- **Database**:
  - SQL Server

- **Other Tools**:
  - CKFinder for file management
  - PagedList for pagination
  - AJAX for asynchronous operations
  - Webpack for asset bundling

## Architecture

KandM Clothes follows the Model-View-Controller (MVC) architectural pattern:

- **Models**: Represent the data and business logic
  - Entity Framework models (Product, Category, Order, etc.)
  - View models for specialized view requirements
  
- **Views**: Responsible for displaying the UI to users
  - Razor views with layout templates
  - Partial views for reusable components
  
- **Controllers**: Handle user requests and coordinate between models and views
  - ProductController, ShoppingCartController, etc.
  - Admin area controllers for administrative functions

The application also utilizes Areas to separate admin functionality from the customer-facing storefront.

## Database Structure

Key database entities include:

- **Product**: Contains product information including title, description, price, and inventory
- **ProductCategory**: Product categories and subcategories
- **ProductImage**: Product image gallery
- **Order**: Customer orders
- **OrderDetail**: Line items for each order
- **ShoppingCart**: Customer shopping carts
- **Category**: Content categories (for blog posts, news, etc.)
- **User**: Customer accounts (extends ASP.NET Identity)

## Installation

### Prerequisites
- Visual Studio 2019 or later
- SQL Server 2016 or later
- .NET Framework 4.7.2 or later

### Steps
1. Clone the repository
   ```
   git clone https://github.com/yourusername/KandM-Clothes.git
   ```

2. Open the solution file `KandM Clothes.sln` in Visual Studio

3. Restore NuGet packages
   ```
   Update-Package -reinstall
   ```

4. Update the connection string in `Web.config` to point to your SQL Server

5. Open Package Manager Console and run migrations
   ```
   Update-Database
   ```

6. Build and run the application
   ```
   F5 or Ctrl+F5
   ```

## Configuration

### Connection Strings
Edit the connection string in `Web.config` to match your database configuration:

```xml
<connectionStrings>
  <add name="DefaultConnection" connectionString="Data Source=YOUR_SERVER;Initial Catalog=KandMClothes;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```

### App Settings
Configure application settings in `Web.config`:

```xml
<appSettings>
  <add key="webpages:Version" value="3.0.0.0" />
  <add key="webpages:Enabled" value="false" />
  <add key="ClientValidationEnabled" value="true" />
  <add key="UnobtrusiveJavaScriptEnabled" value="true" />
  <!-- Add your custom settings here -->
</appSettings>
```

## Usage

### Customer Interface
1. Browse the home page to see featured products
2. Navigate through product categories
3. Use search functionality to find specific products
4. Add items to your shopping cart
5. Proceed to checkout to complete your purchase

### Admin Panel
1. Access the admin area at `/admin`
2. Login with admin credentials
3. Manage products, categories, orders, and content

## Admin Panel

The admin panel is accessible at `/admin` and provides the following management sections:

- **Dashboard**: Overview of system activities
- **Products**: Manage product listings
- **Categories**: Manage product categories
- **Orders**: View and process customer orders
- **Customers**: Manage customer accounts
- **Content**: Manage website content
- **Settings**: Configure system settings

## API Documentation

The application includes internal APIs for various functionalities:

- **Product API**: Manage product data
- **Order API**: Process and track orders
- **Cart API**: Handle shopping cart operations
- **User API**: Manage user accounts

For detailed API documentation, please refer to the API documentation provided in the code comments.

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.