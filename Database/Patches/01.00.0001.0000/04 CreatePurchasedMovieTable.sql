USE [OnlineGym];
IF NOT EXISTS (
	SELECT *
	FROM INFORMATION_SCHEMA.TABLES
	WHERE
		TABLE_NAME = N'PurchasedWorkoutRoutine'
)
BEGIN
	CREATE TABLE [dbo].[PurchasedWorkoutRoutine]
	(
		[PurchasedWorkoutRoutineId] [BIGINT] IDENTITY(1, 1) NOT NULL,
		[WorkoutRoutineId] [BIGINT] NOT NULL,
		[AthleteId] [BIGINT] NOT NULL,
		[Price] [DECIMAL](18, 2) NOT NULL,
		[PurchaseDate] [DATETIME] NOT NULL,
		[ExpirationDate] [DATETIME] NULL,
		CONSTRAINT [PK_PurchasedMovie] PRIMARY KEY CLUSTERED ([PurchasedWorkoutRoutineId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
																							ALLOW_PAGE_LOCKS = ON
									   ) ON [PRIMARY]
	) ON [PRIMARY];

	ALTER TABLE [dbo].[PurchasedWorkoutRoutine] WITH CHECK
	ADD CONSTRAINT [FK_PurchasedWorkoutRoutine_Athlete] FOREIGN KEY ([AthleteId]) REFERENCES [dbo].[Athlete] ([AthleteId]);
	ALTER TABLE [dbo].[PurchasedWorkoutRoutine] CHECK CONSTRAINT [FK_PurchasedWorkoutRoutine_Athlete];
	ALTER TABLE [dbo].[PurchasedWorkoutRoutine] WITH CHECK
	ADD CONSTRAINT [FK_PurchasedWorkoutRoutine_WorkoutRoutine] FOREIGN KEY ([WorkoutRoutineId]) REFERENCES [dbo].[WorkoutRoutine] ([WorkoutRoutineId]);
	ALTER TABLE [dbo].[PurchasedWorkoutRoutine] CHECK CONSTRAINT [FK_PurchasedWorkoutRoutine_WorkoutRoutine];
END;


