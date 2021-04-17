﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Stage_Loader {
    //List of Scenes
    private static List<string> scenesInBuild;

    private static float loadProgress;

    public static void InitialiseStageManager() {
        GetBuiltScenes();
    }
    public static void LoadSceneSafe(string a_sceneToLoad) {
        //Load scenes asynchronously
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(a_sceneToLoad);
        loadProgress = loadingOperation.progress;
    }

    private static void GetBuiltScenes() {
    }
}
