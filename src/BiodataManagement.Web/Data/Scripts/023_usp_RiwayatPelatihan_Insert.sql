create or alter procedure [dbo].[usp_RiwayatPelatihan_Insert](
  @BiodataId int
, @NamaKursus varchar(255)
, @SertifikatAda bit
, @Tahun INT
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
	  @BiodataId
	, @NamaKursus
	, @SertifikatAda
	, @Tahun
	)
end
GO


