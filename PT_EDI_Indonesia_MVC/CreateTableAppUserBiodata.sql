USE PTEdiIndonesia;

CREATE TABLE AppUserBiodata (
    Id INT PRIMARY KEY IDENTITY,
    AppUserId NVARCHAR(450) CONSTRAINT FK_AppUserBiodata_AspNetUsers
        FOREIGN KEY REFERENCES AspNetUsers (Id),
    BiodataId INT CONSTRAINT FK_AppUserBiodata_Biodata
        FOREIGN KEY REFERENCES Biodata (Id),
    Email NVARCHAR (256)
)