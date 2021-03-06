USE [GrubNow]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/5/2020 4:39:41 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Areas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AreaName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Heading] [nvarchar](max) NULL,
	[ImagesUrl] [nvarchar](max) NULL,
	[OtherImagesUrl] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuisines]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuisines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Cuisines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address_Location] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DriverWithAreas]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DriverWithAreas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AreaId] [int] NULL,
	[DriverId] [int] NULL,
 CONSTRAINT [PK_DriverWithAreas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OtherLocations]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtherLocations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [nvarchar](max) NOT NULL,
	[LocationAddress] [nvarchar](max) NOT NULL,
	[VendorID] [int] NOT NULL,
 CONSTRAINT [PK_OtherLocations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Discription] [nvarchar](max) NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[ProfilePic] [nvarchar](max) NULL,
	[DriverCar] [nvarchar](max) NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreName] [nvarchar](max) NULL,
	[CategoryId] [int] NULL,
	[Website_Url] [nvarchar](max) NULL,
	[NumberOfLocation] [nvarchar](max) NULL,
	[Address_Location] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NULL,
	[UniqueFileName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorWithAreas]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorWithAreas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AreaId] [int] NULL,
	[VendorId] [int] NULL,
 CONSTRAINT [PK_VendorWithAreas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorWithCuisines]    Script Date: 6/5/2020 4:39:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorWithCuisines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuisineId] [int] NULL,
	[VendorId] [int] NULL,
 CONSTRAINT [PK_VendorWithCuisines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200417002807_InitialDB', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200417102806_DriverWithArea', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200417104028_DriverWithAre', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200417224836_ProfilePic', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200508171541_AddManyTo', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200510173125_AddVendorWithCoisne', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200510182059_AddVendorLogo', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200510202940_OtherLocation', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200512135437_ChangeFirstname', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200601211538_InitialDB', N'3.1.4')
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (1, N'Apapa')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (2, N'Eko Atlantic')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (3, N'Ikoyi')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (4, N'Ikeja')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (5, N'Ilupeju')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (6, N'Maryland')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (7, N'Victoria Island')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (8, N'Lagos Island')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (9, N'Lekki')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (10, N'Surulere')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (11, N'Yaba')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (12, N'Other')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (13, N'Apapa Test')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (14, N'Apapa Test one')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (15, N'Ikoyi One')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (16, N'Lekki one ')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (17, N'Test Area New')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (21, N'Ebute - Metta')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (22, N'Test Area New 12')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (23, N'Aguda')
INSERT [dbo].[Areas] ([Id], [AreaName]) VALUES (24, N'Test 23')
SET IDENTITY_INSERT [dbo].[Areas] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Restaurants')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Grocery Stores')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Supermarket')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'Juice Bar')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (5, N'Salad Bar')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (6, N'Caterer')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (7, N'Chef')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (8, N'Other')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (9, N'eqeq')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (10, N'Test Category ')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Cuisines] ON 

INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (1, N'Nigerian')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (2, N'African')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (3, N'Breakfast')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (4, N'Burger')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (5, N'Chicken wings')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (6, N'Salads')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (7, N'Sandwiches')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (8, N'European')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (9, N'Pasta')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (10, N'Noodles')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (11, N'Seafood')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (12, N'Vegetarian')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (13, N'Chinese')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (14, N'Pizza')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (15, N'Asian')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (16, N'Indian')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (17, N'American')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (18, N'Bakery and Cakes')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (19, N'British')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (20, N'Soups')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (21, N'Swallows')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (22, N'Italian')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (23, N'Mediterranean')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (24, N'Mexican')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (25, N'Alcoholic Drinks')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (26, N'Champagne')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (27, N'Japanese')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (28, N'Thai')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (29, N'Caribbean')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (30, N'Ice Cream')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (31, N'Beverages')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (32, N'Coffee Shop')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (33, N'Doughnuts Shop')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (34, N'French')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (35, N'Healthy Food')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (36, N'Lebanese')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (37, N'Parfait')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (38, N'Healthy Food')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (39, N'Other')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (40, N'African Test')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (41, N'African Test one')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (42, N'Seafood One')
INSERT [dbo].[Cuisines] ([Id], [Name]) VALUES (43, N'sds')
SET IDENTITY_INSERT [dbo].[Cuisines] OFF
SET IDENTITY_INSERT [dbo].[Drivers] ON 

INSERT [dbo].[Drivers] ([Id], [Address_Location], [UserId]) VALUES (2, N'I-10 Markaz, Islamabad, Pakistan', NULL)
INSERT [dbo].[Drivers] ([Id], [Address_Location], [UserId]) VALUES (8, N'I-10 Markaz, Islamabad, Pakistan', N'7b024cc8-e6d4-4a55-96a4-4e3bd3cedb4c')
INSERT [dbo].[Drivers] ([Id], [Address_Location], [UserId]) VALUES (16, N'223, Elm Street, Lagos', N'41ac8717-5be8-4bb2-9754-c8d2431cea2f')
SET IDENTITY_INSERT [dbo].[Drivers] OFF
SET IDENTITY_INSERT [dbo].[DriverWithAreas] ON 

INSERT [dbo].[DriverWithAreas] ([Id], [AreaId], [DriverId]) VALUES (7, 1, 8)
INSERT [dbo].[DriverWithAreas] ([Id], [AreaId], [DriverId]) VALUES (8, 2, 8)
SET IDENTITY_INSERT [dbo].[DriverWithAreas] OFF
SET IDENTITY_INSERT [dbo].[OtherLocations] ON 

INSERT [dbo].[OtherLocations] ([Id], [LocationName], [LocationAddress], [VendorID]) VALUES (1, N'Location Name', N'Location Test', 8)
SET IDENTITY_INSERT [dbo].[OtherLocations] OFF
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discription], [Created]) VALUES (N'7c774b9b-a537-43f5-9a62-527a2d010ddc', N'Admin', N'ADMIN', N'1f88dc08-b408-4faa-ad94-ab6511b99176', N'Perform All Opration', CAST(N'2020-05-12T23:34:49.9954272' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discription], [Created]) VALUES (N'9b95b724-aec9-478f-bc38-a90721da7308', N'Driver', N'DRIVER', N'a44f9b69-3680-420f-977d-fffc45001860', N'Perform All Opration', CAST(N'2020-04-17T05:44:26.5953529' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discription], [Created]) VALUES (N'cd781277-9a9c-406a-815d-d42951f5e064', N'Vendor', N'VENDOR', N'34b643d3-222a-471f-8849-66ed0c1e98c1', N'Perform All Opration', CAST(N'2020-04-17T05:42:31.6101873' AS DateTime2))
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discription], [Created]) VALUES (N'feba521f-daad-4738-9460-5da4fb7424bc', N'User', N'USER', N'3cf616b7-345a-436e-8396-e631b1b7fc56', N'Perform All Opration', CAST(N'2020-04-17T05:31:38.7256299' AS DateTime2))
INSERT [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Facebook', N'2701943710077879', N'Facebook', N'308b807a-f720-4f46-ade9-d07b23eb04fd')
INSERT [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'101947861132609878477', N'Google', N'67c57650-238d-417c-a807-33fb91502d66')
INSERT [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'104156384356618578371', N'Google', N'450de000-f2b9-4e89-b4f1-6daf5a089266')
INSERT [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'114427983023842882554', N'Google', N'd6e03010-6d72-4729-b9bb-e0286ba400c4')
INSERT [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'118220694167528353884', N'Google', N'91bb2946-ecec-42a4-8abf-05528e385409')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'41ff50ce-996b-4c42-92cd-8d7671fe422c', N'7c774b9b-a537-43f5-9a62-527a2d010ddc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'41ac8717-5be8-4bb2-9754-c8d2431cea2f', N'9b95b724-aec9-478f-bc38-a90721da7308')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'7b024cc8-e6d4-4a55-96a4-4e3bd3cedb4c', N'9b95b724-aec9-478f-bc38-a90721da7308')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'0ae873ef-e9fc-409c-bb2e-44d20295a002', N'cd781277-9a9c-406a-815d-d42951f5e064')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'20eb7b75-1131-49c9-8a8a-4d36af600552', N'cd781277-9a9c-406a-815d-d42951f5e064')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'5404fa05-b0cd-402c-beb0-ee2382343ad5', N'cd781277-9a9c-406a-815d-d42951f5e064')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'e0dd0a30-7130-4fec-b2b8-216c1a3dac7f', N'cd781277-9a9c-406a-815d-d42951f5e064')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'034f6b6c-d941-4857-89f4-2ee444facb66', N'feba521f-daad-4738-9460-5da4fb7424bc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'67c57650-238d-417c-a807-33fb91502d66', N'feba521f-daad-4738-9460-5da4fb7424bc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'6ed1c971-b9a2-4f57-98cf-8b9b7d2415cb', N'feba521f-daad-4738-9460-5da4fb7424bc')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'a0bd7df6-73d0-49ee-89f9-28ea6b145579', N'feba521f-daad-4738-9460-5da4fb7424bc')
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'00a3c557-740c-417a-9fb8-573e67e00765', N'TestUserrfr@gmail.com', N'TESTUSERRFR@GMAIL.COM', N'TestUserrfr@gmail.com', N'TESTUSERRFR@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEBW8DZfOXO4DhH7ikU9b9qo7h7HBO+LC57yqdHQ/di1MSzvPLWmydBRyoKrfXGoyUg==', N'YTTJR73VD4DFOXVR2HLTSI3JGAZI7AT6', N'0e10d6d5-def8-4424-a15b-73554126c121', N'03461578803', 0, 0, NULL, 1, 0, N'testuser', N'Test', NULL, NULL, CAST(N'2020-05-13T11:23:14.3338831' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'034f6b6c-d941-4857-89f4-2ee444facb66', N'driver@gmail.com', N'DRIVER@GMAIL.COM', N'driver@gmail.com', N'DRIVER@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEHrlpCxyHbveyFzFKGPeqlNH3aguaxwx47HnBI/WAGJ4KNrHgOpsychetuWYk1wi+w==', N'RIBMACNH2M447BJPRNQG4JKI4YYCWCQQ', N'f6e885b6-7273-4c20-9fee-d47722f9b593', N'03461578803', 0, 0, NULL, 0, 0, N'13123132', N'Saleem', NULL, NULL, CAST(N'2020-05-12T22:59:53.7355813' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'07423435-dafb-4b26-a6d2-e606815452f3', N'TestUselokr@gmail.com', N'TESTUSELOKR@GMAIL.COM', N'TestUselokr@gmail.com', N'TESTUSELOKR@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJMmkCYwSuxnOqKE9DvRL72SpHm5kSb8iQPT2Yl/Uwx3GSJemE37jhFcpJdaqNsrFw==', N'CFOOAVC277R4K7HSSYQ2QCJV6E4T24WK', N'f2723bf1-50d5-49a1-b4c2-cdc09e33af7f', N'03461578803', 0, 0, NULL, 1, 0, N'TestUser', N'Saleem', NULL, NULL, CAST(N'2020-05-13T11:45:44.8322606' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'0ae873ef-e9fc-409c-bb2e-44d20295a002', N'TestVendor@gmail.com', N'TESTVENDOR@GMAIL.COM', N'TestVendor@gmail.com', N'TESTVENDOR@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAELwQVZXLfHpXev6XOZiltejLcCTWBtqXEy6+n8JMD5rdn8MXsklFprGbWwpP3GuE9w==', N'DJWHDJEIFLYLI54CQXB47E6HEIURWF3P', N'9cb0b5c9-6bdb-4cce-99a7-f15f9198476d', N'03461578803', 0, 0, NULL, 1, 0, N'Test ', N'Vendor', NULL, NULL, CAST(N'2020-05-13T01:48:50.1771199' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'0bef9b27-35cc-4bc9-af6e-6b2d742d8a97', N'dee1@gmail.com', N'DEE1@GMAIL.COM', N'dee1@gmail.com', N'DEE1@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEIZOc4uHWE2aBP1H6wrDG/1OleIXptrrIDGyA1BPnZlYhDBnWSj51IzlXtWtksvuFQ==', N'7ADMCBWRN6GJHOSMT765ODYEKH6QRPJE', N'1c670246-2c50-44a0-ab87-c4f8f380ed6e', N'09072011988', 0, 0, NULL, 1, 0, N'dee', N'sims', NULL, NULL, CAST(N'2020-05-13T12:08:29.0329350' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'1372bfc5-8f9c-48e0-9e8e-d067eacdc744', N'TestUsertr@gmail.com', N'TESTUSERTR@GMAIL.COM', N'TestUsertr@gmail.com', N'TESTUSERTR@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEMbnya8mwHEOQtv+ArfsHgalkqyVpunQHu6CL9I4zOSkPhZkOsefkWMZ8r1nBPi2DA==', N'O3LDOH2CBPZGRUYSUG7CMG35CUMP5QDD', N'd0ebb2ed-ba6c-4e03-9fd7-0521948b70e4', N'03461578803', 0, 0, NULL, 0, 0, N'13123132', N'Saleem', NULL, NULL, CAST(N'2020-05-11T01:07:35.9508452' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'20eb7b75-1131-49c9-8a8a-4d36af600552', N'Livetest1@gmail.com', N'LIVETEST1@GMAIL.COM', N'Livetest1@gmail.com', N'LIVETEST1@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEPv+7zpsesPf91LcGCGMfx7irSJTr3Mz5xP6Sa1lU733Hq1rx0pLU28umLpuCtt2cw==', N'NEM37ENS4SCDWZKHJFQ3XJWOU4GTXG5G', N'08bb0778-3259-4bd5-b1a0-ddbdc1828498', N'03461578803', 0, 0, NULL, 1, 0, N'Livetest1 one', N'Saleem', NULL, NULL, CAST(N'2020-05-13T01:37:52.0113512' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'237c85dc-f670-45af-ae31-062eee8f8707', N'TestUser321@gmail.com', N'TESTUSER321@GMAIL.COM', N'TestUser321@gmail.com', N'TESTUSER321@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKRMRrDH8nKaTh0jjChZPxO17ZIrSnRKLHRFOIjdJJN3C47E+vRjmIqrC30G/JE3eQ==', N'WOUFQGVQZMC5EYV6VBKPQJBZPCWCZF24', N'71e5a5c8-d006-4567-a839-b65b76248f09', N'03461578803', 0, 0, NULL, 1, 0, N'TestUser321', N'TestUser321', NULL, NULL, CAST(N'2020-04-17T15:52:05.9193741' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'285736aa-8562-4b51-8958-b048ddf272af', N'TestUmkser@gmail.com', N'TESTUMKSER@GMAIL.COM', N'TestUmkser@gmail.com', N'TESTUMKSER@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEALok7WwKgcMC2bT+bZg9faD5WsmqSR78UMezZ3SQ7+EsehY8TaLUBNhHcydBOMsrw==', N'Z7SYNPP3U2UZCPHD536G3QRVZSEM7OFP', N'cd74fe48-aff4-4baf-bd20-88c5ca5e1cad', N'03461578803', 0, 0, NULL, 1, 0, N'13123132', N'Test', NULL, NULL, CAST(N'2020-05-13T11:52:28.9264303' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'2b2647a7-4666-4018-a795-df28aece2b69', N'usamaahmeredd248@outlook.com', N'USAMAAHMEREDD248@OUTLOOK.COM', N'usamaahmeredd248@outlook.com', N'USAMAAHMEREDD248@OUTLOOK.COM', 1, N'AQAAAAEAACcQAAAAEGnqfmo9++ASGCywDDXYR5iDm2EsLpbm1w5x5Ui9b4PYL/aO7CgN2zBEd4rHCpystw==', N'5CFCZ4KQ6BGWDXN3DVVLXTGT4ISKDPMH', N'3b6a0148-11c2-406a-9744-7af45a728f41', N'03461578803', 0, 0, NULL, 1, 0, N'TestUser34', N'Test', NULL, NULL, CAST(N'2020-05-13T07:10:16.5358456' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'2b48f933-5aaa-4eb9-b92d-d6c174f89c50', N'Test@gmail.com', N'TEST@GMAIL.COM', N'Test@gmail.com', N'TEST@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAENhc6KTFG/DtJilDh4bgUAWMBIIbpf9tB5skMdiccBD7RPcb2McRIdjRF0FUVXodBg==', N'NSJDBI2RANPHZUB4BHNMCJOGT3GAX6JN', N'beb71cad-5a61-4d99-8aa4-a0a041fc63c8', NULL, 0, 0, NULL, 0, 0, NULL, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'308b807a-f720-4f46-ade9-d07b23eb04fd', N'nadeem.sa.2582@gmail.com', N'NADEEM.SA.2582@GMAIL.COM', N'nadeem.sa.2582@gmail.com', N'NADEEM.SA.2582@GMAIL.COM', 1, NULL, N'K4OAXMQ77UM2HLJXXOINZUFNJSAMU65N', N'2c45a1c1-6b1a-40d9-a486-1726a5c3dabf', NULL, 0, 0, NULL, 1, 0, NULL, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'41ac8717-5be8-4bb2-9754-c8d2431cea2f', N'eskayclothingspree@gmail.com', N'ESKAYCLOTHINGSPREE@GMAIL.COM', N'eskayclothingspree@gmail.com', N'ESKAYCLOTHINGSPREE@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEG9RGAp0JbezcAOmHhxSc97/+V7/hILjbIpSeq0UsZgJj4y8pcEH29gaITy1/4bv/g==', N'2BT27AOPXBGESEGMHOJU6X55PICRKUJN', N'8e0c0d7c-4f67-4895-a7b1-40fbd4641d08', N'08175973079', 0, 0, NULL, 1, 0, N'glory 2', N'glory', NULL, NULL, CAST(N'2020-05-18T00:52:37.5558740' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'41ff50ce-996b-4c42-92cd-8d7671fe422c', N'gabod2000@yahoo.com', N'GABOD2000@YAHOO.COM', N'gabod2000@yahoo.com', N'GABOD2000@YAHOO.COM', 1, N'AQAAAAEAACcQAAAAEKVq62dERwq7P/dSApSL4A/QP69IJLoZEGFn28MOCuVg0CogHLfXOT0Uojw9b/s82A==', N'FZW3OWVCZCIOEV3NFML7JGPEXMJJPX2T', N'ee8d3ad5-8144-4edb-bec6-93904eb75aa0', N'03461578803', 0, 0, NULL, 1, 0, N'gabod2000', N'Admin', N'2020-05-16_2301.png', N'2020-05-17_2102.png', CAST(N'2020-05-12T23:34:45.8822842' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'4494de1f-80ad-4682-82c5-9f4ee26397b4', N'TestUserkjlk@gmail.com', N'TESTUSERKJLK@GMAIL.COM', N'TestUserkjlk@gmail.com', N'TESTUSERKJLK@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAELvhM062/ksa//JXlKwYaCBsHmZ5YvTl6OlIOrVPwGtPM14i3VAvzjsXCISEN57Hlw==', N'NGPYN7MWZ4KLPW4XXZSKKV3ZBHTOHIKX', N'30297fa3-2804-4a45-a191-33165bc54201', N'03461578803', 0, 0, NULL, 1, 0, N'TestUser', N'Saleem', NULL, NULL, CAST(N'2020-05-13T07:31:10.2632699' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'450de000-f2b9-4e89-b4f1-6daf5a089266', N'nadeem.s.2582@gmail.com', N'NADEEM.S.2582@GMAIL.COM', N'nadeem.s.2582@gmail.com', N'NADEEM.S.2582@GMAIL.COM', 1, NULL, N'UENYQDRATQROJ5JKGN26B2WRIUIH4OEO', N'86d737ea-eb1d-4975-99d7-2d5f04d23331', NULL, 0, 0, NULL, 0, 0, NULL, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'5404fa05-b0cd-402c-beb0-ee2382343ad5', N'TestUser1092@gmail.com', N'TESTUSER1092@GMAIL.COM', N'TestUser1092@gmail.com', N'TESTUSER1092@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEFZ/1d4jj1guFJFpcpzjDYdi2s5fTLeoewyRUxXnlyyZFWEFrBewwPRsjWe4P/ZJBw==', N'DZS3QP5I7AGL4RVIGPP6HCH47XNMOZU7', N'4e2c0eb7-6efd-4571-a665-ac52f29d2697', N'03461578803', 0, 0, NULL, 1, 0, N'TestUser', N'Saleem', NULL, NULL, CAST(N'2020-05-13T18:08:26.6350987' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'5b848ec7-a037-4a21-a055-486ef9e528a5', N'bosedele22@gmail.com', N'BOSEDELE22@GMAIL.COM', N'bosedele22@gmail.com', N'BOSEDELE22@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEAvnkLqjOWmY0BhV3+TDZk+WUOFYcIs0Qiwv8SZEqgZrz4ZmJu9IRGo8kr/NTkkziA==', N'2MUS3CKUZPGIMQSGBLFFHAQA4AWKJJWS', N'8ed31129-0b00-4d44-af0f-994faab2c68d', N'0706821655', 0, 0, NULL, 1, 0, N'bose', N'dele', NULL, NULL, CAST(N'2020-05-14T07:36:19.0805886' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'5d921e98-873e-4040-bece-79c0987a0897', N'TestUser1652@gmail.com', N'TESTUSER1652@GMAIL.COM', N'TestUser1652@gmail.com', N'TESTUSER1652@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEOT0S83auwGvFBvArQKGRlzNtoiMTfTHX6o1V47J8hGhjUFK2lTk/N5kdf4CeP2IwA==', N'P5D6YCDKCDAVY5XRZZQKPQPPE253W2DU', N'a92d4d7f-cf94-4a8a-afa2-7bfdf5cca05e', N'03461578803', 0, 0, NULL, 1, 0, N'Test12', N'User', NULL, NULL, CAST(N'2020-05-12T14:20:36.0625497' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'67c57650-238d-417c-a807-33fb91502d66', N'gabod2000@gmail.com', N'GABOD2000@GMAIL.COM', N'gabod2000@gmail.com', N'GABOD2000@GMAIL.COM', 1, NULL, N'6AMA6DNCWTOTQ3WIATSZ3MBGQTZQ7UCT', N'323018c4-d4d4-4233-8176-f4b956a4d08a', N'03461578803', 0, 0, NULL, 1, 0, N'gabod200012', N'Test One one', NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'6ed1c971-b9a2-4f57-98cf-8b9b7d2415cb', N'aali.97@yahoo.com', N'AALI.97@YAHOO.COM', N'aali.97@yahoo.com', N'AALI.97@YAHOO.COM', 1, N'AQAAAAEAACcQAAAAEJn2r4YS6ua2avnMOowcRkzK45KEAF6D4XI9mvbFSIUssW9XYFdXXP44OjUhSddYpA==', N'RTRM5GRD6VQDSKWMLRYVNVD5W2FBGSFU', N'8b0f8562-e94b-4bf1-b96b-9f4107396bfd', N'+9211111111', 0, 0, NULL, 1, 0, N'Ahmed', N'Ali', NULL, NULL, CAST(N'2020-05-28T09:49:38.6872933' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'7b024cc8-e6d4-4a55-96a4-4e3bd3cedb4c', N'TestUser@gmail.com', N'TESTUSER@GMAIL.COM', N'TestUser@gmail.com', N'TESTUSER@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAENhc6KTFG/DtJilDh4bgUAWMBIIbpf9tB5skMdiccBD7RPcb2McRIdjRF0FUVXodBg==', N'XFSZU2QC2YVMSBYEOYYF6HKHL4TEO2X3', N'f6765320-5873-413f-b16a-aac23ef6d92e', N'03461578803', 0, 0, NULL, 0, 0, N'TestUser', N'Saleem', NULL, NULL, CAST(N'2020-05-12T22:32:18.4978057' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'7bf9afe7-e3c2-401e-a4c9-86b3f8b3e205', N'TestUser1ol2@gmail.com', N'TESTUSER1OL2@GMAIL.COM', N'TestUser1ol2@gmail.com', N'TESTUSER1OL2@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJc/5kooaNCnmNkBGbh/bxHAXWRUoM71eOEUPeKEJyUNQd1rBdEj8BMvyFw/WKDWtw==', N'Y2YXOGLGPKGCDQBQGTMB6XJBZEC3SJYQ', N'8e11fb9a-5772-4cd2-aba0-cf1eb593dd09', N'03461578803', 0, 0, NULL, 1, 0, N'TestUser32', N'Saleem', NULL, NULL, CAST(N'2020-05-13T05:56:35.8693794' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'91bb2946-ecec-42a4-8abf-05528e385409', N'automotivecorp786@gmail.com', N'AUTOMOTIVECORP786@GMAIL.COM', N'automotivecorp786@gmail.com', N'AUTOMOTIVECORP786@GMAIL.COM', 1, NULL, N'PBSABAPDLSC7L7REUT5DHAGOKCVSCKTT', N'227e77bd-5bc4-4ddf-b98d-025730e6e658', NULL, 0, 0, NULL, 1, 0, NULL, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'a0bd7df6-73d0-49ee-89f9-28ea6b145579', N'TestUser12@gmail.com', N'TESTUSER12@GMAIL.COM', N'TestUser12@gmail.com', N'TESTUSER12@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEP8M8Otn0S70JaOWMIs+fomIFA7gHIKx0U3Eg8eEWxU2jf2goNyiZC7pSwa7QEL7DQ==', N'SMQMILNWIAVSSJME2SXXWFAQ6OSDJJ7E', N'9e0ec086-0f3a-4405-8733-c4aecc395a34', N'03461578803', 0, 0, NULL, 1, 0, N'Waseem', N'Saleem', NULL, NULL, CAST(N'2020-05-12T23:08:05.7550678' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'a58c6166-c5ad-4c8f-b944-591e169cbf90', N'usamaahmeredkod248@outlook.com', N'USAMAAHMEREDKOD248@OUTLOOK.COM', N'usamaahmeredkod248@outlook.com', N'USAMAAHMEREDKOD248@OUTLOOK.COM', 1, N'AQAAAAEAACcQAAAAEGiqbT25IGB8Uay1B/R3HQgj3+GafyjAUXICOgrUeKVeQ1Y1X9/9BFP0W08M8RWjqg==', N'KDNLHCSSKDSUJC7WUGXQW7Q6J26Z3KXE', N'26f37bf1-89be-418e-b323-328e59960962', N'03461578803', 0, 0, NULL, 1, 0, N'TestUser34', N'Test', NULL, NULL, CAST(N'2020-05-13T07:15:41.9664917' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'b403112b-159f-42c6-80a0-227f16452034', N'TestUser54@gmail.com', N'TESTUSER54@GMAIL.COM', N'TestUser54@gmail.com', N'TESTUSER54@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEDn2LkpQJ1bHKsGiVxdBR1W5fBNuU/lt3s17QfAwhHrzmWVUxs2XRx7v927P1dWf4w==', N'BRRNVLUZTDV4F5JKFFJM6YZ7HUYKGRQG', N'1b0edfb2-2b06-44ec-8d38-9b257a781958', N'03461578803', 0, 0, NULL, 1, 0, N'13123132', N'Saleem', NULL, NULL, CAST(N'2020-05-12T13:22:48.3308802' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'b9b4c2bd-dd8e-48b0-a8c4-5c2878719bd7', N'usamaahmeredkjd248@outlook.com', N'USAMAAHMEREDKJD248@OUTLOOK.COM', N'usamaahmeredkjd248@outlook.com', N'USAMAAHMEREDKJD248@OUTLOOK.COM', 1, N'AQAAAAEAACcQAAAAEBvmiVyOzIkd4QDy7sSHWBSS1bhyp7SKQZ+u3uA1VvAUhJiTWd42icA4fT/Ll9/xUQ==', N'33N44PUHGCVJ57PGPSC2XBZZQ3J3F3NH', N'df11eb96-c0df-4a2f-bae4-17c27e3a453f', N'03461578803', 0, 0, NULL, 1, 0, N'TestUser34', N'Test', NULL, NULL, CAST(N'2020-05-13T07:12:58.4483719' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'bc5fa698-c805-46ce-b3a6-5f027b695faa', N'dee@gmail.com', N'DEE@GMAIL.COM', N'dee@gmail.com', N'DEE@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJz+GjkXocuMvYu48+Fm/5BnSnzK32JIKyyrPvWAPNvTO+NG67e99ZMHmaIfWUkWnQ==', N'4MZGA4RNJMOXCQOIZIS4LXXFWD4POJV3', N'd080a41a-3ceb-46d2-a954-d4665b523327', N'09072011988', 0, 0, NULL, 1, 0, N'dee', N'sims', NULL, NULL, CAST(N'2020-05-13T12:06:24.7734844' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'd6e03010-6d72-4729-b9bb-e0286ba400c4', N'experisphere@gmail.com', N'EXPERISPHERE@GMAIL.COM', N'experisphere@gmail.com', N'EXPERISPHERE@GMAIL.COM', 1, NULL, N'PQTVBUIHS3FC7P4RV34R3OALYYX3YXZT', N'517f2232-ac6e-4a06-935c-dd974242a13d', NULL, 0, 0, NULL, 1, 0, NULL, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'e0dd0a30-7130-4fec-b2b8-216c1a3dac7f', N'TestUser10k92@gmail.com', N'TESTUSER10K92@GMAIL.COM', N'TestUser10k92@gmail.com', N'TESTUSER10K92@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEDA+BIqqSjbLVJf+i6PQJ6r5NJx7p4cBPzyhRL0XJa3Cos4NRpVZmAt2K05PcSwM+g==', N'2BLO7FTVDNRXC4ZYNKIYEFFAWF3WXTZP', N'8bb0dc56-ed2f-4a83-941d-4bc31c240756', N'03461578803', 0, 0, NULL, 1, 0, N'Driver Test 12', N'Test One', NULL, NULL, CAST(N'2020-05-18T15:42:06.0329696' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'e909bd7c-2bf4-48f1-b168-934370df28ba', N'TestUsertrre@gmail.com', N'TESTUSERTRRE@GMAIL.COM', N'TestUsertrre@gmail.com', N'TESTUSERTRRE@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAENhc6KTFG/DtJilDh4bgUAWMBIIbpf9tB5skMdiccBD7RPcb2McRIdjRF0FUVXodBg==', N'5IDFAUOCPEV42MU7CSZSVFUFNWYXWZNX', N'9d4c625d-0198-4495-afbc-428f5a49ac89', N'03461578803', 0, 0, NULL, 0, 0, N'13123132', N'Saleem', NULL, NULL, CAST(N'2020-05-11T01:08:47.7577545' AS DateTime2))
INSERT [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePic], [DriverCar], [Created]) VALUES (N'fea89d7a-0cc3-4496-bb36-0c61fe9f3951', N'driver467@gmail.com', N'DRIVER467@GMAIL.COM', N'driver467@gmail.com', N'DRIVER467@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAELeCURXq94ZaJuhPyIp8441wxxd9eNBZag061km3H7WpNbNCb3PnsfHv6FJdJqgdog==', N'PFW6YGSRU2JQN4D6TVQ6RNJ2ML6DL3UG', N'e39babfe-e420-46c4-9970-149f8dba4df5', N'03461578803', 0, 0, NULL, 1, 0, N'Driver32', N'Saleem', NULL, NULL, CAST(N'2020-05-12T14:23:21.6633134' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Vendors] ON 

INSERT [dbo].[Vendors] ([Id], [StoreName], [CategoryId], [Website_Url], [NumberOfLocation], [Address_Location], [UserId], [UniqueFileName]) VALUES (7, N'Rail Caffe', 1, N'https://www.google.com', N'1 - 4', N'I-10 Markaz, Islamabad, Pakistan', N'20eb7b75-1131-49c9-8a8a-4d36af600552', N'pexels-photo-1267320.jpeg')
INSERT [dbo].[Vendors] ([Id], [StoreName], [CategoryId], [Website_Url], [NumberOfLocation], [Address_Location], [UserId], [UniqueFileName]) VALUES (8, N'Nafees', 1, N'https://www.google.com', N'4 - 10', N'I-10 Markaz, Islamabad, Pakistan', N'0ae873ef-e9fc-409c-bb2e-44d20295a002', N'pexels-photo-1307698.jpeg')
INSERT [dbo].[Vendors] ([Id], [StoreName], [CategoryId], [Website_Url], [NumberOfLocation], [Address_Location], [UserId], [UniqueFileName]) VALUES (10, N'Monall', 1, N'https://www.google.com', N'10 - 20', N'I-10 Markaz, Islamabad, Pakistan', N'5404fa05-b0cd-402c-beb0-ee2382343ad5', N'pexels-photo-460537.jpeg')
INSERT [dbo].[Vendors] ([Id], [StoreName], [CategoryId], [Website_Url], [NumberOfLocation], [Address_Location], [UserId], [UniqueFileName]) VALUES (11, N'Maga Mart', 1, N'https://www.google.com', N'4 - 10', N'I-10 Markaz, Islamabad, Pakistan', N'e0dd0a30-7130-4fec-b2b8-216c1a3dac7f', N'pexels-photo-262978.jpeg')
SET IDENTITY_INSERT [dbo].[Vendors] OFF
SET IDENTITY_INSERT [dbo].[VendorWithAreas] ON 

INSERT [dbo].[VendorWithAreas] ([Id], [AreaId], [VendorId]) VALUES (7, 2, 7)
INSERT [dbo].[VendorWithAreas] ([Id], [AreaId], [VendorId]) VALUES (8, 3, 7)
INSERT [dbo].[VendorWithAreas] ([Id], [AreaId], [VendorId]) VALUES (9, 2, 8)
INSERT [dbo].[VendorWithAreas] ([Id], [AreaId], [VendorId]) VALUES (10, 3, 8)
INSERT [dbo].[VendorWithAreas] ([Id], [AreaId], [VendorId]) VALUES (13, 1, 10)
INSERT [dbo].[VendorWithAreas] ([Id], [AreaId], [VendorId]) VALUES (14, 2, 10)
INSERT [dbo].[VendorWithAreas] ([Id], [AreaId], [VendorId]) VALUES (15, 2, 11)
SET IDENTITY_INSERT [dbo].[VendorWithAreas] OFF
SET IDENTITY_INSERT [dbo].[VendorWithCuisines] ON 

INSERT [dbo].[VendorWithCuisines] ([Id], [CuisineId], [VendorId]) VALUES (7, 2, 7)
INSERT [dbo].[VendorWithCuisines] ([Id], [CuisineId], [VendorId]) VALUES (8, 3, 7)
INSERT [dbo].[VendorWithCuisines] ([Id], [CuisineId], [VendorId]) VALUES (9, 2, 8)
INSERT [dbo].[VendorWithCuisines] ([Id], [CuisineId], [VendorId]) VALUES (10, 3, 8)
INSERT [dbo].[VendorWithCuisines] ([Id], [CuisineId], [VendorId]) VALUES (13, 1, 10)
INSERT [dbo].[VendorWithCuisines] ([Id], [CuisineId], [VendorId]) VALUES (14, 2, 10)
INSERT [dbo].[VendorWithCuisines] ([Id], [CuisineId], [VendorId]) VALUES (15, 3, 11)
INSERT [dbo].[VendorWithCuisines] ([Id], [CuisineId], [VendorId]) VALUES (16, 5, 11)
SET IDENTITY_INSERT [dbo].[VendorWithCuisines] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Drivers_UserId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_Drivers_UserId] ON [dbo].[Drivers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DriverWithAreas_AreaId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_DriverWithAreas_AreaId] ON [dbo].[DriverWithAreas]
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DriverWithAreas_DriverId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_DriverWithAreas_DriverId] ON [dbo].[DriverWithAreas]
(
	[DriverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OtherLocations_VendorID]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_OtherLocations_VendorID] ON [dbo].[OtherLocations]
(
	[VendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleClaims_RoleId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId] ON [dbo].[RoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Roles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserClaims_UserId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId] ON [dbo].[UserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserLogins_UserId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId] ON [dbo].[UserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[Users]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[Users]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Vendors_CategoryId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_Vendors_CategoryId] ON [dbo].[Vendors]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Vendors_UserId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_Vendors_UserId] ON [dbo].[Vendors]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VendorWithAreas_AreaId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_VendorWithAreas_AreaId] ON [dbo].[VendorWithAreas]
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VendorWithAreas_VendorId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_VendorWithAreas_VendorId] ON [dbo].[VendorWithAreas]
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VendorWithCuisines_VendorId]    Script Date: 6/5/2020 4:39:41 AM ******/
CREATE NONCLUSTERED INDEX [IX_VendorWithCuisines_VendorId] ON [dbo].[VendorWithCuisines]
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_Users_UserId]
GO
ALTER TABLE [dbo].[DriverWithAreas]  WITH CHECK ADD  CONSTRAINT [FK_DriverWithAreas_Areas_AreaId] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Areas] ([Id])
GO
ALTER TABLE [dbo].[DriverWithAreas] CHECK CONSTRAINT [FK_DriverWithAreas_Areas_AreaId]
GO
ALTER TABLE [dbo].[DriverWithAreas]  WITH CHECK ADD  CONSTRAINT [FK_DriverWithAreas_Drivers_DriverId] FOREIGN KEY([DriverId])
REFERENCES [dbo].[Drivers] ([Id])
GO
ALTER TABLE [dbo].[DriverWithAreas] CHECK CONSTRAINT [FK_DriverWithAreas_Drivers_DriverId]
GO
ALTER TABLE [dbo].[OtherLocations]  WITH CHECK ADD  CONSTRAINT [FK_OtherLocations_Vendors_VendorID] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OtherLocations] CHECK CONSTRAINT [FK_OtherLocations_Vendors_VendorID]
GO
ALTER TABLE [dbo].[RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaims] CHECK CONSTRAINT [FK_RoleClaims_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
ALTER TABLE [dbo].[Vendors]  WITH CHECK ADD  CONSTRAINT [FK_Vendors_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Vendors] CHECK CONSTRAINT [FK_Vendors_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Vendors]  WITH CHECK ADD  CONSTRAINT [FK_Vendors_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Vendors] CHECK CONSTRAINT [FK_Vendors_Users_UserId]
GO
ALTER TABLE [dbo].[VendorWithAreas]  WITH CHECK ADD  CONSTRAINT [FK_VendorWithAreas_Areas_AreaId] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Areas] ([Id])
GO
ALTER TABLE [dbo].[VendorWithAreas] CHECK CONSTRAINT [FK_VendorWithAreas_Areas_AreaId]
GO
ALTER TABLE [dbo].[VendorWithAreas]  WITH CHECK ADD  CONSTRAINT [FK_VendorWithAreas_Vendors_VendorId] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendors] ([Id])
GO
ALTER TABLE [dbo].[VendorWithAreas] CHECK CONSTRAINT [FK_VendorWithAreas_Vendors_VendorId]
GO
ALTER TABLE [dbo].[VendorWithCuisines]  WITH CHECK ADD  CONSTRAINT [FK_VendorWithCuisines_Cuisines_VendorId] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Cuisines] ([Id])
GO
ALTER TABLE [dbo].[VendorWithCuisines] CHECK CONSTRAINT [FK_VendorWithCuisines_Cuisines_VendorId]
GO
ALTER TABLE [dbo].[VendorWithCuisines]  WITH CHECK ADD  CONSTRAINT [FK_VendorWithCuisines_Vendors_VendorId] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendors] ([Id])
GO
ALTER TABLE [dbo].[VendorWithCuisines] CHECK CONSTRAINT [FK_VendorWithCuisines_Vendors_VendorId]

