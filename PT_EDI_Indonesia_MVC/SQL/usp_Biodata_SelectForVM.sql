alter procedure usp_Biodata_SelectForVM
as
begin
	select Id, Nama, TempatLahir, TanggalLahir, PosisiDilamar from Biodata
end