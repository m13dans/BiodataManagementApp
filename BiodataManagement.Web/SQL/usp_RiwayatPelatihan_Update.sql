USE [PTEdiIndonesia]
GO

/****** Object:  StoredProcedure [dbo].[usp_Biodata_BulkInsert]    Script Date: 6/8/2024 2:43:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_RiwayatPelatihan_Update](
  @id int
, @namaKursus varchar(255)
, @sertifikatAda bit
, @tahun int
)
as 
begin
	--set nocount on
	update RiwayatPelatihan
		set NamaKursus = @namaKursus
		, SertifikatAda = @sertifikatAda
		, Tahun = @tahun

		where Id = @id
end
GO

