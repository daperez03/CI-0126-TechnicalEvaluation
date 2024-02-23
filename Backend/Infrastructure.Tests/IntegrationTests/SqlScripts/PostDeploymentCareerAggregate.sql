USE [TechnicalEvaluation.Database]


MERGE INTO Career AS TARGET
USING (VALUES
	('Computacion', 0, 0),
	('Derecho', 0, 0),
	('Matematica', 0, 0)
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
	('Matematicas'),
	('Ciencias'),
	('Tecnologia'),
	('Historia')
)
AS Source
	([Id])
ON TARGET.[Id] = Source.[Id]
WHEN NOT MATCHED BY TARGET THEN
	INSERT
		([Id])
	VALUES 
		([Id]);		

MERGE INTO Content AS TARGET
USING (VALUES
	('Su escuela fue fundada en 1981', 'Computacion', 'Historia'),
	('Cuenta con un edificio anexo', 'Computacion', 'Historia'),
	('Su facultad fue creada en 1843', 'Derecho', 'Historia')
)
AS Source
	([Description], [CareerName], [ContentTypeId])
ON 
	TARGET.[Description] = Source.[Description] AND
	TARGET.[CareerName] = Source.[CareerName]
WHEN NOT MATCHED BY TARGET THEN
	INSERT
		([Description], [CareerName], [ContentTypeId])
	VALUES
		([Description], [CareerName], [ContentTypeId]);
	

MERGE INTO Area AS TARGET
USING (VALUES
	('Tecnologia'),
	('Leyes'),
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
	('Derecho', 'Leyes'),
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
