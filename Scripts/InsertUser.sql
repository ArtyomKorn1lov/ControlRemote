USE [ControlRemote]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [Login], [Password]) VALUES (1, N'Вадимов Станислав', N'vadimov_s', N'1')
INSERT [dbo].[User] ([Id], [Name], [Login], [Password]) VALUES (2, N'Владимирова Татьяна', N'vladimirova_t', N'1')
INSERT [dbo].[User] ([Id], [Name], [Login], [Password]) VALUES (3, N'Огородникова Оксана', N'oksana', N'1')
INSERT [dbo].[User] ([Id], [Name], [Login], [Password]) VALUES (4, N'Бусыгин Валерий', N'pavel', N'1')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
