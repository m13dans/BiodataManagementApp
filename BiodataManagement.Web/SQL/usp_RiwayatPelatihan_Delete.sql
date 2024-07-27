create or alter procedure usp_RiwayatPelatihan_Delete @id int
as
begin
	delete from RiwayatPelatihan where Id = @id
end