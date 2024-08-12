create or alter procedure [dbo].[usp_PendidikanTerakhir_Delete] @Id int
as
begin
	delete from PendidikanTerakhir where Id = @Id
end
GO


