USE [PTEdiIndonesia]
GO
/****** Object:  StoredProcedure [dbo].[usp_RiwayatPekerjaan_GetList]    Script Date: 6/8/2024 2:39:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[usp_RiwayatPelatihan_GetByBioId] @biodataId int
as
begin
	select * from RiwayatPelatihan where BiodataId = @biodataId
end