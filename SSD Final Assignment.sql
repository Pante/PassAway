/*======================================================*/
/*  Created in May 2017                                 */
/*  DWA 2017 April Semester					            */
/*  Diploma in IT/FI                                    */
/*                                                      */
/*  Database Script for setting up the database         */
/*  required for DWA Assignment.                        */
/*======================================================*/

Create Database PassAway
GO

Use PassAway
GO

/***************************************************************/
/***           Delete tables before creating                 ***/
/***************************************************************/

/* Table: dbo.CashVoucher */
if exists (select * from sysobjects
  where id = object_id('dbo.CashVoucher') and sysstat & 0xf = 3)
  drop table dbo.CashVoucher
GO

/* Table: dbo.TransactionItem */
if exists (select * from sysobjects
  where id = object_id('dbo.TransactionItem') and sysstat & 0xf = 3)
  drop table dbo.TransactionItem
GO

/* Table: dbo.SalesTransaction */
if exists (select * from sysobjects
  where id = object_id('dbo.SalesTransaction') and sysstat & 0xf = 3)
  drop table dbo.SalesTransaction
GO

/* Table: dbo.Product */
if exists (select * from sysobjects
  where id = object_id('dbo.Product') and sysstat & 0xf = 3)
  drop table dbo.Product
GO

/* Table: dbo.Customer */
if exists (select * from sysobjects
  where id = object_id('dbo.Customer') and sysstat & 0xf = 3)
  drop table dbo.Customer
GO

/* Table: dbo.Staff */
if exists (select * from sysobjects
  where id = object_id('dbo.Staff') and sysstat & 0xf = 3)
  drop table dbo.Staff
GO

/***************************************************************/
/***                     Creating tables                     ***/
/***************************************************************/

/* Table: dbo.Staff */
CREATE TABLE dbo.Staff
(
  StaffID 	    varchar(20),
  SName			varchar(50) 	NOT NULL,
  SGender		char(1) 		NOT NULL	CHECK (SGender IN ('M','F')),
  SAppt			varchar(50) 	NOT NULL,
  STelNo		varchar(20)		NOT NULL,
  SEmailAddr	varchar(50) 	NOT NULL,
  SPassword		varchar(20)		NOT NULL,
  CONSTRAINT PK_Staff PRIMARY KEY CLUSTERED (StaffID)
)
GO

/* Table: dbo.Customer */
CREATE TABLE dbo.Customer
(
  MemberID 			char(9),
  MName				varchar(50) 	NOT NULL,
  MGender			char(1) 		NOT NULL	CHECK (MGender IN ('M','F')),
  MBirthDate		datetime		NOT NULL,
  MAddress			varchar(250)	NULL,
  MCountry			varchar(50)		NOT NULL,
  MTelNo			varchar(20)		NULL,
  MEmailAddr		varchar(50)		NULL,
  MPassword			varchar(20)		NOT NULL	DEFAULT ('AbC@123#'),
  CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (MemberID)
)
GO

/* Table: dbo.Product*/
CREATE TABLE dbo.Product
(
  ProductID 		int IDENTITY (1,1),
  ProductTitle  	varchar(255) 	NOT NULL,
  ProductImage		varchar(255) 	NULL,
  Price				money			NOT NULL,
  Type	  	varchar(255)		NOT NULL,
  Region      varchar(255)  NOT NULL,
  DateProduced  int    NULL,
  CONSTRAINT PK_Product PRIMARY KEY CLUSTERED (ProductID)
)
GO

/* Table: dbo.SalesTransaction */
CREATE TABLE dbo.SalesTransaction
(
 TransactionID		int IDENTITY (1,1),
 MemberID 			char(9)		NULL,
 SubTotal			money		NOT NULL DEFAULT (0),
 Tax				money		NOT NULL DEFAULT (0),
 DiscountPercent	float		NOT NULL DEFAULT (0),
 DiscountAmt		money		NOT NULL DEFAULT (0),
 Total				money		NOT NULL DEFAULT (0),
 DateCreated	 	datetime 	NOT NULL DEFAULT (getdate()),
 CONSTRAINT PK_SalesTransaction PRIMARY KEY CLUSTERED (TransactionID),
 CONSTRAINT FK_SalesTransaction_MemberID  FOREIGN KEY (MemberID)
 REFERENCES dbo.Customer(MemberID)
)
GO

/* Table: dbo.TransactionItem */
CREATE TABLE dbo.TransactionItem
(
  TransactionID		int  	NOT NULL,
  ProductID			int		NOT NULL,
  Price 			money	NOT NULL,
  Quantity 			int		NOT NULL,
  CONSTRAINT FK_TransactionItem_TransactionID FOREIGN KEY (TransactionID)
  REFERENCES dbo.SalesTransaction(TransactionID),
  CONSTRAINT FK_TransactionItem_ProductID  FOREIGN KEY (ProductID)
  REFERENCES dbo.Product(ProductID)
)
GO

/* Table: dbo.CashVoucher */
CREATE TABLE dbo.CashVoucher
(
  IssuingID			int IDENTITY (1,1),
  MonthIssuedFor	int			NOT NULL,
  YearIssuedFor		int			NOT NULL,
  DateTimeIssued	datetime	NOT NULL	DEFAULT (getdate()),
  VoucherSN			varchar(30)	NULL,
  [Status]			char(1)		NOT NULL	DEFAULT ('0')
					CHECK ([Status] IN ('0','1','2')),
  CONSTRAINT PK_CashVoucher PRIMARY KEY CLUSTERED (IssuingID),
  CONSTRAINT FK_CashVoucher_MemberID FOREIGN KEY (MemberID)
  REFERENCES dbo.Customer(MemberID)
)
GO


/***************************************************************/
/***              Load the tables with sample data           ***/
/***************************************************************/

/*****  Create records in Staff Table *****/
INSERT [dbo].[Staff] ([StaffID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('Staff001', 'Samatha Tan', 'F', 'Sales Personnel', '64561234', 'st@zzf.com.sg', 'passSales')
INSERT [dbo].[Staff] ([StaffID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('Staff002', 'Pinky Pander', 'F', 'Sales Personnel', '64561235', 'pp@zzf.com.sg', 'passSales')
INSERT [dbo].[Staff] ([StaffID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('Staff003', 'Edward Lee', 'M', 'Sales Personnel', '64561236', 'el@zzf.com.sg', 'passSales')
INSERT [dbo].[Staff] ([StaffID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('Manager', 'Jenifer Greenspan', 'F', 'Product Manager', '64561237', 'jg@zzf.com.sg', 'passProduct')
INSERT [dbo].[Staff] ([StaffID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('Staff004', 'Ali Imran', 'M', 'Sales Personnel', '64561238', 'ai@zzf.com.sg', 'passMarketing')

/*****  Create records in Customer Table *****/
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000001', 'Benjamin Bean', 'M', '05-May-1970', NULL, 'United Kingdom', '94609901', NULL, 'pass1234')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000002', 'Fatimah Bte Ahmad', 'F', '21-Jun-1992', '100, Bukit Timah Road', 'Singapore', '91234567', 'fa92@yahoo.com', 'AbC@123#')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000003', 'Peter Ghim', 'M', '31-Aug-1991', '203, Jalan Wong Ah Fok, Johor Bahru', 'Malaysia', '98765432', 'pg91@hotmail.com', 'pgPass')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000004', 'Xu Yazhi', 'F', '25-Dec-1980', NULL, 'China', NULL, 'xyz@np.edu.sg', 'xyz')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000005', 'Eliza Wong', 'F', '24-Jul-1993', 'Blk 123, #10-321, Hougang Ave 2', 'Singapore', NULL, NULL, 'pass1234')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000006', 'K Kannan', 'M', '12-Sep-1990', NULL, 'India', NULL, '20100134@np.edu.sg', 'pass1234')

/*****  Create records in Product Table *****/
SET IDENTITY_INSERT [dbo].[Product] ON
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [Price], [Type], [Region], [DateProduced]) VALUES (1, 'GAUCHEZCO-RESERVA MALBEC', '1381043712_1_1_3.jpg', 80.0000, 'Red Wine', 'ARGENTINA', 2014)
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [Price], [Type], [Region], [DateProduced]) VALUES (2, 'LICENCIDO-RIOJA RRESERVA', '2705273400_1_1_3.jpg', 90.0000, 'Red Wine', 'SPAIN', 2012)
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [Price], [Type], [Region], [DateProduced]) VALUES (3, 'WOLFBERGER-GEWURZTRAMINER VENDANGES TARDIVES', '5644031413_1_1_3.jpg', 60.0000, 'White Wine', 'FRANCE', '2011')
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [Price], [Type], [Region], [DateProduced]) VALUES (4, 'CHATEAU GRILLON-SAUTERNES', '2070239550_1_1_3.jpg', 35.0000, 'White Wine', 'FRANCE', 2012)
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [Price], [Type], [Region], [DateProduced]) VALUES (5, 'WESTEND ESTATE RICKLAND-MOSCATO', '0722437052_1_1_3.jpg', 30.0000, 'Sweet Wine', 'AUSTRALIA', 2014)
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [Price], [Type], [Region], [DateProduced]) VALUES (6, 'VACALHOA-MOSCATEL DESETUBAL', '0367420800_1_1_3.jpg', 40.0000, 'Sweet Wine', 'Portugal', 2012)
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [Price], [Type], [Region], [DateProduced]) VALUES (7, 'MILLESIMATO CAMASELLA DOC', '9815401617_1_1_3.jpg', 32.0000, 'Champagne', 'ITALY', 2015)
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [Price], [Type], [Region], [DateProduced]) VALUES (8, 'AZAHARA', '6917485615_1_1_3.jpg', 79.9000, 'Champagne', 'AUSTRALIA', NULL)
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [Price], [Type], [Region], [DateProduced]) VALUES (9, 'MANON-COTES DE PROVENCE', '2669795710_1_1_3.jpg', 70.0000, 'ROSE', 'FRANCE', 2015)
SET IDENTITY_INSERT [dbo].[Product] OFF

/*****  Create records in SalesTransaction Table *****/
SET IDENTITY_INSERT [dbo].[SalesTransaction] ON
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (1, 'M00000004', 536.7000, 37.5700, 10.0, 57.4300, 516.8400, '24-Apr-2017')
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (2, 'M00000005', 218.0000, 15.2600, 10.0, 23.3300, 209.9300, '25-Apr-2017')
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (3, 'M00000001', 357.0000, 24.9900, 25.0, 95.5000, 286.4900, '10-May-2017')
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (4, 'M00000002', 566.7000, 39.6700, 10.0, 60.6400, 545.6300, '15-May-2017')
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (5, 'M00000002', -69.9000, -4.8900, 10.0, -7.4800, -67.3100, '16-May-2017')
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (6, 'M00000001', 319.6000, 22.3700, 25.0, 85.4900, 256.4800, '27-May-2017')
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (7,  NULL, 79.9000, 5.5900, 0.0, 0.0000, 85.4900, '27-May-2017')
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (8, 'M00000003', 218.9000, 15.3200, 10.0, 23.4200, 210.8000, '30-May-2017')
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (9, 'M00000006', 79.9000, 5.5900, 10.0, 8.5500, 76.9400, '02-Jun-2017')
INSERT [dbo].[SalesTransaction] ([TransactionID], [MemberID], [SubTotal], [Tax], [DiscountPercent], [DiscountAmt], [Total],[DateCreated]) VALUES (10, 'M00000002', 251.6000, 17.6100, 25.0, 67.3000, 201.9100, '12-Jun-2017')
SET IDENTITY_INSERT [dbo].[SalesTransaction] OFF

/*****  Create records in TransactionItem Table *****/
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (1, 3, 69.9000, 3)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (1, 4, 109.0000, 3)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (2, 4, 109.0000, 2)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (3, 1, 139.0000, 1)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (3, 4, 109.0000, 2)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (4, 1, 139.0000, 1)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (4, 3, 69.9000, 2)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (4, 4, 109.0000, 2)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (6, 9, 79.9000, 4)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (7, 9, 79.9000, 1)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (8, 9, 79.9000, 1)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (8, 1, 139.0000, 1)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (9, 2, 79.9000, 1)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (10, 5, 45.9000, 2)
INSERT [dbo].[TransactionItem] ([TransactionID], [ProductID], [Price], [Quantity]) VALUES (10, 2, 79.9000, 2)

/*****  Create records in CashVoucher Table *****/
SET IDENTITY_INSERT [dbo].[CashVoucher] ON
INSERT [dbo].[CashVoucher] ([IssuingID], [MonthIssuedFor], [YearIssuedFor], [DateTimeIssued], [VoucherSN], [Status]) VALUES (1, 4, 2017, '01-May-2017', '2017-05-000001', '2')
INSERT [dbo].[CashVoucher] ([IssuingID], [MonthIssuedFor], [YearIssuedFor], [DateTimeIssued], [VoucherSN], [Status]) VALUES (2, 4, 2017, '01-May-2017', '2017-05-000002', '1')
INSERT [dbo].[CashVoucher] ([IssuingID], [MonthIssuedFor], [YearIssuedFor], [DateTimeIssued], [VoucherSN], [Status]) VALUES (3, 4, 2017, '01-May-2017', NULL, '0')
SET IDENTITY_INSERT [dbo].[CashVoucher] OFF
