USE [PTEdiIndonesia]
GO

/****** Object:  StoredProcedure [dbo].[usp_Biodata_Delete]    Script Date: 6/8/2024 3:19:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[usp_PendidikanTerakhir_Delete] @id int
as
begin
	delete from PendidikanTerakhir where Id = @id
end
GO


