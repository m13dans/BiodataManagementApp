create or alter procedure [dbo].[usp_PendidikanTerakhir_Insert](
  @BiodataId int
, @JenjangPendidikanTerakhir varchar(50)
, @NamaInstitusiAkademik varchar(255)
, @Jurusan varchar(255)
, @TahunLulus int
, @Ipk float
)
as 
begin
	--set nocount on
	INSERT INTO PendidikanTerakhir (
		BiodataId,
		JenjangPendidikanTerakhir,
		NamaInstitusiAkademik,
		Jurusan,
		TahunLulus,
		IPK
	)

	OUTPUT Inserted.*

	VALUES (
		@BiodataId,
		@JenjangPendidikanTerakhir,
		@NamaInstitusiAkademik,
		@Jurusan,
		@TahunLulus,
		@IPK
	)
end
GO


