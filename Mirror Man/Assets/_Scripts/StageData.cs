using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stages data in an easily modifialbe formatt

[System.Serializable]
[CreateAssetMenu]
public class StageData : ScriptableObject {
    public int stageNumber;
    public string stageName;
    public string stageBuildName;
    public Sprite stageSelectIcon;

    public float targetTime;
    public int targetnumberOfFlips;
    public int targetJumps;

    public int numberOfJumps;
    public int numberOfFlips;
    public bool collectableGot;
    public float bestTime;
}