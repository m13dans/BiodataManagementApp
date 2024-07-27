create or alter procedure [dbo].[usp_RiwayatPekerjaan_Insert](
  @biodataId int
, @namaPerusahaan varchar(255)
, @posisiTerakhir varchar(255)
, @pendapatanTerakhir varchar(255)
, @tahun int
)
as 
begin
	--set nocount on
	insert into RiwayatPekerjaan(
		BiodataId
		, NamaPerusahaan
		, PosisiTerakhir
		, PendapatanTerakhir
		, Tahun

	)
	values(
	  @BiodataId
	, @namaPerusahaan
	, @posisiTerakhir
	, @pendapatanTerakhir
	, @tahun
	)
end
GO


