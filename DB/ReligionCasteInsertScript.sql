INSERT INTO [dbo].[MasterFieldValue]
SELECT distinct value,3,1,11 FROM STRING_SPLIT
('Ansari,Ansari,Arain,Arain,Awan,Awan,Bengali,Bengali,Dawoodi Bohra,Dawoodi Bohra,Dekkani,Dekkani,Dudekula,Dudekula,Jat,Jat,Khoja,Khoja,Lebbai,Lebbai,Mapila,Mapila,Maraicar,Maraicar,Memon,Memon,Mughal,Mughal,Pathan,Pathan,Qureshi,Qureshi,Rajput,Rajput,Rowther,Rowther,Shafi,Shafi,Sheikh,Sheikh,Shia,Shia,Shia Bohra,Shia Bohra,Shia Imami Ismaili,Shia Imami Ismaili,Shia Ithna ashariyyah,Shia Ithna ashariyyah,Shia Zaidi,Shia Zaidi,Siddiqui,Siddiqui,Sunni,Sunni,Sunni Ehle-Hadith,Sunni Ehle-Hadith,Sunni Hanafi,Sunni Hanafi,Sunni Hunbali,Sunni Hunbali,Sunni Maliki,Sunni Maliki,Sunni Shafi,Sunni Shafi,Syed,Syed',',')

INSERT INTO [dbo].[MasterFieldValue]
SELECT distinct value,3,1,12 FROM STRING_SPLIT
('Anglo Indian,Anglo Indian,Basel Mission,Basel Mission,Born Again,Born Again,Bretheren,Bretheren,CMS,CMS,Cannonite,Cannonite,Catholic,Catholic,Catholic Knanya,Catholic Knanya,Catholic Malankara,Catholic Malankara,Chaldean Syrian,Chaldean Syrian,Cheramar,Cheramar,Christian Nadar,Christian Nadar,Church of North India (CNI),Church of North India (CNI),Church of South India (CSI),Church of South India (CSI),Convert,Convert,Evangelical,Evangelical,IPC,IPC,Indian Orthodox,Indian Orthodox,Intercaste,Intercaste,Jacobite,Jacobite,Jacobite Knanya,Jacobite Knanya,Knanaya,Knanaya,Knanaya Catholic,Knanaya Catholic,Knanaya Jacobite,Knanaya Jacobite,Knanaya Pentecostal,Knanaya Pentecostal,Knanya,Knanya,Latin Catholic,Latin Catholic,Malankara,Malankara,Malankara Catholic,Malankara Catholic,Manglorean,Manglorean,Marthoma,Marthoma,Methodist,Methodist,Mormon,Mormon,Nadar,Nadar,Orthodox,Orthodox,Pentecost,Pentecost,Presbyterian,Presbyterian,Protestant,Protestant,RCSC,RCSC,Roman Catholic,Roman Catholic,Salvation Army,Salvation Army,Scheduled Caste (SC),Scheduled Caste (SC),Scheduled Tribe (ST),Scheduled Tribe (ST),Seventh day Adventist,Seventh day Adventist,Syrian,Syrian,Syrian Catholic,Syrian Catholic,Syrian Orthodox,Syrian Orthodox,Syro Malabar,Syro Malabar',',')