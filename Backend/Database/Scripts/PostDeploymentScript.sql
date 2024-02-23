USE [TechnicalEvaluation.Database]


MERGE INTO Career AS TARGET
USING (VALUES
	('Computacion', 60, 0),
	('Derecho', 40, 0),
	('Matematica', 51, 0)
)
AS Source
	([Name], [WomenPercentage], [ScholarshipBudget])
ON TARGET.[Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
	INSERT
		([Name], [WomenPercentage], [ScholarshipBudget])
	VALUES
		([Name], [WomenPercentage], [ScholarshipBudget]);

MERGE INTO ContentType AS TARGET
USING (VALUES
	('Tecnologico'),
	('Ambiental'),
	('Social')
)
AS Source
	([Id])
ON TARGET.[Id] = Source.[Id]
WHEN NOT MATCHED BY TARGET THEN
	INSERT
		([Id])
	VALUES 
		([Id]);		
	

MERGE INTO Area AS TARGET
USING (VALUES
	('Ciencia'),
	('Tecnologia'),
	('Ingenieria'),
	('Matematica'),
	('Computacion e Informatica')
)
AS Source
	([Type])
ON TARGET.[Type] = Source.[Type]
WHEN NOT MATCHED BY TARGET THEN
	INSERT
		([Type])
	VALUES 
		([Type]);		


MERGE INTO CareerAreas AS TARGET
USING (VALUES
	('Computacion', 'Tecnologia'),
	('Derecho', 'Tecnologia'),
	('Computacion', 'Computacion e Informatica')
)
AS Source
	([CareerName], [AreaType])
ON TARGET.[CareerName] = Source.[CareerName] AND
   TARGET.[AreaType] = Source.[AreaType]
WHEN NOT MATCHED BY TARGET THEN
	INSERT
		([CareerName], [AreaType])
	VALUES
		([CareerName], [AreaType]);
