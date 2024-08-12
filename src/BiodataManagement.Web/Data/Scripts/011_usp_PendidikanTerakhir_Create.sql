create or alter procedure usp_PendidikanTerakhir_Create (
	@UdtPendidikanTerakir UDT_PendidikanTerakhir READONLY)

as
begin
	INSERT INTO PendidikanTerakhir (
		BiodataId,
		JenjangPendidikanTerakhir,
		NamaInstitusiAkademik,
		Jurusan,
		TahunLulus,
		IPK
	)
	OUTPUT INSERTED.*
	select * from @UdtPendidikanTerakir
end

