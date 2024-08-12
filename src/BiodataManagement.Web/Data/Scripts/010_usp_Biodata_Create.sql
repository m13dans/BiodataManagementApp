CREATE or alter procedure [usp_Biodata_Create](
@udtBiodata UDT_Biodata readonly)

as 
begin	
	set nocount on
	insert into Biodata (
		PosisiDilamar
		,Nama
		,NoKTP
		,TempatLahir
		,TanggalLahir
		,JenisKelamin
		,Agama
		,GolonganDarah
		,[Status]
		,AlamatKtp
		,AlamatTinggal
		,Email
		,NoTelepon
		,KontakOrangTerdekat
		,Skill
		,BersediaDiTempatkan
		,PenghasilanDiHarapkan
		,UserId
	)
	OUTPUT inserted.*
	select * from @udtBiodata
end