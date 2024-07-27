create or alter procedure usp_Biodata_Update(
	@Id int
	,@PosisiDilamar varchar(255)
	,@Nama varchar(255)
	,@TempatLahir varchar(100)
	,@TanggalLahir date
	,@JenisKelamin tinyint
	,@Agama varchar(50)
	,@GolonganDarah varchar(2)
	,@Status varchar(50)
	,@AlamatKtp varchar(255)
	,@AlamatTinggal varchar(255)
	,@Email varchar (255)
	,@NoTelepon varchar(50)
	,@KontakOrangTerdekat varchar(50)
	,@Skill varchar(255)
	,@BersediaDitempatkan bit
	,@PenghasilanDiharapkan money
)
as
begin
	update Biodata 
	set PosisiDilamar = @PosisiDilamar,
	Nama = @Nama,
	TempatLahir = @TempatLahir,
	TanggalLahir = @TanggalLahir,
	JenisKelamin = @JenisKelamin,
	Agama = @Agama,
	GolonganDarah = @GolonganDarah,
	[Status] = @Status,
	AlamatKtp = @AlamatKtp,
	AlamatTinggal = @AlamatTinggal,
	Email = @Email,
	NoTelepon = @NoTelepon,
	KontakOrangTerdekat = @KontakOrangTerdekat,
	Skill = @Skill,
	BersediaDiTempatkan = @BersediaDitempatkan,
	PenghasilanDiHarapkan = @PenghasilanDiharapkan
where Id = @Id
end