USE [Healthcare]
GO
/****** Object:  Table [dbo].[Follow_Ups]    Script Date: 1/02/2025 3:38:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Follow_Ups](
	[Follow_Up_ID] [int] IDENTITY(1,1) NOT NULL,
	[Allergy] [nvarchar](max) NOT NULL,
	[Symptom] [nvarchar](max) NOT NULL,
	[Screening] [nvarchar](max) NOT NULL,
	[Patient_ID] [int] NOT NULL,
	[Registration_Date] [datetime2](7) NOT NULL,
	[Modify_Date] [datetime2](7) NULL,
	[Registration_User_ID] [int] NOT NULL,
	[Update_User_ID] [int] NULL,
 CONSTRAINT [PK_Follow_Ups] PRIMARY KEY CLUSTERED 
(
	[Follow_Up_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Follow_Ups_Patient_ID]    Script Date: 1/02/2025 3:38:36 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Follow_Ups_Patient_ID] ON [dbo].[Follow_Ups]
(
	[Patient_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Follow_Ups]  WITH CHECK ADD  CONSTRAINT [FK_Follow_Ups_Patients_Patient_ID] FOREIGN KEY([Patient_ID])
REFERENCES [dbo].[Patients] ([Patient_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Follow_Ups] CHECK CONSTRAINT [FK_Follow_Ups_Patients_Patient_ID]
GO
