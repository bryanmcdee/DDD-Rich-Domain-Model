DECLARE @DbName NVARCHAR(128);
SET @DbName = N'OnlineGym';

IF NOT (EXISTS (
	SELECT name
	FROM master.dbo.sysdatabases
	WHERE
		(
		'[' + name + ']' = @DbName
		OR name = @DbName
	)
)
)
BEGIN	
	CREATE DATABASE [OnlineGym]
END	