CREATE or alter procedure [dbo].[usp_RiwayatPekerjaan_GetByBioId] @biodataId int
as
begin
	select * from RiwayatPekerjaan where BiodataId = @biodataId
end