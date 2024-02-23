CREATE TABLE [dbo].[Content]
(
	[Description] VARCHAR(255) NOT NULL,
	[CareerName] VARCHAR(30) NOT NULL,
	[ContentTypeId] VARCHAR(30) NOT NULL, 
	PRIMARY KEY ([Description], [CareerName]),
	FOREIGN KEY ([CareerName]) REFERENCES [Career]([Name]),
	FOREIGN KEY ([ContentTypeId]) REFERENCES [ContentType]([Id])
)
