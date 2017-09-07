
USE [MOSAIQ]
GO

/****** Object:  StoredProcedure [dbo].[Shields_WSOncologyCanvas_GetAllPatients]    Script Date: 11/12/2014 11:49:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bartholomew, Christopher
-- Create date: 1/29/2014
-- Description:	Returns All Patients 
-- =============================================
ALTER PROCEDURE [dbo].[Shields_WSOncologyCanvas_GetAllPatients]
	-- Add the parameters for the stored procedure here
	@LASTID INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @BENCHMARK_IDENTITY AS INT;

	-- 2012 patients and most recent
	SET @BENCHMARK_IDENTITY = 15441;

	IF @LASTID IS NULL OR @LASTID = 0
		BEGIN
			SELECT 
			I.IDA,
			I.IDENT_ID, 
			p.First_Name,
			p.Last_Name, 
			p.Birth_DtTm, 
			P.SS_Number, 
			I.Create_DtTm
			FROM dbo.Patient AS P (NOLOCK)
			INNER JOIN DBO.Ident AS I (NOLOCK)
			ON P.Pat_ID1 = I.Pat_ID1
			WHERE I.IDENT_ID > @BENCHMARK_IDENTITY
			ORDER BY I.IDENT_ID ASC
		END
	ELSE
		BEGIN
			-- IF LAST ID IS PROVIDED, USE THIS ONE
			SELECT 
			I.IDA,
			I.IDENT_ID, 
			p.First_Name,
			p.Last_Name, 
			p.Birth_DtTm, 
			P.SS_Number, 
			I.Create_DtTm
			FROM dbo.Patient AS P (NOLOCK)
			INNER JOIN DBO.Ident AS I (NOLOCK)
			ON P.Pat_ID1 = I.Pat_ID1
			WHERE I.IDENT_ID > @LASTID
			ORDER BY I.IDENT_ID DESC	
		END

END
GO

USE [MOSAIQ]
GO

/****** Object:  StoredProcedure [dbo].[Shields_WSOncologyCanvas_GetLastPT]    Script Date: 11/12/2014 11:50:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bartholomew, Christopher
-- Create date: 1/29/2014
-- Description:	Obtains the last inserted identity in mosaiq
-- =============================================
ALTER PROCEDURE [dbo].[Shields_WSOncologyCanvas_GetLastPT]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	MAX(I.IDENT_ID) AS LAST_ID
	FROM dbo.Patient AS P (NOLOCK)
	INNER JOIN DBO.Ident AS I (NOLOCK)
	ON P.Pat_ID1 = I.Pat_ID1

END
GO
