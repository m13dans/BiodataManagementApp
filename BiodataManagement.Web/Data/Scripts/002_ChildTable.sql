IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].PendidikanTerakhir') AND type in (N'U'))

BEGIN
	create table PendidikanTerakhir(
		Id int primary key clustered identity(1,1),
		BiodataId int,
		JenjangPendidikanTerakhir varchar(50),
		NamaInstitusiAkademik varchar(255),
		Jurusan varchar(255),
		TahunLulus int,
		IPK float,
		constraint FK_PendidikanTerakhir_BiodataId
		foreign key (BiodataId) references biodata(Id))
END

IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].RiwayatPekerjaan') AND type in (N'U'))

BEGIN
	create table RiwayatPekerjaan(
		Id int primary key clustered identity(1,1),
		BiodataId int,
		NamaPerusahaan varchar(255),
		PosisiTerakhir varchar(255),
		PendapatanTerakhir money,
		Tahun int,
		CONSTRAINT FK_RiwayatPekerjaan_BiodataId
		foreign key (biodataid) references biodata(Id))
END

IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].RiwayatPelatihan') AND type in (N'U'))

BEGIN
create table RiwayatPelatihan (
	Id int primary key clustered identity(1,1),
	BiodataId int,
	NamaKursus varchar(255),
	SertifikatAda bit,
	Tahun int,
	constraint FK_RiwayatPelatihan_BiodataId
	foreign key (BiodataId) references [dbo].[Biodata](Id))
END