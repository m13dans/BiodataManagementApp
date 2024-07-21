IF not EXISTS(SELECT 1 FROM sys.procedures 
    WHERE object_id = OBJECT_ID(N'dbo.usp_Biodata_BulkInsert'))

PRINT 2 + 2

