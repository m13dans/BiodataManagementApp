IF TYPE_ID(N'UDT_PendidikanTerakhir') IS NULL

BEGIN
	CREATE TYPE UDT_PendidikanTerakhir AS TABLE (
		BiodataId int,
		JenjangPendidikanTerakhir varchar(50),
		NamaInstitusiAkademik varchar(255),
		Jurusan varchar(255),
		TahunLulus int,
		IPK float)
END


