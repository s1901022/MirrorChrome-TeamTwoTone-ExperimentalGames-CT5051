using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class About : MonoBehaviour {
    // About screen
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            Stage_Loader.LoadSceneSafe("TitleScreen");
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
