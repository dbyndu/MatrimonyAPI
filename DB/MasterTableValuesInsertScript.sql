USE [Matrimony]
GO

DECLARE @tableId VARCHAR(100)
DECLARE @Values NVARCHAR(MAX)
/****** Profile Created For  ******/
--SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'ProfileCreatedFor'
--SET @Values = 'Self,Son,Daughter,Brother,Sister,Relative/Friend,Client-Marriage Bureau'
--INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
--SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',')  


--/****** Gender  ******/
--SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'Gender'
--SET @Values = 'Male,Female'
--INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
--SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',')  

--/****** Religion  ******/
--SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'Religion'
--SET @Values = 'Hindu,Muslim,Christian,Sikh,Parsi,Jain,Buddhist,Jewish,No Religion,Spiritual,Other'
--INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
--SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',')  

--/****** MotherTongue  ******/
--SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'MotherTongue'
--SET @Values = 'Bengali,English,Gujarati,Hindi,Kannada,Konkani,Malayalam,Marathi,Marwari,Odia,Punjabi'
--INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
--SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',')  

/****** BodyType  ******/
SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'BodyType'
SET @Values = 'Slim,Athletic,Average,Heavy'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',') 
/****** Compexion  ******/
SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'Compexion'
SET @Values = 'Very Fair,Fair,Whiteish,Whiteish Brown,Dark'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',') 

/****** BloodGroup  ******/

SELECT @tableId = Id FROM MasterTableMetadata WHERE TableName = 'BloodGroup'
SET @Values = 'A+,O+,B+,AB+,A-,O-,B-,AB-'
INSERT INTO [dbo].[MasterFieldValue] ([Value],[MasterTableId])
SELECT value,@tableId  FROM STRING_SPLIT ( @Values, ',') 

GO

