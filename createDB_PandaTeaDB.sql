-- Run below script alone in a new query window first to created database for the first time
--CREATE DATABASE pandateadb


USE [pandateadb]
GO
/****** Object:  Table [dbo].[menuTbl]    Script Date: 2019-09-27 9:21:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[menuTbl](
	[menuId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[productId] [numeric](18, 0) NOT NULL,
	[size] [nchar](10) NULL,
	[price] [decimal](6, 2) NULL,
	[calories] [int] NULL,
 CONSTRAINT [PK_menuTbl] PRIMARY KEY CLUSTERED 
(
	[menuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productTbl]    Script Date: 2019-09-27 9:21:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productTbl](
	[productId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[productName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_productTbl] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reviewTbl]    Script Date: 2019-09-27 9:21:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reviewTbl](
	[reviewId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[userId] [numeric](18, 0) NOT NULL,
	[productId] [numeric](18, 0) NOT NULL,
	[score] [numeric](3, 1) NULL,
	[text] [nvarchar](max) NULL,
	[dateReviewed] [date] NULL,
 CONSTRAINT [PK_reviewTbl] PRIMARY KEY CLUSTERED 
(
	[reviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[storeTbl]    Script Date: 2019-09-27 9:21:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[storeTbl](
	[storeId] [numeric](18, 0) NOT NULL,
	[storeName] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[street] [nvarchar](50) NULL,
	[city] [nvarchar](50) NULL,
	[state] [nvarchar](50) NULL,
	[country] [nvarchar](50) NULL,
	[postalCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_storeTbl] PRIMARY KEY CLUSTERED 
(
	[storeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transactionTbl]    Script Date: 2019-09-27 9:21:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transactionTbl](
	[transactionId] [numeric](18, 0) NOT NULL,
	[userId] [numeric](18, 0) NOT NULL,
	[productId] [numeric](18, 0) NOT NULL,
	[storeId] [numeric](18, 0) NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [decimal](6, 2) NOT NULL,
	[datePurchased] [date] NULL,
 CONSTRAINT [PK_transactionTbl] PRIMARY KEY CLUSTERED 
(
	[transactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userTbl]    Script Date: 2019-09-27 9:21:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userTbl](
	[userId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](50) NULL,
	[lastName] [nvarchar](50) NULL,
	[phoneNumber] [varchar](20) NULL,
	[email] [nvarchar](50) NULL,
	[dateRegistered] [date] NULL,
	[password] [varchar](20) NULL,
 CONSTRAINT [PK_userTbl] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[menuTbl] ON 

INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), N'S         ', CAST(3.99 AS Decimal(6, 2)), 400)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), N'M         ', CAST(4.49 AS Decimal(6, 2)), 500)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), N'L         ', CAST(4.99 AS Decimal(6, 2)), 600)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(4 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), N'S         ', CAST(3.99 AS Decimal(6, 2)), 600)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(5 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), N'M         ', CAST(4.49 AS Decimal(6, 2)), 800)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(6 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), N'L         ', CAST(4.99 AS Decimal(6, 2)), 1000)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(7 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), N'S         ', CAST(3.99 AS Decimal(6, 2)), 500)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(8 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), N'M         ', CAST(4.49 AS Decimal(6, 2)), 600)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(9 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), N'L         ', CAST(4.99 AS Decimal(6, 2)), 700)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(10 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), N'S         ', CAST(3.99 AS Decimal(6, 2)), 400)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(11 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), N'M         ', CAST(4.49 AS Decimal(6, 2)), 500)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(12 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), N'L         ', CAST(4.99 AS Decimal(6, 2)), 600)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(13 AS Numeric(18, 0)), CAST(5 AS Numeric(18, 0)), N'S         ', CAST(3.99 AS Decimal(6, 2)), 300)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(14 AS Numeric(18, 0)), CAST(5 AS Numeric(18, 0)), N'M         ', CAST(4.49 AS Decimal(6, 2)), 400)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(15 AS Numeric(18, 0)), CAST(5 AS Numeric(18, 0)), N'L         ', CAST(4.99 AS Decimal(6, 2)), 500)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(16 AS Numeric(18, 0)), CAST(6 AS Numeric(18, 0)), N'S         ', CAST(3.99 AS Decimal(6, 2)), 300)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(17 AS Numeric(18, 0)), CAST(6 AS Numeric(18, 0)), N'M         ', CAST(4.49 AS Decimal(6, 2)), 400)
INSERT [dbo].[menuTbl] ([menuId], [productId], [size], [price], [calories]) VALUES (CAST(18 AS Numeric(18, 0)), CAST(6 AS Numeric(18, 0)), N'L         ', CAST(4.99 AS Decimal(6, 2)), 500)
SET IDENTITY_INSERT [dbo].[menuTbl] OFF
SET IDENTITY_INSERT [dbo].[productTbl] ON 

INSERT [dbo].[productTbl] ([productId], [productName]) VALUES (CAST(1 AS Numeric(18, 0)), N'Strawberry Explosion')
INSERT [dbo].[productTbl] ([productId], [productName]) VALUES (CAST(2 AS Numeric(18, 0)), N'Creamy Avocado')
INSERT [dbo].[productTbl] ([productId], [productName]) VALUES (CAST(3 AS Numeric(18, 0)), N'Taro Basic')
INSERT [dbo].[productTbl] ([productId], [productName]) VALUES (CAST(4 AS Numeric(18, 0)), N'Mango Delight')
INSERT [dbo].[productTbl] ([productId], [productName]) VALUES (CAST(5 AS Numeric(18, 0)), N'Zen  Matcha')
INSERT [dbo].[productTbl] ([productId], [productName]) VALUES (CAST(6 AS Numeric(18, 0)), N'Coffee Chill')
SET IDENTITY_INSERT [dbo].[productTbl] OFF
