create or alter procedure [dbo].[usp_RiwayatPelatihan_GetByBioId] @biodataId int
as
begin
	select * from RiwayatPelatihan where BiodataId = @biodataId
end