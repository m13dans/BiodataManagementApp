create or alter procedure [dbo].[usp_RiwayatPekerjaan_Delete] @id int
as
begin
	delete from RiwayatPekerjaan where Id = @id
end
GO


