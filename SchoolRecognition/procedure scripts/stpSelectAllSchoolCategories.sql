USE [SchoolRecognition]
GO
/****** Object:  StoredProcedure [dbo].[stpSelectAllSchoolCategories]    Script Date: 28-Mar-20 11:25:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[stpSelectAllSchoolCategories]	
AS
BEGIN

	SET NOCOUNT ON;

 select * from SchoolCategories

END


