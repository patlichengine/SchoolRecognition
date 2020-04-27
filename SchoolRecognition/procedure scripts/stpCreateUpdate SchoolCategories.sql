USE [SchoolRecognition]
GO
/****** Object:  StoredProcedure [dbo].[stpCreateUpdateSchoolCategories]    Script Date: 28-Mar-20 11:18:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[stpCreateUpdateSchoolCategories]
	-- Add the parameters for the stored procedure here
	 @categoryName nvarchar(50), @categoryCode nvarchar(50)
	
	AS
	DECLARE @categoryID uniqueidentifier

	SET @categoryID  = NEWID()

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	
	
	insert into dbo.SchoolCategories 
	(ID, Name,Code)    values(@categoryID,@categoryName, @categoryCode)

	

	UPDATE dbo.SchoolCategories 
	SET

	Name = @categoryName,
	code = @categoryCode

	
	where ID = @categoryID

	select * from SchoolCategories



