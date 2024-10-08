IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].Biodata') AND type in (N'U'))

BEGIN
	create table Biodata (
		Id int primary key clustered identity(1,1),
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
		UserId NVARCHAR(450) NULL)
END