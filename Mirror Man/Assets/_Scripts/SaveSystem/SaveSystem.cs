using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGameData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/MirrorChromeSave.Dat";
        FileStream fs = new FileStream(path, FileMode.Create);
       
        formatter.Serialize(fs, SetSaveData());
        fs.Close();
    }

    public static void LoadSaveData()
    {
        string path = Application.persistentDataPath + "/MirrorChromeSave.Dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            SaveData data = (SaveData)formatter.Deserialize(fs);
            fs.Close();

            for (int i = 0; i < Stage_Data.GetNumberOfStages(); i++)
            {
                Stage_Data.LoadSavedStageData(i, data);
            }
        }
    }

    static SaveData SetSaveData()
    {
        SaveData data = new SaveData();
        data.InitialiseLevelData(Stage_Data.GetNumberOfStages());
        for (int i = 0; i < Stage_Data.GetNumberOfStages(); i++)
        {
            StageData tempstageData = new StageData();
            Stage_Data.GetStageData(i, tempstageData);
            data.AddStageData(i, tempstageData.bestTime, tempstageData.collectableGot, tempstageData.numberOfFlips);
        }
        data.SetProgress(Stage_Data.GetProgress());
        return data;
    }
}
