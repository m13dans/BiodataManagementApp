create or alter procedure [dbo].[usp_RiwayatPekerjaan_Delete] @Id int
as
begin
	delete from RiwayatPekerjaan OUTPUT DELETED.* where Id = @Id
end
GO


