create or alter procedure [dbo].[usp_RiwayatPelatihan_GetByBioId] @BiodataId int
as
begin
	select * from RiwayatPelatihan where BiodataId = @BiodataId
end