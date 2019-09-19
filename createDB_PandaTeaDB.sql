DROP DATABASE IF EXISTS pandateadb
CREATE DATABASE pandateadb

USE [pandateadb]
GO
/****** Object:  Table [dbo].[productTbl]    Script Date: 2019-09-19 5:11:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productTbl](
	[productId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[productName] [nvarchar](50) NOT NULL,
	[size] [nchar](10) NULL,
	[price] [decimal](6, 2) NULL,
 CONSTRAINT [PK_productTbl] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reviewTbl]    Script Date: 2019-09-19 5:11:45 PM ******/
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
/****** Object:  Table [dbo].[storeTbl]    Script Date: 2019-09-19 5:11:45 PM ******/
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
/****** Object:  Table [dbo].[transactionTbl]    Script Date: 2019-09-19 5:11:45 PM ******/
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
/****** Object:  Table [dbo].[userTbl]    Script Date: 2019-09-19 5:11:45 PM ******/
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
 CONSTRAINT [PK_userTbl] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[productTbl] ON 

INSERT [dbo].[productTbl] ([productId], [productName], [size], [price]) VALUES (CAST(1 AS Numeric(18, 0)), N'Strawberry Explosion', N'S         ', CAST(3.99 AS Decimal(6, 2)))
INSERT [dbo].[productTbl] ([productId], [productName], [size], [price]) VALUES (CAST(2 AS Numeric(18, 0)), N'Strawberry Explosion', N'M         ', CAST(4.49 AS Decimal(6, 2)))
INSERT [dbo].[productTbl] ([productId], [productName], [size], [price]) VALUES (CAST(3 AS Numeric(18, 0)), N'Strawberry Explosion', N'L         ', CAST(4.99 AS Decimal(6, 2)))
SET IDENTITY_INSERT [dbo].[productTbl] OFF
SET IDENTITY_INSERT [dbo].[userTbl] ON 

INSERT [dbo].[userTbl] ([userId], [firstName], [lastName], [phoneNumber], [email], [dateRegistered]) VALUES (CAST(1 AS Numeric(18, 0)), N'Jaden', N'Ahn', N'2268993057', N'jahn2488@conestogac.on.ca', CAST(N'2019-09-19' AS Date))
SET IDENTITY_INSERT [dbo].[userTbl] OFF
