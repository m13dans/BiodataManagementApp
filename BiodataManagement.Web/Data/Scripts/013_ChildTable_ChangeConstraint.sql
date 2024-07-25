ALTER TABLE PendidikanTerakhir DROP CONSTRAINT FK_PendidikanTerakhir_BiodataId
ALTER TABLE PendidikanTerakhir ADD CONSTRAINT FK_PendidikanTerakhir_BiodataId 
	FOREIGN KEY (BiodataId) REFERENCES Biodata (Id) ON DELETE CASCADE

ALTER TABLE RiwayatPekerjaan DROP CONSTRAINT FK_RiwayatPekerjaan_BiodataId
ALTER TABLE RiwayatPekerjaan ADD CONSTRAINT FK_RiwayatPekerjaan_BiodataId 
	FOREIGN KEY (BiodataId) REFERENCES Biodata (Id) ON DELETE CASCADE

ALTER TABLE RiwayatPelatihan DROP CONSTRAINT FK_RiwayatPelatihan_BiodataId
ALTER TABLE RiwayatPelatihan ADD CONSTRAINT FK_RiwayatPelatihan_BiodataId 
	FOREIGN KEY (BiodataId) REFERENCES Biodata (Id) ON DELETE CASCADE

