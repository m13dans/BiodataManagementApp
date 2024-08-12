create or alter procedure usp_PendidikanTerakhir_GetAllFor(
	@BiodataId INT)

as
begin
	select * from PendidikanTerakhir WHERE BiodataId = @BiodataId
end

