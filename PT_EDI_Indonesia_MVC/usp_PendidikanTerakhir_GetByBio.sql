create procedure usp_PendidikanTerakhir_GetByBioId @bioId int
as
begin
	select * from PendidikanTerakhir where BiodataId = @bioId
end

