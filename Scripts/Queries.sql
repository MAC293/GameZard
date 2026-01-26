SELECT * FROM Emulator;
SELECT * FROM Emulator_Savedata;

UPDATE Emulator
SET Is_Selected = 0
WHERE Is_Selected = 1;

UPDATE Emulator_Savedata
SET Backup_Mode = 'Manually'
WHERE Backup_Mode = 'Automatically';

UPDATE Emulator_Savedata
SET Backup_Mode = 'Manually'
WHERE ID = 'Save_PPSSPP';

UPDATE Emulator_Savedata
SET Backup_Mode = 'Automatically'
WHERE ID = 'Save_YUZU';

UPDATE Emulator_Savedata
SET From_Path = 'D:\Videogames\Windows\Art Of Rally Polacolour\MonoBleedingEdge\etc\mono\2.0\Browsers'
WHERE ID = 'Save_PPSSPP';

UPDATE Emulator_Savedata
SET To_Path = 'D:\Videogames\Windows\Resident Evil 2\NoDVD\Goldberg\steam_settings'
WHERE ID = 'Save_PPSSPP';

UPDATE Emulator_Savedata
SET Last_Save = '01/04/2021 at 13:00'
WHERE ID = 'Save_PPSSPP';


