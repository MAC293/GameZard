SELECT * FROM Emulator;
SELECT * FROM Emulator_Savedata;

UPDATE Emulator
SET Is_Selected = 0
WHERE Is_Selected = 1;

UPDATE Emulator_Savedata
SET Backup_Mode = 'Manually'
WHERE Backup_Mode = 'Automatic';

UPDATE Emulator_Savedata
SET Backup_Mode = 'Manual'
WHERE ID = 'Save_PPSSPP';
