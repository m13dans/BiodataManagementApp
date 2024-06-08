create procedure usp_Biodata_GetById @id int
as 
begin
	select * from Biodata b 
	left join PendidikanTerakhir pt on b.Id = pt.BiodataId
	left join RiwayatPekerjaan rpk on b.Id = rpk.BiodataId
	left join RiwayatPelatihan rpl on b.Id = rpl.BiodataId
	where b.Id = @id
end

