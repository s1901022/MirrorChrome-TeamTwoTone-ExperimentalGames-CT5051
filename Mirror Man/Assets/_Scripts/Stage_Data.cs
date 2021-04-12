using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Stage_Data
{
    //progression starts at 0
    static int Progression = 9;
    static List<StageData> stageDatas = new List<StageData>();

    public static void Initialise(List<StageData> a_loadedStageData)
    {
        for (int i = 0; i < a_loadedStageData.Count; i++)
        {
            stageDatas.Add(a_loadedStageData[i]);
            Debug.LogError(a_loadedStageData[i].stageNumber);
            Debug.LogError(stageDatas[i].stageNumber);
        }

    }

    public static int GetStageNumber(int a_index) { return stageDatas[a_index].stageNumber; }
    public static string GetStageName(int a_index) { return stageDatas[a_index].stageName; }
    public static string GetStageBuildName(int a_index) { return stageDatas[a_index].stageBuildName; }
    public static float GetTargetTime(int a_index) { return stageDatas[a_index].targetTime; }
    public static int GetTargetFlips(int a_index) { return stageDatas[a_index].targetnumberOfFlips; }
    public static Sprite GetIconSprite(int a_index) { return stageDatas[a_index].stageSelectIcon; }
    public static float GetBestTime(int a_index) { return stageDatas[a_index].bestTime; }
    public static int GetFlips(int a_index) { return stageDatas[a_index].numberOfFlips; }
    public static bool GetCollectableBool(int a_index) { return stageDatas[a_index].collectableGot; }

    public static void GetStageData(int stageNumber, StageData a_stageData)
    {
        for (int i = 0; i < stageDatas.Count; i++)
        {
            if (stageNumber == stageDatas[i].stageNumber)
            {
                a_stageData.stageNumber = stageDatas[i].stageNumber;
                a_stageData.stageName = stageDatas[i].stageName;
                a_stageData.stageBuildName = stageDatas[i].stageBuildName;
                a_stageData.bestTime = stageDatas[i].bestTime;
                a_stageData.collectableGot = stageDatas[i].collectableGot;
                a_stageData.targetnumberOfFlips = stageDatas[i].targetnumberOfFlips;
                a_stageData.numberOfFlips = stageDatas[i].numberOfFlips;
            }
            return;
        }
        Debug.LogError("Stage does not exist");
    }

    public static void SetStageData(StageData a_stageData)
    {
        for (int i = 0; i < stageDatas.Count; i++)
        {
            if (a_stageData.stageNumber == stageDatas[i].stageNumber)
            {
                stageDatas[i] = a_stageData;
            }
            return;
        }
        Debug.LogError("Stage does not exist");
    }

    public static void LoadSavedStageData(int a_stageNumber, SaveData a_saveData)
    {
        if (a_stageNumber < stageDatas.Count)
        {
            stageDatas[a_stageNumber].bestTime = a_saveData.GetBestTime(a_stageNumber);
            stageDatas[a_stageNumber].collectableGot = a_saveData.GetCollectables(a_stageNumber);
            stageDatas[a_stageNumber].numberOfFlips = a_saveData.GetNumberOfFlips(a_stageNumber);
        }
    }

    public static int GetNumberOfStages()
    {
        return stageDatas.Count;
    }

    public static int GetProgress()
    {
        return Progression;
    }

    public static void SetProgress(int a_progress)
    {
        if (a_progress > Progression)
        {
            Progression = a_progress;
        }
    }
}
