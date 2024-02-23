CREATE TABLE [dbo].[CareerAreas]
(
	[CareerName] VARCHAR(30) NOT NULL,
	[AreaType] VARCHAR(30) NOT NULL,
	PRIMARY KEY ([CareerName], [AreaType]),
	FOREIGN KEY ([CareerName]) REFERENCES [Career]([Name]),
	FOREIGN KEY ([AreaType]) REFERENCES [Area]([Type])
)
