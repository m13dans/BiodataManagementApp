create procedure usp_Biodata_Update(
	@id int
	,@posisiDilamar varchar(255)
	,@nama varchar(255)
	,@noKTP varchar(50)
	,@tempatLahir varchar(100)
	,@tanggalLahir date
	,@jenisKelamin tinyint
	,@agama varchar(50)
	,@golonganDarah varchar(2)
	,@status varchar(50)
	,@alamatKtp varchar(255)
	,@alamatTinggal varchar(255)
	,@email varchar (255)
	,@noTelepon varchar(50)
	,@kontakOrangTerdekat varchar(50)
	,@skill varchar(255)
	,@bersediaDitempatkan bit
	,@penghasilanDiharapkan money
)
as
begin
	update Biodata 
	set PosisiDilamar = @posisiDilamar,
	Nama = @nama,
	NoKTP = @noKTP,
	TempatLahir = @tempatLahir,
	TanggalLahir = @tanggalLahir,
	JenisKelamin = @jenisKelamin,
	Agama = @agama,
	GolonganDarah = @golonganDarah,
	[Status] = @status,
	AlamatKtp = @alamatKtp,
	AlamatTinggal = @alamatTinggal,
	Email = @email,
	NoTelepon = @noTelepon,
	KontakOrangTerdekat = @kontakOrangTerdekat,
	Skill = @skill,
	BersediaDiTempatkan = @bersediaDitempatkan,
	PenghasilanDiHarapkan = @penghasilanDiharapkan

where Id = @id
end