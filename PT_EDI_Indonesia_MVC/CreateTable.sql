use PTEdiIndonesia;

create table RiwayatPekerjaan(
	Id int primary key clustered identity(1,1),
	BiodataId int,
	NamaPerusahaan varchar(255),
	PosisiTerakhir varchar(255),
	PendapatanTerakhir money,
	tahun int
)

create table RiwayatPelatihan (
	Id int primary key clustered identity(1,1),
	BiodataId int,
	NamaKursus varchar(255),
	SertifikatAda bit,
	Tahun int

)

alter table RiwayatPekerjaan add constraint FK_RiwayatPekerjaan_BiodataId
foreign key (biodataid) references biodata(Id)

alter table RiwayatPelatihan add constraint FK_RiwayatPelatihan_BiodataId
foreign key (BiodataId) references [dbo].[Biodata](Id)

alter table PendidikanTerakhir add constraint FK_PendidikanTerakhir_BiodataId
foreign key (BiodataId) references biodata(Id)
