create or alter procedure usp_Biodata_Delete @id int
as
begin
	delete from Biodata where Id = @id
end