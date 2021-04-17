using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    public static void SaveGameData() {
        //Save Game Data
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/MirrorChromeSave.Dat";
        FileStream fs = new FileStream(path, FileMode.Create);
       
        formatter.Serialize(fs, SetSaveData());
        fs.Close();
    }

    public static void LoadSaveData() {
        //Load save Data
        string path = Application.persistentDataPath + "/MirrorChromeSave.Dat";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            SaveData data = (SaveData)formatter.Deserialize(fs);
            fs.Close();

            Debug.LogError("Number of stages loaded:" + Stage_Data.GetNumberOfStages());
            for (int i = 0; i < Stage_Data.GetNumberOfStages(); i++) {
                Stage_Data.LoadSavedStageData(i, data);
            }
        }
    }

    public static SaveData SetSaveData() {
        //Set Data savable
        SaveData data = new SaveData();
        data.InitialiseLevelData(Stage_Data.GetNumberOfStages());
        for (int i = 0; i < Stage_Data.GetNumberOfStages(); i++) {
            StageData tempstageData = new StageData();
            Stage_Data.GetStageData(i, tempstageData);
            data.AddStageData(i, tempstageData.bestTime, tempstageData.collectableGot, tempstageData.numberOfFlips);
        }
        Debug.LogError("Number of stages Saved:" + Stage_Data.GetNumberOfStages());
        data.SetProgress(Stage_Data.GetProgress());
        return data;
    }

    public static void DeleteSaveData() {
        //Delete Save File if exists
        string path = Application.persistentDataPath + "/MirrorChromeSave.Dat";
        if (File.Exists(path)) {
            File.Delete(path);
        }
        Debug.Log("File Does not exist");
    }
}
