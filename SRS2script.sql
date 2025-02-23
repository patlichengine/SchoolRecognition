USE [master]
GO
/****** Object:  Database [SchoolRecognition]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE DATABASE [SchoolRecognition]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolRecognition', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\SchoolRecognition.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SchoolRecognition_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\SchoolRecognition_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SchoolRecognition] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolRecognition].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolRecognition] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolRecognition] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolRecognition] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolRecognition] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolRecognition] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolRecognition] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SchoolRecognition] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolRecognition] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolRecognition] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolRecognition] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolRecognition] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolRecognition] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolRecognition] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolRecognition] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolRecognition] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SchoolRecognition] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolRecognition] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolRecognition] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolRecognition] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolRecognition] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolRecognition] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SchoolRecognition] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolRecognition] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SchoolRecognition] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolRecognition] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolRecognition] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolRecognition] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolRecognition] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SchoolRecognition] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SchoolRecognition] SET QUERY_STORE = OFF
GO
USE [SchoolRecognition]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SchoolRecognition]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23-Apr-20 12:08:27 PM ******/
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
/****** Object:  Table [dbo].[AuditTrail]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditTrail](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[Description] [ntext] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_AuditTrail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Centres]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Centres](
	[ID] [uniqueidentifier] NOT NULL,
	[CentreNo] [nvarchar](7) NOT NULL,
	[CentreName] [nvarchar](100) NOT NULL,
	[SchoolCategoryID] [uniqueidentifier] NULL,
	[CentreImage] [varbinary](max) NULL,
	[Longitude] [float] NULL,
	[Latitude] [float] NULL,
 CONSTRAINT [PK_Centres] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DBLogger]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DBLogger](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ErrorMsg] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_DBLogger] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalGovernments]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalGovernments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](3) NULL,
	[StateID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LocalGovernments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocationTypes]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationTypes](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_LocationTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Offices]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offices](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[StateID] [uniqueidentifier] NULL,
	[DateCreated] [date] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[OfficeImage] [varbinary](max) NULL,
	[Longitute] [float] NULL,
	[Latitude] [float] NULL,
	[OfficeTypeID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Offices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OfficeTypes]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfficeTypes](
	[ID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_OfficeTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PinHistories]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PinHistories](
	[ID] [uniqueidentifier] NOT NULL,
	[SchoolID] [uniqueidentifier] NULL,
	[PinID] [uniqueidentifier] NULL,
	[DateActive] [date] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PinHistories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pins]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pins](
	[ID] [uniqueidentifier] NOT NULL,
	[RecognitionTypeID] [uniqueidentifier] NULL,
	[SerialPin] [nvarchar](25) NULL,
	[IsActive] [bit] NOT NULL,
	[IsInUse] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[DateCreated] [date] NULL,
 CONSTRAINT [PK_Pins] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ranks]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ranks](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](10) NULL,
 CONSTRAINT [PK_Ranks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RecognitionTypes]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecognitionTypes](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [varchar](3) NULL,
 CONSTRAINT [PK_RecognitionTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsDefault] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsSuperAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SchoolCategories]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolCategories](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](2) NULL,
 CONSTRAINT [PK_SchoolCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SchoolPayments]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolPayments](
	[ID] [uniqueidentifier] NOT NULL,
	[PinID] [uniqueidentifier] NULL,
	[SchoolID] [uniqueidentifier] NULL,
	[Amount] [decimal](18, 0) NULL,
	[ReceiptNo] [nvarchar](50) NULL,
	[ReceiptImage] [varbinary](max) NULL,
	[DateCreated] [date] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SchoolPayments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Schools]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schools](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CategoryID] [uniqueidentifier] NULL,
	[OfficeID] [uniqueidentifier] NULL,
	[LgID] [uniqueidentifier] NULL,
	[Address] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](20) NULL,
	[YearEstablished] [bigint] NULL,
 CONSTRAINT [PK_Schools] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SecurityConfig]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityConfig](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SupportedBrowsers] [varchar](500) NOT NULL,
	[LocalDataRetentionPeriod] [int] NOT NULL,
	[OrganisationName] [varchar](500) NOT NULL,
	[Logo] [varbinary](max) NULL,
	[Address] [varchar](500) NOT NULL,
	[ClientAllowedIP] [varchar](max) NULL,
	[LastUpdateDate] [datetime] NULL,
	[SupportedClientVersion] [varchar](50) NULL,
 CONSTRAINT [PK_SecurityConfig] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[States]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](3) NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Titles]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Titles](
	[ID] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_Titles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 23-Apr-20 12:08:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] NOT NULL,
	[Surname] [nvarchar](50) NULL,
	[Othernames] [nvarchar](50) NULL,
	[Password] [varbinary](max) NULL,
	[RankID] [uniqueidentifier] NULL,
	[RoleID] [uniqueidentifier] NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](20) NULL,
	[LPNO] [nvarchar](30) NULL,
	[Signature] [varbinary](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsVerified] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Centres]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Centres] ON [dbo].[Centres]
(
	[CentreNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Centres_SchoolCategoryID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Centres_SchoolCategoryID] ON [dbo].[Centres]
(
	[SchoolCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LocalGovernments_StateID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_LocalGovernments_StateID] ON [dbo].[LocalGovernments]
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Offices_OfficeTypeID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Offices_OfficeTypeID] ON [dbo].[Offices]
(
	[OfficeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PinHistories_PinID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_PinHistories_PinID] ON [dbo].[PinHistories]
(
	[PinID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PinHistories_SchoolID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_PinHistories_SchoolID] ON [dbo].[PinHistories]
(
	[SchoolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Pins_CreatedBy]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Pins_CreatedBy] ON [dbo].[Pins]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Pins_RecognitionTypeID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Pins_RecognitionTypeID] ON [dbo].[Pins]
(
	[RecognitionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SchoolPayments_CreatedBy]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_SchoolPayments_CreatedBy] ON [dbo].[SchoolPayments]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SchoolPayments_PinID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_SchoolPayments_PinID] ON [dbo].[SchoolPayments]
(
	[PinID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SchoolPayments_SchoolID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_SchoolPayments_SchoolID] ON [dbo].[SchoolPayments]
(
	[SchoolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schools_CategoryID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Schools_CategoryID] ON [dbo].[Schools]
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schools_LgID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Schools_LgID] ON [dbo].[Schools]
(
	[LgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schools_OfficeID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Schools_OfficeID] ON [dbo].[Schools]
(
	[OfficeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_State]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_State] ON [dbo].[States]
(
	[Code] ASC
)
WHERE ([Code] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RankID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_RankID] ON [dbo].[Users]
(
	[RankID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RoleID]    Script Date: 23-Apr-20 12:08:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_RoleID] ON [dbo].[Users]
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AuditTrail] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[AuditTrail] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Centres] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[DBLogger] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[LocalGovernments] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[LocationTypes] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Offices] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[OfficeTypes] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[OfficeTypes] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PinHistories] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Pins] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Ranks] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[RecognitionTypes] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[SchoolCategories] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[SchoolPayments] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Schools] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[States] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Centres]  WITH CHECK ADD  CONSTRAINT [FK_Centres_SchoolCategories] FOREIGN KEY([SchoolCategoryID])
REFERENCES [dbo].[SchoolCategories] ([ID])
GO
ALTER TABLE [dbo].[Centres] CHECK CONSTRAINT [FK_Centres_SchoolCategories]
GO
ALTER TABLE [dbo].[LocalGovernments]  WITH CHECK ADD  CONSTRAINT [FK_LocalGovernments_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[States] ([ID])
GO
ALTER TABLE [dbo].[LocalGovernments] CHECK CONSTRAINT [FK_LocalGovernments_State]
GO
ALTER TABLE [dbo].[Offices]  WITH CHECK ADD  CONSTRAINT [FK_Offices_OfficeTypes] FOREIGN KEY([OfficeTypeID])
REFERENCES [dbo].[OfficeTypes] ([ID])
GO
ALTER TABLE [dbo].[Offices] CHECK CONSTRAINT [FK_Offices_OfficeTypes]
GO
ALTER TABLE [dbo].[PinHistories]  WITH CHECK ADD  CONSTRAINT [FK_PinHistory_PIN] FOREIGN KEY([PinID])
REFERENCES [dbo].[Pins] ([ID])
GO
ALTER TABLE [dbo].[PinHistories] CHECK CONSTRAINT [FK_PinHistory_PIN]
GO
ALTER TABLE [dbo].[PinHistories]  WITH CHECK ADD  CONSTRAINT [FK_PinHistory_School] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[Schools] ([ID])
GO
ALTER TABLE [dbo].[PinHistories] CHECK CONSTRAINT [FK_PinHistory_School]
GO
ALTER TABLE [dbo].[Pins]  WITH CHECK ADD  CONSTRAINT [FK_PIN_RecognitionType] FOREIGN KEY([RecognitionTypeID])
REFERENCES [dbo].[RecognitionTypes] ([ID])
GO
ALTER TABLE [dbo].[Pins] CHECK CONSTRAINT [FK_PIN_RecognitionType]
GO
ALTER TABLE [dbo].[Pins]  WITH CHECK ADD  CONSTRAINT [FK_PIN_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Pins] CHECK CONSTRAINT [FK_PIN_User]
GO
ALTER TABLE [dbo].[SchoolPayments]  WITH CHECK ADD  CONSTRAINT [FK_SchoolPayment_PIN] FOREIGN KEY([PinID])
REFERENCES [dbo].[Pins] ([ID])
GO
ALTER TABLE [dbo].[SchoolPayments] CHECK CONSTRAINT [FK_SchoolPayment_PIN]
GO
ALTER TABLE [dbo].[SchoolPayments]  WITH CHECK ADD  CONSTRAINT [FK_SchoolPayment_School] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[Schools] ([ID])
GO
ALTER TABLE [dbo].[SchoolPayments] CHECK CONSTRAINT [FK_SchoolPayment_School]
GO
ALTER TABLE [dbo].[SchoolPayments]  WITH CHECK ADD  CONSTRAINT [FK_SchoolPayment_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SchoolPayments] CHECK CONSTRAINT [FK_SchoolPayment_User]
GO
ALTER TABLE [dbo].[Schools]  WITH CHECK ADD  CONSTRAINT [FK_School_LocalGovernment] FOREIGN KEY([LgID])
REFERENCES [dbo].[LocalGovernments] ([ID])
GO
ALTER TABLE [dbo].[Schools] CHECK CONSTRAINT [FK_School_LocalGovernment]
GO
ALTER TABLE [dbo].[Schools]  WITH CHECK ADD  CONSTRAINT [FK_School_Office] FOREIGN KEY([OfficeID])
REFERENCES [dbo].[Offices] ([ID])
GO
ALTER TABLE [dbo].[Schools] CHECK CONSTRAINT [FK_School_Office]
GO
ALTER TABLE [dbo].[Schools]  WITH CHECK ADD  CONSTRAINT [FK_School_SchoolCategory] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[SchoolCategories] ([ID])
GO
ALTER TABLE [dbo].[Schools] CHECK CONSTRAINT [FK_School_SchoolCategory]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_User_Rank] FOREIGN KEY([RankID])
REFERENCES [dbo].[Ranks] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_User_Rank]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [SchoolRecognition] SET  READ_WRITE 
GO
