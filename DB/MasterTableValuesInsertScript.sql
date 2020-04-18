USE [Matrimony]
GO

DECLARE @tableId VARCHAR(100)
DECLARE @Values NVARCHAR(MAX)


SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'EducationLevel'
SET @Values = 'Doctorate,Masters,Honours degree,Bachelors,Diploma,High school,Less than high school,Trade school'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',') 

SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'EducationField'
SET @Values = 'Advertising/ Marketing,Administrative services,Architecture,Armed Forces,Arts,Commerce,Computers/ IT,Education'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',') 

SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'WorkingSector'
SET @Values = 'Private Company,Government / Public Sector,Defense / Civil Services,Business / Self Employed,Non Working'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',') 

SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'WorkDesignation'
SET @Values = 'Chartered Accountant,Banking Professional,Investment Professional,Admin Professional,Human Resources Professional,Advertising Professional,Entertainment Professional'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',') 

SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'AnualIncome'
SET @Values = 'Upto INR 1 Lakh,INR 1 Lakh to 2 Lakh,INR 2 Lakh to 4 Lakh'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',')
GO


