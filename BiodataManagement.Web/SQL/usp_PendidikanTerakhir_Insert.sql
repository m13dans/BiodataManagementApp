create or alter procedure [dbo].[usp_PendidikanTerakhir_Insert](
 @JenjangPendidikanTerakhir varchar(50)
, @NamaInstitusiAkademik varchar(255)
, @Jurusan varchar(255)
, @TahunLulus int
, @Ipk float
)
as 
begin
	--set nocount on
	INSERT INTO PendidikanTerakhir (
		JenjangPendidikanTerakhir,
		NamaInstitusiAkademik,
		Jurusan,
		TahunLulus,
		IPK
	)
	VALUES (
		@JenjangPendidikanTerakhir,
		@NamaInstitusiAkademik,
		@Jurusan,
		@TahunLulus,
		@IPK
	)
end
GO


