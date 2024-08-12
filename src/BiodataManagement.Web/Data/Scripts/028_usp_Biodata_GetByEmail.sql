create or alter procedure usp_Biodata_GetByEmail @Email VARCHAR(255)
as 
begin
	select * from Biodata b 
	left join PendidikanTerakhir pt on b.Id = pt.BiodataId
	left join RiwayatPekerjaan rpk on b.Id = rpk.BiodataId
	left join RiwayatPelatihan rpl on b.Id = rpl.BiodataId
	where b.Email = @Email
end

