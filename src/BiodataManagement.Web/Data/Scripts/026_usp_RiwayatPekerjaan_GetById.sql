CREATE or alter procedure [dbo].[usp_RiwayatPekerjaan_GetById] @Id int
as
begin
	select * from RiwayatPekerjaan where Id = @Id
end