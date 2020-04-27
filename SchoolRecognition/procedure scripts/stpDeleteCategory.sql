USE [SchoolRecognition]
GO
/****** Object:  StoredProcedure [dbo].[stpDeleteCategory]    Script Date: 28-Mar-20 11:21:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[stpDeleteCategory]

AS
DECLARE @categoryID uniqueidentifier

 
SET @categoryID  = NEWID()

Delete from dbo.SchoolCategories

where ID = @categoryID