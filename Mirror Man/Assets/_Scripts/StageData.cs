using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public int stageNumber;
    public string stageName;
    public string stageBuildName;
    public Sprite stageSelectIcon;

    public float targetTime;
    public int targetnumberOfFlips;

    public int numberOfFlips;
    public bool collectableGot;
    public float bestTime;
}
