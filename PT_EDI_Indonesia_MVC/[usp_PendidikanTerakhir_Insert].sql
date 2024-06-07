USE [PTEdiIndonesia]
GO

/****** Object:  StoredProcedure [dbo].[usp_Biodata_BulkInsert]    Script Date: 6/8/2024 2:43:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[usp_PendidikanTerakhir_update](
  @id int
, @jenjangPendidikanTerakhir varchar(50)
, @namaInstitusiAkademik varchar(255)
, @jurusan varchar(255)
, @tahunLulus date
, @ipk float
)
as 
begin
	--set nocount on
	update PendidikanTerakhir 
	set JenjangPendidikanTerakhir = @jenjangPendidikanTerakhir,
	NamaInstitusiAkademik = @namaInstitusiAkademik,
	Jurusan = @jurusan,
	TahunLulus = @tahunLulus,
	IPK = @ipk
end
GO


