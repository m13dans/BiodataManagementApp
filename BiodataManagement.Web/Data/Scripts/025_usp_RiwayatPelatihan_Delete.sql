create or alter procedure usp_RiwayatPelatihan_Delete @Id int
as
begin
	delete from RiwayatPelatihan where Id = @Id
end