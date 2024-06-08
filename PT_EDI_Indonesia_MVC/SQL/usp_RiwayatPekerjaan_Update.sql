USE [PTEdiIndonesia]
GO

/****** Object:  StoredProcedure [dbo].[usp_Biodata_BulkInsert]    Script Date: 6/8/2024 2:43:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_RiwayatPekerjaan_Update](
  @id int
, @namaPerusahaan varchar(255)
, @posisiTerakhir varchar(255)
, @pendapatanTerakhir varchar(255)
, @tahun int
)
as 
begin
	--set nocount on
	update RiwayatPekerjaan
		set 
		NamaPerusahaan = @namaPerusahaan
		, PosisiTerakhir = @posisiTerakhir
		, PendapatanTerakhir = @pendapatanTerakhir
		, Tahun = @tahun
	where Id = @id
end
GO

