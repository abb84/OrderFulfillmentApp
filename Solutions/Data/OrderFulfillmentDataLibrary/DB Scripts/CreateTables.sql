USE [OrderFulfillment]
GO

/****** Object:  Table [dbo].[Order]    Script Date: 6/5/2016 10:59:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Order](
	[OrderNumber] [int] IDENTITY(1,1) NOT NULL,
	[BillingFullName] [varchar](50) NULL,
	[BillingCompany] [varchar](50) NULL,
	[BillingEmail] [varchar](50) NULL,
	[BillingAddress] [varchar](max) NULL,
	[BillingAddress2] [varchar](max) NULL,
	[BillingCity] [varchar](50) NULL,
	[BillingState] [varchar](50) NULL,
	[BillingZip] [varchar](50) NULL,
	[BillingCountry] [varchar](50) NULL,
	[BillingPhone] [varchar](50) NULL,
	[ProductTotal] [decimal](9, 2) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[OrderItem](
	[OrderNumber] [int] NOT NULL,
	[ItemNumber] [int] NOT NULL,
	[ItemName] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[PricePerUnit] [decimal](9, 2) NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[OrderNumber] ASC,
	[ItemNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
