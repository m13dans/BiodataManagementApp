CREATE or alter procedure [dbo].[usp_RiwayatPekerjaan_GetByBioId] @BiodataId int
as
begin
	select * from RiwayatPekerjaan where BiodataId = @BiodataId
end