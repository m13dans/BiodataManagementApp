CREATE or alter procedure [dbo].[usp_RiwayatPelatihan_GetById] @Id int
as
begin
	select * from RiwayatPelatihan where Id = @Id
end