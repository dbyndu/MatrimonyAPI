USE [master]
GO
/****** Object:  Database [Matrimony]    Script Date: 08-03-2020 02:09:50 ******/
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
/****** Object:  Table [dbo].[MasterFieldValue]    Script Date: 08-03-2020 02:09:50 ******/
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
/****** Object:  Table [dbo].[MasterTableMetadata]    Script Date: 08-03-2020 02:09:50 ******/
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
/****** Object:  Table [dbo].[PreferenceMaster]    Script Date: 08-03-2020 02:09:50 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 08-03-2020 02:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[MiddleNmae] [varchar](50) NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[ProfileCreatedForId] [int] NOT NULL,
	[ContactName] [varchar](100) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserBasicInfo]    Script Date: 08-03-2020 02:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBasicInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[GenderId] [int] NOT NULL,
	[DOB] [date] NOT NULL,
	[MaritalStatusId] [int] NULL,
	[Height] [int] NULL,
	[Weight] [int] NULL,
	[HealthInfoId] [int] NULL,
	[IsDisability] [bit] NULL,
	[BloodGroupId] [int] NULL,
	[ReligionId] [int] NULL,
	[MotherTongueId] [int] NULL,
	[ComunityId] [int] NULL,
	[Gothra] [varchar](100) NULL,
	[IsIgnorCast] [bit] NULL,
	[About] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserBasicInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCareer]    Script Date: 08-03-2020 02:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCareer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[WorkingSectorId] [int] NULL,
	[WorkDesignationId] [int] NULL,
	[EmployerId] [int] NULL,
	[AnualIncomeId] [int] NULL,
	[IsDisplayIncome] [bit] NULL,
 CONSTRAINT [PK_UserCareer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEducation]    Script Date: 08-03-2020 02:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEducation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[EducationLevelId] [int] NULL,
	[EducationFieldId] [int] NULL,
	[Institution] [varchar](250) NULL,
	[University] [varchar](250) NULL,
 CONSTRAINT [PK_UserEducation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFamilyInfo]    Script Date: 08-03-2020 02:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFamilyInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FatherStatusId] [int] NULL,
	[MotherStatusId] [int] NULL,
	[NativePlace] [varchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[FamilyLocation] [varchar](100) NULL,
	[MarriedSiblingMale] [int] NULL,
	[NotMarriedSiblingMale] [int] NULL,
	[MarriedSiblingFemale] [int] NULL,
	[NotMarriedSiblingFemale] [int] NULL,
	[FamilyTypeId] [int] NULL,
	[FamilyValuesId] [int] NULL,
	[FamilyAffluenceId] [int] NULL,
 CONSTRAINT [PK_UserFamilyInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLifeStyle]    Script Date: 08-03-2020 02:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLifeStyle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DietId] [int] NULL,
	[Hobies] [varchar](500) NULL,
	[SmokingId] [int] NULL,
	[ChildrenChoiceId] [int] NULL,
	[WeadingStyleId] [int] NULL,
 CONSTRAINT [PK_UserLifeStyle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLocation]    Script Date: 08-03-2020 02:09:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLocation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CountryId] [int] NULL,
	[StateId] [int] NULL,
	[City] [varchar](100) NULL,
	[GrewUpIn] [varchar](100) NULL,
	[Origin] [varchar](100) NULL,
	[PIN] [bigint] NULL,
 CONSTRAINT [PK_UserLocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPreferenceSetting]    Script Date: 08-03-2020 02:09:50 ******/
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
ALTER TABLE [dbo].[UserFamilyInfo] ADD  CONSTRAINT [DF_UserFamilyInfo_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
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
ALTER TABLE [dbo].[UserBasicInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserBasicInfo_MasterFieldValueBlood] FOREIGN KEY([BloodGroupId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserBasicInfo] CHECK CONSTRAINT [FK_UserBasicInfo_MasterFieldValueBlood]
GO
ALTER TABLE [dbo].[UserBasicInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserBasicInfo_MasterFieldValueComunity] FOREIGN KEY([ComunityId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserBasicInfo] CHECK CONSTRAINT [FK_UserBasicInfo_MasterFieldValueComunity]
GO
ALTER TABLE [dbo].[UserBasicInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserBasicInfo_MasterFieldValueGender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserBasicInfo] CHECK CONSTRAINT [FK_UserBasicInfo_MasterFieldValueGender]
GO
ALTER TABLE [dbo].[UserBasicInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserBasicInfo_MasterFieldValueHealth] FOREIGN KEY([HealthInfoId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserBasicInfo] CHECK CONSTRAINT [FK_UserBasicInfo_MasterFieldValueHealth]
GO
ALTER TABLE [dbo].[UserBasicInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserBasicInfo_MasterFieldValueMotherTongue] FOREIGN KEY([MotherTongueId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserBasicInfo] CHECK CONSTRAINT [FK_UserBasicInfo_MasterFieldValueMotherTongue]
GO
ALTER TABLE [dbo].[UserBasicInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserBasicInfo_MasterFieldValueReligion] FOREIGN KEY([ReligionId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserBasicInfo] CHECK CONSTRAINT [FK_UserBasicInfo_MasterFieldValueReligion]
GO
ALTER TABLE [dbo].[UserBasicInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserBasicInfo_MasterFieldValueStatus] FOREIGN KEY([MaritalStatusId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserBasicInfo] CHECK CONSTRAINT [FK_UserBasicInfo_MasterFieldValueStatus]
GO
ALTER TABLE [dbo].[UserBasicInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserBasicInfo_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserBasicInfo] CHECK CONSTRAINT [FK_UserBasicInfo_User]
GO
ALTER TABLE [dbo].[UserCareer]  WITH CHECK ADD  CONSTRAINT [FK_UserCareer_MasterFieldValueAnualIncome] FOREIGN KEY([AnualIncomeId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserCareer] CHECK CONSTRAINT [FK_UserCareer_MasterFieldValueAnualIncome]
GO
ALTER TABLE [dbo].[UserCareer]  WITH CHECK ADD  CONSTRAINT [FK_UserCareer_MasterFieldValueDesignation] FOREIGN KEY([WorkDesignationId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserCareer] CHECK CONSTRAINT [FK_UserCareer_MasterFieldValueDesignation]
GO
ALTER TABLE [dbo].[UserCareer]  WITH CHECK ADD  CONSTRAINT [FK_UserCareer_MasterFieldValueEmployer] FOREIGN KEY([EmployerId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserCareer] CHECK CONSTRAINT [FK_UserCareer_MasterFieldValueEmployer]
GO
ALTER TABLE [dbo].[UserCareer]  WITH CHECK ADD  CONSTRAINT [FK_UserCareer_MasterFieldValueSector] FOREIGN KEY([WorkingSectorId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserCareer] CHECK CONSTRAINT [FK_UserCareer_MasterFieldValueSector]
GO
ALTER TABLE [dbo].[UserCareer]  WITH CHECK ADD  CONSTRAINT [FK_UserCareer_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCareer] CHECK CONSTRAINT [FK_UserCareer_User]
GO
ALTER TABLE [dbo].[UserEducation]  WITH CHECK ADD  CONSTRAINT [FK_UserEducation_MasterFieldValueField] FOREIGN KEY([EducationFieldId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserEducation] CHECK CONSTRAINT [FK_UserEducation_MasterFieldValueField]
GO
ALTER TABLE [dbo].[UserEducation]  WITH CHECK ADD  CONSTRAINT [FK_UserEducation_MasterFieldValueLevel] FOREIGN KEY([EducationLevelId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserEducation] CHECK CONSTRAINT [FK_UserEducation_MasterFieldValueLevel]
GO
ALTER TABLE [dbo].[UserEducation]  WITH CHECK ADD  CONSTRAINT [FK_UserEducation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserEducation] CHECK CONSTRAINT [FK_UserEducation_User]
GO
ALTER TABLE [dbo].[UserFamilyInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueAffluence] FOREIGN KEY([FamilyAffluenceId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserFamilyInfo] CHECK CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueAffluence]
GO
ALTER TABLE [dbo].[UserFamilyInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueFamilyType] FOREIGN KEY([FamilyTypeId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserFamilyInfo] CHECK CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueFamilyType]
GO
ALTER TABLE [dbo].[UserFamilyInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueFather] FOREIGN KEY([FatherStatusId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserFamilyInfo] CHECK CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueFather]
GO
ALTER TABLE [dbo].[UserFamilyInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueMother] FOREIGN KEY([MotherStatusId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserFamilyInfo] CHECK CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueMother]
GO
ALTER TABLE [dbo].[UserFamilyInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueValues] FOREIGN KEY([FamilyValuesId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserFamilyInfo] CHECK CONSTRAINT [FK_UserFamilyInfo_MasterFieldValueValues]
GO
ALTER TABLE [dbo].[UserFamilyInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserFamilyInfo_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserFamilyInfo] CHECK CONSTRAINT [FK_UserFamilyInfo_User]
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
ALTER TABLE [dbo].[UserLocation]  WITH CHECK ADD  CONSTRAINT [FK_UserLocation_MasterFieldValueCountry] FOREIGN KEY([CountryId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserLocation] CHECK CONSTRAINT [FK_UserLocation_MasterFieldValueCountry]
GO
ALTER TABLE [dbo].[UserLocation]  WITH CHECK ADD  CONSTRAINT [FK_UserLocation_MasterFieldValueState] FOREIGN KEY([StateId])
REFERENCES [dbo].[MasterFieldValue] ([Id])
GO
ALTER TABLE [dbo].[UserLocation] CHECK CONSTRAINT [FK_UserLocation_MasterFieldValueState]
GO
ALTER TABLE [dbo].[UserLocation]  WITH CHECK ADD  CONSTRAINT [FK_UserLocation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLocation] CHECK CONSTRAINT [FK_UserLocation_User]
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
