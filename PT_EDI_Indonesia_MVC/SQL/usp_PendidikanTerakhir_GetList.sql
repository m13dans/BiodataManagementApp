create procedure usp_PendidikanTerakhir_GetList @biodataId int
as
begin
	select * from PendidikanTerakhir join Biodata
	on PendidikanTerakhir.BiodataId = @biodataId
end