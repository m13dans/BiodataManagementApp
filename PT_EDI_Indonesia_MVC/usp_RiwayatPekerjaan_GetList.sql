create procedure usp_RiwayatPekerjaan_GetList @biodataId int
as
begin
	select * from RiwayatPekerjaan join Biodata
	on RiwayatPekerjaan.BiodataId = @biodataId
end