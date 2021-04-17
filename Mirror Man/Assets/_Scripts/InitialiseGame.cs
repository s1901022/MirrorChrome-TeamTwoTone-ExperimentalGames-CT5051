using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseGame : MonoBehaviour {
    public List<StageData> GameStages;

    private void Start() {
        //Initialise Scenes
        Stage_Data.Initialise(GameStages);
        Stage_Loader.InitialiseStageManager();
        Stage_Loader.LoadSceneSafe("TwoTone");
    }
}
