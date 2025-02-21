USE [Healthcare]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 1/02/2025 3:38:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Patient_ID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [nvarchar](max) NOT NULL,
	[Family_Name] [nvarchar](max) NOT NULL,
	[Identification] [nvarchar](max) NOT NULL,
	[Birth_Date] [datetime2](7) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone_Number] [nvarchar](max) NULL,
	[Registration_Date] [datetime2](7) NOT NULL,
	[Modify_Date] [datetime2](7) NULL,
	[Registration_User_ID] [int] NOT NULL,
	[Update_User_ID] [int] NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[Patient_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
