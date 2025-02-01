USE [Healthcare]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Role_ID], [Name], [Description]) VALUES (1, N'ADMIN', N'the user has access to the entire system')
INSERT [dbo].[Roles] ([Role_ID], [Name], [Description]) VALUES (2, N'USER', N'user has limited access')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
