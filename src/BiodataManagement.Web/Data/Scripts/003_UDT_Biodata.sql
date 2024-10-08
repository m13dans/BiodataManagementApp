IF TYPE_ID(N'UDT_Biodata') IS NULL

BEGIN
	CREATE TYPE UDT_Biodata AS TABLE (
		PosisiDilamar varchar(255) not null,
		Nama varchar(255) not null,
		NoKTP varchar(50) unique not null,
		TempatLahir varchar(100) not null,
		TanggalLahir date not null,
		JenisKelamin tinyint not null,
		Agama varchar(50),
		GolonganDarah varchar(2),
		[Status] varchar(50),
		AlamatKtp varchar (255) not null,
		AlamatTinggal varchar (255) not null,
		Email varchar (255) unique not null ,
		NoTelepon varchar (50) not null,
		KontakOrangTerdekat varchar (50),
		Skill varchar(255),
		BersediaDiTempatkan bit default 0,
		PenghasilanDiHarapkan money,
		UserId NVARCHAR(450) null)
END


