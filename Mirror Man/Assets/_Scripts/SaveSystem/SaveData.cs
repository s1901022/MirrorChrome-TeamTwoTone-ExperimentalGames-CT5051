using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    /* save data
    Whole game:
        Progress (stages unlocked)
    Each stage:
        Best Time
        Number of Flips
        Collectable got
    */

    //Whole game
    int Progression;

    //Each level
        //level - star data
    float[,]    levelData_BestTime;
    bool[,]     levelData_Collectable;
    int [,]     levelData_NumberOfFlips;

    public void InitialiseLevelData(int a_numberOfStages) {
        levelData_BestTime = new float[a_numberOfStages, 1];
        levelData_Collectable = new bool[a_numberOfStages, 1];
        levelData_NumberOfFlips = new int[a_numberOfStages, 1];
    }
    public void AddStageData(int stageNumber, float a_bestTime, bool a_collectable, int a_numberFlips) {
        levelData_BestTime[stageNumber, 0] = a_bestTime;
        levelData_Collectable[stageNumber, 0] = a_collectable;
        levelData_NumberOfFlips[stageNumber, 0] = a_numberFlips;
    }

    public void SetProgress(int a_progression) {
        Progression = a_progression;
    }

    public int GetProgress() { return Progression; }
    public bool GetCollectables(int a_levelIndex) {
        return levelData_Collectable[a_levelIndex, 0];
    }
    public float GetBestTime(int a_levelIndex) {
        return levelData_BestTime[a_levelIndex, 0];
    }
    public int GetNumberOfFlips(int a_levelIndex) {
        return levelData_NumberOfFlips[a_levelIndex, 0];
    }
}

