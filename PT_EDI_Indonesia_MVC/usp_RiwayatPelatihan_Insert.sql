USE [PTEdiIndonesia]
GO

/****** Object:  StoredProcedure [dbo].[usp_Biodata_BulkInsert]    Script Date: 6/8/2024 2:43:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_RiwayatPelatihan_Insert](
  @biodataId int
, @namaKursus varchar(255)
, @sertifikatAda bit
, @tahun date
)
as 
begin
	--set nocount on
	insert into RiwayatPelatihan(
		BiodataId
		, NamaKursus
		, SertifikatAda
		, Tahun
	)
	values(
	  @biodataId
	, @namaKursus
	, @sertifikatAda
	, @tahun
	)
end
GO


