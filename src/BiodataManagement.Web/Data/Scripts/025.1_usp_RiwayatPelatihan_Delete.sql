create or alter procedure usp_RiwayatPelatihan_Delete @Id int
as
begin
	delete from RiwayatPelatihan
	OUTPUT DELETED.*
	where Id = @Id
end