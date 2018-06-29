/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if '$(LoadTestData)' = 'True'

Begin

Delete from Treatment;
Delete from [Procedure];
Delete from Pet;
Delete from [Owner];

Insert into [Owner] Values
(1, 'Sinatra', 'Frank', '0400111222'),
(2, 'Ellington' , 'Duke', '0400222333'),
(3, 'Fitzgerald', 'Ella', '0400333444');

Insert into Pet Values
('Buster', 'Dog', 1),
('Fluffy', 'Cat', 1),
('Stew', 'Rabbit', 2),
('Buster', 'Dog', 3),
('Pooper', 'Dog', 3);

Insert into [Procedure] Values
(01, 'Rabies Vaccination', $24.00),
(10, 'Examine and Treat Wound', $30.00),
(05, 'Heart Worm Test', $25.00),
(08, 'Tetnus Vaccination', $17.00);

Insert into Treatment Values
('Buster', 1, 01, '20/Jun/17', 'Routine Vacination', $22.00),
('Buster', 1, 01, '15/May/18', 'Booster Shot', $24.00),
('Fluffy', 1, 10, '10/May/18', 'Wound sustained in apparent cat fight', $30.00),
('Stew', 2, 10, '10/May/18', 'Wounds sustained in attempt to cook the stew', $30.00),
('Pooper', 3, 05, '18/May/18', 'Routine Test', $25.00);


End;