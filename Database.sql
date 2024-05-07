CREATE DATABASE KandM 
GO

USE KandM
GO

CREATE TABLE _tb_Menu (
  Id INT PRIMARY KEY,
  Title NVARCHAR(MAX),
  Description NVARCHAR(MAX),
  Position INT,
  SeoTitle NVARCHAR(MAX),
  SeoDescription NVARCHAR(MAX),
  SeoKeywords NVARCHAR(MAX),
  CreatedDate DATETIME,
  CreatedBy NVARCHAR(MAX),
  ModifiedDate DATETIME,
  ModifiedBy NVARCHAR(MAX)
);

CREATE TABLE _tb_Advertisement (
  Id INT PRIMARY KEY,
  Title NVARCHAR(MAX),
  Description NVARCHAR(MAX),
  Image NVARCHAR(MAX),
  Type INT,
  Link NVARCHAR(MAX),
  CreatedDate DATETIME,
  CreatedBy NVARCHAR(MAX),
  ModifiedDate DATETIME,
  ModifiedBy NVARCHAR(MAX)
);

CREATE TABLE _tb_Category (
  Id INT PRIMARY KEY,
  Title NVARCHAR(MAX),
  Description NVARCHAR(MAX),
  CreatedDate DATETIME,
  CreatedBy NVARCHAR(MAX),
  ModifiedDate DATETIME,
  ModifiedBy NVARCHAR(MAX)
);

CREATE TABLE _tb_ProductCategory (
  Id INT PRIMARY KEY,
  Title NVARCHAR(MAX),
  Description NVARCHAR(MAX),
  Icon NVARCHAR(MAX),
  CreatedDate DATETIME,
  CreatedBy NVARCHAR(MAX),
  ModifiedDate DATETIME,
  ModifiedBy NVARCHAR(MAX)
);

CREATE TABLE _tb_Contact (
  Id INT PRIMARY KEY,
  Name NVARCHAR(MAX),
  Website NVARCHAR(MAX),
  Email NVARCHAR(MAX),
  Message NVARCHAR(MAX),
  IsRead BIT,
  CreatedDate DATETIME,
  CreatedBy NVARCHAR(MAX),
  ModifiedDate DATETIME,
  ModifiedBy NVARCHAR(MAX)
);

CREATE TABLE _tb_Product (
  Id INT PRIMARY KEY,
  Title NVARCHAR(MAX),
  CategoryId INT,
  Description NVARCHAR(MAX),
  Price DECIMAL(18, 2),
  PriceSale DECIMAL(18, 2),
  Quantity INT,
  Detail NVARCHAR(MAX),
  Image NVARCHAR(MAX),
  SeoTitle NVARCHAR(MAX),
  SeoDescription NVARCHAR(MAX),
  SeoKeywords NVARCHAR(MAX),
  CreatedDate DATETIME,
  CreatedBy NVARCHAR(MAX),
  ModifiedDate DATETIME,
  ModifiedBy NVARCHAR(MAX)
);

CREATE TABLE _tb_New (
  Id INT PRIMARY KEY,
  Title NVARCHAR(MAX),
  CategoryId INT,
  Description NVARCHAR(MAX),
  Detail NVARCHAR(MAX),
  SeoTitle NVARCHAR(MAX),
  SeoDescription NVARCHAR(MAX),
  SeoKeywords NVARCHAR(MAX),
  CreatedDate DATETIME,
  CreatedBy NVARCHAR(MAX),
  ModifiedDate DATETIME,
  ModifiedBy NVARCHAR(MAX)
);

CREATE TABLE _tb_Post (
  Id INT PRIMARY KEY,
  Title NVARCHAR(MAX),
  CategoryId INT,
  Description NVARCHAR(MAX),
  Detail NVARCHAR(MAX),
  SeoTitle NVARCHAR(MAX),
  SeoDescription NVARCHAR(MAX),
  SeoKeywords NVARCHAR(MAX),
  CreatedDate DATETIME,
  CreatedBy NVARCHAR(MAX),
  ModifiedDate DATETIME,
  ModifiedBy NVARCHAR(MAX)
);

CREATE TABLE _tb_Order (
  Id INT PRIMARY KEY,
  Code NVARCHAR(MAX),
  CustomerName NVARCHAR(MAX),
  Phone NVARCHAR(MAX),
  Address NVARCHAR(MAX),
  TotalAmount DECIMAL(18, 2),
  Quantity INT,
  CreatedDate DATETIME,
  CreatedBy NVARCHAR(MAX),
  ModifiedDate DATETIME,
  ModifiedBy NVARCHAR(MAX)
);

CREATE TABLE _tb_OrderDetail (
  Id INT PRIMARY KEY,
  OrderId INT,
  ProductId INT,
  Price DECIMAL(18, 2),
  Quantity INT
);

CREATE TABLE _tb_Subscribe (
  Id INT PRIMARY KEY,
  Email NVARCHAR(MAX),
  CreatedDate DATETIME
);

CREATE TABLE _tb_SystemSetting (
  SettingKey NVARCHAR(450) PRIMARY KEY,
  SettingValue NVARCHAR(MAX),
  SettingDescription NVARCHAR(MAX)
);

ALTER TABLE _tb_Post 
ADD CONSTRAINT FK_Post_Category 
FOREIGN KEY (CategoryId) REFERENCES _tb_Category(Id);

ALTER TABLE _tb_Product 
ADD CONSTRAINT FK_Product_Category 
FOREIGN KEY (CategoryId) REFERENCES _tb_ProductCategory(Id);

ALTER TABLE _tb_New 
ADD CONSTRAINT FK_New_Category 
FOREIGN KEY (CategoryId) REFERENCES _tb_Category(Id);

ALTER TABLE _tb_OrderDetail 
ADD CONSTRAINT FK_OrderDetail_Order 
FOREIGN KEY (OrderId) REFERENCES _tb_Order(Id);

ALTER TABLE _tb_OrderDetail 
ADD CONSTRAINT FK_OrderDetail_Product 
FOREIGN KEY (ProductId) REFERENCES _tb_Product(Id);