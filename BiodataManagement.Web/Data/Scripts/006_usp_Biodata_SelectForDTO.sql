CREATE OR ALTER PROCEDURE usp_Biodata_SelectForDTO
AS
BEGIN
    SELECT Id, Nama, TempatLahir, TanggalLahir, PosisiDilamar FROM Biodata
END