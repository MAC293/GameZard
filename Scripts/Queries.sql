SELECT * FROM Emulator;
SELECT * FROM Emulator_Savedata;

UPDATE Emulator
SET Is_Selected = 0
WHERE Is_Selected = 1;