USE [master]
GO
/****** Object:  Database [SchoolRecognition]    Script Date: 10/03/2020 11:54:04 ******/
CREATE DATABASE [SchoolRecognition]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolRecognition', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SchoolRecognition.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SchoolRecognition_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SchoolRecognition_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SchoolRecognition] SET COMPATIBILITY_LEVEL = 120
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
ALTER DATABASE [SchoolRecognition] SET AUTO_CLOSE OFF 
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
ALTER DATABASE [SchoolRecognition] SET  DISABLE_BROKER 
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
ALTER DATABASE [SchoolRecognition] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolRecognition] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolRecognition] SET RECOVERY FULL 
GO
ALTER DATABASE [SchoolRecognition] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolRecognition] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolRecognition] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolRecognition] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolRecognition] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SchoolRecognition] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SchoolRecognition', N'ON'
GO
USE [SchoolRecognition]
GO
/****** Object:  Table [dbo].[LocalGovernments]    Script Date: 10/03/2020 11:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalGovernments](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](3) NULL,
	[StateID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LocalGovernment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Offices]    Script Date: 10/03/2020 11:54:04 ******/
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
 CONSTRAINT [PK_Office] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PinHistories]    Script Date: 10/03/2020 11:54:04 ******/
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
 CONSTRAINT [PK_PinHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PINs]    Script Date: 10/03/2020 11:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PINs](
	[ID] [uniqueidentifier] NOT NULL,
	[RecognitionTypeID] [uniqueidentifier] NULL,
	[SerialPin] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsInUse] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[DateCreated] [date] NULL,
 CONSTRAINT [PK_PIN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ranks]    Script Date: 10/03/2020 11:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ranks](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](10) NULL,
 CONSTRAINT [PK_Rank] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RecognitionTypes]    Script Date: 10/03/2020 11:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecognitionTypes](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [varchar](3) NULL,
 CONSTRAINT [PK_RecognitionType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/03/2020 11:54:04 ******/
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
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SchoolCategories]    Script Date: 10/03/2020 11:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolCategories](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](2) NULL,
 CONSTRAINT [PK_SchoolCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SchoolPayments]    Script Date: 10/03/2020 11:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
 CONSTRAINT [PK_SchoolPayment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Schools]    Script Date: 10/03/2020 11:54:04 ******/
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
 CONSTRAINT [PK_School] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[States]    Script Date: 10/03/2020 11:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](3) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Titles]    Script Date: 10/03/2020 11:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Titles](
	[ID] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_Title] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/03/2020 11:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] NOT NULL,
	[Surname] [nvarchar](max) NULL,
	[Others] [nvarchar](max) NULL,
	[Password] [varbinary](max) NULL,
	[RankID] [uniqueidentifier] NULL,
	[RoleID] [uniqueidentifier] NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](20) NULL,
	[LPNO] [nvarchar](30) NULL,
	[Signature] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsVerified] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_State]    Script Date: 10/03/2020 11:54:04 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_State] ON [dbo].[States]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PINs] ADD  CONSTRAINT [DF_PINs_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PINs] ADD  CONSTRAINT [DF_PINs_IsInUse]  DEFAULT ((0)) FOR [IsInUse]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_IsDefault]  DEFAULT ((0)) FOR [IsDefault]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_IsSuperAdmin]  DEFAULT ((0)) FOR [IsSuperAdmin]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsVerified]  DEFAULT ((0)) FOR [IsVerified]
GO
ALTER TABLE [dbo].[LocalGovernments]  WITH CHECK ADD  CONSTRAINT [FK_LocalGovernments_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[States] ([ID])
GO
ALTER TABLE [dbo].[LocalGovernments] CHECK CONSTRAINT [FK_LocalGovernments_State]
GO
ALTER TABLE [dbo].[PinHistories]  WITH CHECK ADD  CONSTRAINT [FK_PinHistories_PinHistories] FOREIGN KEY([ID])
REFERENCES [dbo].[PinHistories] ([ID])
GO
ALTER TABLE [dbo].[PinHistories] CHECK CONSTRAINT [FK_PinHistories_PinHistories]
GO
ALTER TABLE [dbo].[PinHistories]  WITH CHECK ADD  CONSTRAINT [FK_PinHistory_PIN] FOREIGN KEY([PinID])
REFERENCES [dbo].[PINs] ([ID])
GO
ALTER TABLE [dbo].[PinHistories] CHECK CONSTRAINT [FK_PinHistory_PIN]
GO
ALTER TABLE [dbo].[PinHistories]  WITH CHECK ADD  CONSTRAINT [FK_PinHistory_School] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[Schools] ([ID])
GO
ALTER TABLE [dbo].[PinHistories] CHECK CONSTRAINT [FK_PinHistory_School]
GO
ALTER TABLE [dbo].[PINs]  WITH CHECK ADD  CONSTRAINT [FK_PIN_RecognitionType] FOREIGN KEY([RecognitionTypeID])
REFERENCES [dbo].[RecognitionTypes] ([ID])
GO
ALTER TABLE [dbo].[PINs] CHECK CONSTRAINT [FK_PIN_RecognitionType]
GO
ALTER TABLE [dbo].[PINs]  WITH CHECK ADD  CONSTRAINT [FK_PIN_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[PINs] CHECK CONSTRAINT [FK_PIN_User]
GO
ALTER TABLE [dbo].[SchoolPayments]  WITH CHECK ADD  CONSTRAINT [FK_SchoolPayment_PIN] FOREIGN KEY([PinID])
REFERENCES [dbo].[PINs] ([ID])
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
