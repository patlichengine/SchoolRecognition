USE [SchoolRecognition]
GO
/****** Object:  StoredProcedure [dbo].[stpGetCategoryById]    Script Date: 28-Mar-20 11:23:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER Procedure [dbo].[stpGetCategoryById]

As
Declare @categoryID uniqueidentifier

 
SET @categoryID  = NEWID()
begin

	SELECT * 

	from dbo.SchoolCategories where ID = @categoryID
	

END
