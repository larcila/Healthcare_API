USE [Healthcare]
GO
/****** Object:  Table [dbo].[Roles_By_Users]    Script Date: 1/02/2025 3:42:31 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles_By_Users](
	[Role_By_User_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Role_ID] [int] NOT NULL,
 CONSTRAINT [PK_Roles_By_Users] PRIMARY KEY CLUSTERED 
(
	[Role_By_User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Roles_By_Users]  WITH CHECK ADD  CONSTRAINT [FK_Roles_By_Users_Roles_Role_ID] FOREIGN KEY([Role_ID])
REFERENCES [dbo].[Roles] ([Role_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Roles_By_Users] CHECK CONSTRAINT [FK_Roles_By_Users_Roles_Role_ID]
GO
ALTER TABLE [dbo].[Roles_By_Users]  WITH CHECK ADD  CONSTRAINT [FK_Roles_By_Users_Users_User_ID] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Roles_By_Users] CHECK CONSTRAINT [FK_Roles_By_Users_Users_User_ID]
GO
