create or alter procedure [dbo].[usp_RiwayatPekerjaan_Update](
  @Id int
, @NamaPerusahaan varchar(255)
, @PosisiTerakhir varchar(255)
, @PendapatanTerakhir varchar(255)
, @Tahun int
)
as 
begin
	--set nocount on
	update RiwayatPekerjaan
		set 
		NamaPerusahaan = @NamaPerusahaan
		, PosisiTerakhir = @PosisiTerakhir
		, PendapatanTerakhir = @PendapatanTerakhir
		, Tahun = @Tahun
	where Id = @Id
end
GO

