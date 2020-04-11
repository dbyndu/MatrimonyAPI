USE [master]
GO
/****** Object:  Database [Matrimony]    Script Date: 06-04-2020 01:38:08 ******/
CREATE DATABASE [Matrimony]
 
GO
ALTER DATABASE [Matrimony] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Matrimony].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Matrimony] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Matrimony] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Matrimony] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Matrimony] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Matrimony] SET ARITHABORT OFF 
GO
ALTER DATABASE [Matrimony] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Matrimony] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Matrimony] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Matrimony] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Matrimony] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Matrimony] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Matrimony] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Matrimony] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Matrimony] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Matrimony] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Matrimony] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Matrimony] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Matrimony] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Matrimony] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Matrimony] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Matrimony] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Matrimony] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Matrimony] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Matrimony] SET  MULTI_USER 
GO
ALTER DATABASE [Matrimony] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Matrimony] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Matrimony] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Matrimony] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Matrimony] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Matrimony] SET QUERY_STORE = OFF
GO
USE [Matrimony]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[StateId] [int] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Cities__A09B75FBE449741E] UNIQUE NONCLUSTERED 
(
	[StateId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] NOT NULL,
	[Sortname] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Phonecode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterFieldValue]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterFieldValue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](250) NOT NULL,
	[MasterTableId] [int] NOT NULL,
 CONSTRAINT [PK_MasterFieldValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterTableMetadata]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterTableMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MasterTableMetadata] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PreferenceMaster]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreferenceMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [varchar](250) NOT NULL,
	[Constrained] [bit] NOT NULL,
	[DataType] [varchar](100) NOT NULL,
	[ReferenceTableId] [int] NULL,
 CONSTRAINT [PK_PreferenceMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[States]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleNmae] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[ProfileCreatedForId] [int] NOT NULL,
	[ContactName] [nvarchar](100) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserImage]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Image] [varbinary](max) NOT NULL,
	[ContentType] [varchar](50) NULL,
	[IsVisible] [bit] NULL,
	[IsProfilePicture] [bit] NULL,
	[ImageOrder] [int] NULL,
	[IsApproved] [bit] NULL,
	[CreatedDate] [date] NULL,
 CONSTRAINT [PK_UserImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[GenderId] [int] NULL,
	[DOB] [date] NULL,
	[MaritalStatusId] [int] NULL,
	[Height] [int] NULL,
	[Weight] [int] NULL,
	[BodyTypeId] [int] NULL,
	[ComplexionId] [int] NULL,
	[IsDisability] [bit] NULL,
	[BloodGroupId] [int] NULL,
	[ReligionId] [int] NULL,
	[Caste] [nvarchar](100) NULL,
	[MotherTongueId] [int] NULL,
	[ComunityId] [int] NULL,
	[Gothra] [nvarchar](100) NULL,
	[IsIgnorCast] [bit] NULL,
	[CountryId] [int] NULL,
	[CitizenshipId] [int] NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
	[GrewUpIn] [nvarchar](100) NULL,
	[Origin] [nvarchar](100) NULL,
	[PIN] [bigint] NULL,
	[HighestQualificationId] [int] NULL,
	[HighestSpecializationId] [int] NULL,
	[SecondaryQualificationId] [int] NULL,
	[SecondarySpecializationId] [int] NULL,
	[Institution] [nvarchar](200) NULL,
	[University] [nvarchar](200) NULL,
	[WorkingSectorId] [int] NULL,
	[WorkDesignationId] [int] NULL,
	[EmployerId] [int] NULL,
	[AnualIncomeId] [int] NULL,
	[IsDisplayIncome] [bit] NULL,
	[FatherStatusId] [int] NULL,
	[MotherStatusId] [int] NULL,
	[NativePlace] [nvarchar](100) NULL,
	[FamilyLocation] [nvarchar](100) NULL,
	[MarriedSiblingMale] [int] NULL,
	[NotMarriedSiblingMale] [int] NULL,
	[MarriedSiblingFemale] [int] NULL,
	[NotMarriedSiblingFemale] [int] NULL,
	[FamilyTypeId] [int] NULL,
	[FamilyValuesId] [int] NULL,
	[FamilyIncomeID] [int] NULL,
	[About] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLifeStyle]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLifeStyle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DietId] [int] NULL,
	[Hobies] [nvarchar](500) NULL,
	[SmokingId] [int] NULL,
	[ChildrenChoiceId] [int] NULL,
	[WeadingStyleId] [int] NULL,
	[DrinkingId] [int] NULL,
	[HouseLivingInId] [int] NULL,
	[OwnCar] [bit] NULL,
	[OwnPet] [bit] NULL,
	[Interests] [nvarchar](500) NULL,
	[Musics] [nvarchar](500) NULL,
	[Books] [nvarchar](500) NULL,
	[Movies] [nvarchar](500) NULL,
	[Fitness] [nvarchar](500) NULL,
	[Cuisines] [nvarchar](500) NULL,
 CONSTRAINT [PK_UserLifeStyle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPreferenceSetting]    Script Date: 06-04-2020 01:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPreferenceSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PreferenceMasterId] [int] NOT NULL,
	[FieldValueId] [int] NULL,
	[UnconstraineValue] [varchar](250) NULL,
 CONSTRAINT [PK_UserPreferenceSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserImage] ADD  CONSTRAINT [DF_UserImage_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_States] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_States]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Countries_Countries] FOREIGN KEY([Id])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Countries_Countries]
GO
ALTER TABLE [dbo].[MasterFieldValue]  WITH CHECK ADD  CONSTRAINT [FK_MasterFieldValue_MasterTableMetadata] FOREIGN KEY([MasterTableId])
REFERENCES [dbo].[MasterTableMetadata] ([Id])
GO
ALTER TABLE [dbo].[MasterFieldValue] CHECK CONSTRAINT [FK_MasterFieldValue_MasterTableMetadata]
GO
ALTER TABLE [dbo].[PreferenceMaster]  WITH CHECK ADD  CONSTRAINT [FK_PreferenceMaster_MasterTableMetadata] FOREIGN KEY([ReferenceTableId])
REFERENCES [dbo].[MasterTableMetadata] ([Id])
GO
ALTER TABLE [dbo].[PreferenceMaster] CHECK CONSTRAINT [FK_PreferenceMaster_MasterTableMetadata]
GO
ALTER TABLE [dbo].[States]  WITH CHECK ADD  CONSTRAINT [FK_States_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[States] CHECK CONSTRAINT [FK_States_Countries]
GO
ALTER TABLE [dbo].[UserImage]  WITH CHECK ADD  CONSTRAINT [FK_UserImage_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserImage] CHECK CONSTRAINT [FK_UserImage_User]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_Cities]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_Countries]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueAnualIncome] FOREIGN KEY([AnualIncomeId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueAnualIncome]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueBlood] FOREIGN KEY([BloodGroupId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueBlood]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueBody] FOREIGN KEY([BodyTypeId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueBody]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueCitizenship] FOREIGN KEY([CitizenshipId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueCitizenship]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueComplexion] FOREIGN KEY([ComplexionId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueComplexion]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueComunity] FOREIGN KEY([ComunityId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueComunity]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueEmployer] FOREIGN KEY([EmployerId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueEmployer]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueFamilyIncome] FOREIGN KEY([FamilyIncomeID])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueFamilyIncome]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueFamilyType] FOREIGN KEY([FamilyTypeId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueFamilyType]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueFamilyValues] FOREIGN KEY([FamilyValuesId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueFamilyValues]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueFatherStatus] FOREIGN KEY([FatherStatusId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueFatherStatus]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueGender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueGender]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueHighestQualification] FOREIGN KEY([HighestQualificationId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueHighestQualification]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueHighestSpecialization] FOREIGN KEY([HighestSpecializationId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueHighestSpecialization]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueMotherStatus] FOREIGN KEY([MotherStatusId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueMotherStatus]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueMotherTongue] FOREIGN KEY([MotherTongueId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueMotherTongue]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueReligion] FOREIGN KEY([ReligionId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueReligion]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueSecondaryQualification] FOREIGN KEY([SecondaryQualificationId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueSecondaryQualification]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueSecondarySpecialization] FOREIGN KEY([SecondarySpecializationId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueSecondarySpecialization]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueStatus] FOREIGN KEY([MaritalStatusId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueStatus]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueWorkDesignation] FOREIGN KEY([WorkDesignationId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueWorkDesignation]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_MasterFieldValueWorkingSector] FOREIGN KEY([WorkingSectorId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_MasterFieldValueWorkingSector]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_States] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_States]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_User]
GO
ALTER TABLE [dbo].[UserLifeStyle]  WITH CHECK ADD  CONSTRAINT [FK_UserLifeStyle_MasterFieldValueChildrenChoice] FOREIGN KEY([ChildrenChoiceId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserLifeStyle] CHECK CONSTRAINT [FK_UserLifeStyle_MasterFieldValueChildrenChoice]
GO
ALTER TABLE [dbo].[UserLifeStyle]  WITH CHECK ADD  CONSTRAINT [FK_UserLifeStyle_MasterFieldValueDiet] FOREIGN KEY([DietId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserLifeStyle] CHECK CONSTRAINT [FK_UserLifeStyle_MasterFieldValueDiet]
GO
ALTER TABLE [dbo].[UserLifeStyle]  WITH CHECK ADD  CONSTRAINT [FK_UserLifeStyle_MasterFieldValueDrinking] FOREIGN KEY([DrinkingId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserLifeStyle] CHECK CONSTRAINT [FK_UserLifeStyle_MasterFieldValueDrinking]
GO
ALTER TABLE [dbo].[UserLifeStyle]  WITH CHECK ADD  CONSTRAINT [FK_UserLifeStyle_MasterFieldValueHouseLiving] FOREIGN KEY([HouseLivingInId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserLifeStyle] CHECK CONSTRAINT [FK_UserLifeStyle_MasterFieldValueHouseLiving]
GO
ALTER TABLE [dbo].[UserLifeStyle]  WITH CHECK ADD  CONSTRAINT [FK_UserLifeStyle_MasterFieldValueSmoking] FOREIGN KEY([SmokingId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserLifeStyle] CHECK CONSTRAINT [FK_UserLifeStyle_MasterFieldValueSmoking]
GO
ALTER TABLE [dbo].[UserLifeStyle]  WITH CHECK ADD  CONSTRAINT [FK_UserLifeStyle_MasterFieldValueWeadingStyle] FOREIGN KEY([WeadingStyleId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserLifeStyle] CHECK CONSTRAINT [FK_UserLifeStyle_MasterFieldValueWeadingStyle]
GO
ALTER TABLE [dbo].[UserLifeStyle]  WITH CHECK ADD  CONSTRAINT [FK_UserLifeStyle_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLifeStyle] CHECK CONSTRAINT [FK_UserLifeStyle_User]
GO
ALTER TABLE [dbo].[UserPreferenceSetting]  WITH CHECK ADD  CONSTRAINT [FK_UserPreferenceSetting_MasterFieldValue] FOREIGN KEY([FieldValueId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserPreferenceSetting] CHECK CONSTRAINT [FK_UserPreferenceSetting_MasterFieldValue]
GO
ALTER TABLE [dbo].[UserPreferenceSetting]  WITH CHECK ADD  CONSTRAINT [FK_UserPreferenceSetting_PreferenceMaster] FOREIGN KEY([PreferenceMasterId])
REFERENCES [dbo].[PreferenceMaster] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPreferenceSetting] CHECK CONSTRAINT [FK_UserPreferenceSetting_PreferenceMaster]
GO
ALTER TABLE [dbo].[UserPreferenceSetting]  WITH CHECK ADD  CONSTRAINT [FK_UserPreferenceSetting_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPreferenceSetting] CHECK CONSTRAINT [FK_UserPreferenceSetting_User]
GO
USE [master]
GO
ALTER DATABASE [Matrimony] SET  READ_WRITE 
GO
