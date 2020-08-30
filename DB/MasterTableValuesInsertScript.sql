USE [Matrimony]
GO

DECLARE @tableId VARCHAR(100)
DECLARE @Values NVARCHAR(MAX)


SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'MaritalStatus'
SET @Values = 'Divorced,Widowed,Awaiting Divorce,Annulled'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',')

SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'Comunity'
SET @Values = '24 Manai Telugu Chettiar,96 Kuli Maratha,96K Kokanastha,Adi Andhra,Adi Dharmi,Adi Dravida,Adi Karnataka,Agamudayar,Agarwal,Agnikula Kshatriya,Agri,Ahir,Ahom,Ambalavasi,Arcot,Arekatica,Arora,Arunthathiyar,Arya Vysya,Aryasamaj,Ayyaraka,Badaga,Baghel/Pal/Gaderiya,Bahi,Baidya,Baishnab,Baishya,Bajantri,Balija,Balija - Naidu,anayat Oriya,Banik,Baniya,Barai,Bari,Barnwal,Barujibi,Bengali,Besta'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',')


GO


