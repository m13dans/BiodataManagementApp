create or alter procedure [dbo].[usp_PendidikanTerakhir_update](
  @Id int
, @JenjangPendidikanTerakhir varchar(50)
, @NamaInstitusiAkademik varchar(255)
, @Jurusan varchar(255)
, @TahunLulus int
, @Ipk float
)
as 
begin
	--set nocount on
	update PendidikanTerakhir 
	set JenjangPendidikanTerakhir = @JenjangPendidikanTerakhir,
	NamaInstitusiAkademik = @NamaInstitusiAkademik,
	Jurusan = @Jurusan,
	TahunLulus = @TahunLulus,
	IPK = @Ipk
	where Id = @Id
end
GO
