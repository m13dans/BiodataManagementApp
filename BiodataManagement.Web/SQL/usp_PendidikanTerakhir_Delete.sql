create or alter procedure [dbo].[usp_PendidikanTerakhir_Delete] @id int
as
begin
	delete from PendidikanTerakhir where Id = @id
end
GO


