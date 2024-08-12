create or alter procedure [dbo].[usp_RiwayatPekerjaan_Insert](
  @BiodataId int
, @NamaPerusahaan varchar(255)
, @PosisiTerakhir varchar(255)
, @PendapatanTerakhir varchar(255)
, @Tahun int
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
	OUTPUT INSERTED.*
	values(
	  @BiodataId
	, @NamaPerusahaan
	, @PosisiTerakhir
	, @PendapatanTerakhir
	, @Tahun
	)
end
GO


