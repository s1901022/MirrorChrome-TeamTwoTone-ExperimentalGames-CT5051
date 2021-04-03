using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scrChangeScene : MonoBehaviour
{
    public string sceneToLoad;

    GameObject canvas;

    void OnTriggerEnter2D(Collider2D col)
    {
        canvas = GameObject.FindGameObjectWithTag("EndLevelCanvas");
        if (col.gameObject.tag == "Player")
        {
            canvas.GetComponent<LevelClearScript>().isActive = true;
        }
    }

    public void Load() {
        Time.timeScale = 1.0f;
        Stage_Loader.LoadSceneSafe(sceneToLoad);
    }
}
