USE [master]
GO
/****** Object:  Database [gac_wms]    Script Date: 4/19/2025 6:52:00 PM ******/
CREATE DATABASE [gac_wms]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'gac_wms', FILENAME = N'/var/opt/mssql/data/gac_wms.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'gac_wms_log', FILENAME = N'/var/opt/mssql/data/gac_wms_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [gac_wms] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [gac_wms].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [gac_wms] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [gac_wms] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [gac_wms] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [gac_wms] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [gac_wms] SET ARITHABORT OFF 
GO
ALTER DATABASE [gac_wms] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [gac_wms] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [gac_wms] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [gac_wms] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [gac_wms] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [gac_wms] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [gac_wms] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [gac_wms] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [gac_wms] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [gac_wms] SET  DISABLE_BROKER 
GO
ALTER DATABASE [gac_wms] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [gac_wms] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [gac_wms] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [gac_wms] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [gac_wms] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [gac_wms] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [gac_wms] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [gac_wms] SET RECOVERY FULL 
GO
ALTER DATABASE [gac_wms] SET  MULTI_USER 
GO
ALTER DATABASE [gac_wms] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [gac_wms] SET DB_CHAINING OFF 
GO
ALTER DATABASE [gac_wms] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [gac_wms] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [gac_wms] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [gac_wms] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'gac_wms', N'ON'
GO
ALTER DATABASE [gac_wms] SET QUERY_STORE = ON
GO
ALTER DATABASE [gac_wms] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [gac_wms]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/19/2025 6:52:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 4/19/2025 6:52:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Mobile] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 4/19/2025 6:52:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductCode] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Length] [float] NOT NULL,
	[Width] [float] NOT NULL,
	[Weight] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
	[SKU] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderHeaders]    Script Date: 4/19/2025 6:52:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderHeaders](
	[Id] [uniqueidentifier] NOT NULL,
	[ProcessingDate] [datetime2](7) NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_PurchaseOrderHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderLines]    Script Date: 4/19/2025 6:52:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderLines](
	[Id] [uniqueidentifier] NOT NULL,
	[PurchaseOrderHeaderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_PurchaseOrderLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrderHeaders]    Script Date: 4/19/2025 6:52:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderHeaders](
	[Id] [uniqueidentifier] NOT NULL,
	[ProcessingDate] [datetime2](7) NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[ShipmentAddressId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_SalesOrderHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrderLines]    Script Date: 4/19/2025 6:52:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderLines](
	[Id] [uniqueidentifier] NOT NULL,
	[SalesOrderHeaderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_SalesOrderLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipmentAddress]    Script Date: 4/19/2025 6:52:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentAddress](
	[Id] [uniqueidentifier] NOT NULL,
	[AddressLine] [nvarchar](200) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[State] [nvarchar](100) NOT NULL,
	[ZipCode] [nvarchar](20) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ShipmentAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Products_SKU]    Script Date: 4/19/2025 6:52:01 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Products_SKU] ON [dbo].[Products]
(
	[SKU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrderHeaders_CustomerId]    Script Date: 4/19/2025 6:52:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrderHeaders_CustomerId] ON [dbo].[PurchaseOrderHeaders]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrderLines_ProductId]    Script Date: 4/19/2025 6:52:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrderLines_ProductId] ON [dbo].[PurchaseOrderLines]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrderLines_PurchaseOrderHeaderId]    Script Date: 4/19/2025 6:52:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrderLines_PurchaseOrderHeaderId] ON [dbo].[PurchaseOrderLines]
(
	[PurchaseOrderHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SalesOrderHeaders_CustomerId]    Script Date: 4/19/2025 6:52:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaders_CustomerId] ON [dbo].[SalesOrderHeaders]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SalesOrderHeaders_ShipmentAddressId]    Script Date: 4/19/2025 6:52:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_SalesOrderHeaders_ShipmentAddressId] ON [dbo].[SalesOrderHeaders]
(
	[ShipmentAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SalesOrderLines_ProductId]    Script Date: 4/19/2025 6:52:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_SalesOrderLines_ProductId] ON [dbo].[SalesOrderLines]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SalesOrderLines_SalesOrderHeaderId]    Script Date: 4/19/2025 6:52:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_SalesOrderLines_SalesOrderHeaderId] ON [dbo].[SalesOrderLines]
(
	[SalesOrderHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShipmentAddress_CustomerId]    Script Date: 4/19/2025 6:52:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_ShipmentAddress_CustomerId] ON [dbo].[ShipmentAddress]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PurchaseOrderHeaders]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderHeaders_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseOrderHeaders] CHECK CONSTRAINT [FK_PurchaseOrderHeaders_Customers_CustomerId]
GO
ALTER TABLE [dbo].[PurchaseOrderLines]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderLines_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseOrderLines] CHECK CONSTRAINT [FK_PurchaseOrderLines_Products_ProductId]
GO
ALTER TABLE [dbo].[PurchaseOrderLines]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderLines_PurchaseOrderHeaders_PurchaseOrderHeaderId] FOREIGN KEY([PurchaseOrderHeaderId])
REFERENCES [dbo].[PurchaseOrderHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseOrderLines] CHECK CONSTRAINT [FK_PurchaseOrderLines_PurchaseOrderHeaders_PurchaseOrderHeaderId]
GO
ALTER TABLE [dbo].[SalesOrderHeaders]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeaders_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesOrderHeaders] CHECK CONSTRAINT [FK_SalesOrderHeaders_Customers_CustomerId]
GO
ALTER TABLE [dbo].[SalesOrderHeaders]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderHeaders_ShipmentAddress_ShipmentAddressId] FOREIGN KEY([ShipmentAddressId])
REFERENCES [dbo].[ShipmentAddress] ([Id])
GO
ALTER TABLE [dbo].[SalesOrderHeaders] CHECK CONSTRAINT [FK_SalesOrderHeaders_ShipmentAddress_ShipmentAddressId]
GO
ALTER TABLE [dbo].[SalesOrderLines]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLines_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesOrderLines] CHECK CONSTRAINT [FK_SalesOrderLines_Products_ProductId]
GO
ALTER TABLE [dbo].[SalesOrderLines]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLines_SalesOrderHeaders_SalesOrderHeaderId] FOREIGN KEY([SalesOrderHeaderId])
REFERENCES [dbo].[SalesOrderHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesOrderLines] CHECK CONSTRAINT [FK_SalesOrderLines_SalesOrderHeaders_SalesOrderHeaderId]
GO
ALTER TABLE [dbo].[ShipmentAddress]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentAddress_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShipmentAddress] CHECK CONSTRAINT [FK_ShipmentAddress_Customers_CustomerId]
GO
USE [master]
GO
ALTER DATABASE [gac_wms] SET  READ_WRITE 
GO
