create or alter procedure [dbo].[usp_RiwayatPelatihan_Update](
  @Id int
, @NamaKursus varchar(255)
, @SertifikatAda bit
, @Tahun int
)
as 
begin
	--set nocount on
	update RiwayatPelatihan
		set NamaKursus = @namaKursus
		, SertifikatAda = @SertifikatAda
		, Tahun = @Tahun

		where Id = @Id
end
GO

