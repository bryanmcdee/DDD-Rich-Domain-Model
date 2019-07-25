USE [OnlineGym]
IF NOT EXISTS (SELECT TOP 1 * FROM dbo.Athlete)
BEGIN	
PRINT 'Seeding Athletes'
SET IDENTITY_INSERT [dbo].[Athlete] ON 
INSERT [dbo].[Athlete] ([AthleteId], [Name], [Email], [Status], [StatusExpirationDate], [MoneySpent]) VALUES (1, N'Alan Turing', N'alan.turing@gmail.com', 1, NULL, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[Athlete] ([AthleteId], [Name], [Email], [Status], [StatusExpirationDate], [MoneySpent]) VALUES (2, N'Larry Page', N'larry.page@yahoo.com', 2, CAST(N'2018-10-14 01:37:27.000' AS DateTime), CAST(4.00 AS Decimal(18, 2)))
INSERT [dbo].[Athlete] ([AthleteId], [Name], [Email], [Status], [StatusExpirationDate], [MoneySpent]) VALUES (3, N'Grace Hopper', N'grace.hopper@gmail.com', 1, NULL, CAST(8.00 AS Decimal(18, 2)))
INSERT [dbo].[Athlete] ([AthleteId], [Name], [Email], [Status], [StatusExpirationDate], [MoneySpent]) VALUES (4, N'Steve Wozniak', N'steve.wozniak@gmail.com', 1, NULL, CAST(2.00 AS Decimal(18, 2)))
INSERT [dbo].[Athlete] ([AthleteId], [Name], [Email], [Status], [StatusExpirationDate], [MoneySpent]) VALUES (5, N'Linus Torvalds', N'linus.torvalds@gmail.com', 1, NULL, CAST(4.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Athlete] OFF
END	

IF NOT EXISTS (SELECT TOP 1 * FROM dbo.WorkoutRoutine)
BEGIN
PRINT 'Seeding WorkoutRoutines'
SET IDENTITY_INSERT [dbo].[WorkoutRoutine] ON 
INSERT [dbo].[WorkoutRoutine] ([WorkoutRoutineId], [Name], [LicensingModel]) VALUES (2, N'CrossFit', 1)
INSERT [dbo].[WorkoutRoutine] ([WorkoutRoutineId], [Name], [LicensingModel]) VALUES (1, N'P90X', 2)
INSERT [dbo].[WorkoutRoutine] ([WorkoutRoutineId], [Name], [LicensingModel]) VALUES (2, N'Jazzercise', 3)
SET IDENTITY_INSERT [dbo].[WorkoutRoutine] OFF
END

IF NOT EXISTS (SELECT TOP 1 * FROM dbo.PurchasedWorkoutRoutine)
BEGIN	
PRINT 'Seeding PurchasedWorkoutRoutines'
SET IDENTITY_INSERT [dbo].[PurchasedWorkoutRoutine] ON 
INSERT [dbo].[PurchasedWorkoutRoutine] ([PurchasedWorkoutRoutineId], [WorkoutRoutineId], [AthleteId], [Price], [PurchaseDate], [ExpirationDate]) VALUES (1, 1, 1, CAST(2.00 AS Decimal(18, 2)), CAST(N'2017-09-16 16:30:05.773' AS DateTime), CAST(N'2017-09-18 00:00:00.000' AS DateTime))
INSERT [dbo].[PurchasedWorkoutRoutine] ([PurchasedWorkoutRoutineId], [WorkoutRoutineId], [AthleteId], [Price], [PurchaseDate], [ExpirationDate]) VALUES (2, 2, 3, CAST(4.00 AS Decimal(18, 2)), CAST(N'2017-09-15 15:30:05.773' AS DateTime), CAST(N'2017-10-09 23:54:22.000' AS DateTime))
INSERT [dbo].[PurchasedWorkoutRoutine] ([PurchasedWorkoutRoutineId], [WorkoutRoutineId], [AthleteId], [Price], [PurchaseDate], [ExpirationDate]) VALUES (3, 3, 3, CAST(8.00 AS Decimal(18, 2)), CAST(N'2017-10-07 23:54:22.000' AS DateTime), NULL)
INSERT [dbo].[PurchasedWorkoutRoutine] ([PurchasedWorkoutRoutineId], [WorkoutRoutineId], [AthleteId], [Price], [PurchaseDate], [ExpirationDate]) VALUES (4, 1, 4, CAST(2.00 AS Decimal(18, 2)), CAST(N'2017-10-15 13:26:19.000' AS DateTime), CAST(N'2017-10-17 13:26:19.000' AS DateTime))
INSERT [dbo].[PurchasedWorkoutRoutine] ([PurchasedWorkoutRoutineId], [WorkoutRoutineId], [AthleteId], [Price], [PurchaseDate], [ExpirationDate]) VALUES (5, 2, 5, CAST(4.00 AS Decimal(18, 2)), CAST(N'2017-10-22 16:06:51.000' AS DateTime), CAST(N'2017-10-24 16:06:51.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[PurchasedWorkoutRoutine] OFF
END