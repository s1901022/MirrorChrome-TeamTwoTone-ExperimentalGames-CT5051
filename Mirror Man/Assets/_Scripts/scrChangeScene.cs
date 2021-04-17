﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class scrChangeScene : MonoBehaviour {
    //Loading
    public string thisScene;
    public string sceneToLoad;
    private Animator anim;

    //Transition effect variables
    [SerializeField] private float transitionTime;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    Image wipe;

    private void Start() {
        anim = GetComponentInChildren<Animator>();
        wipe.color = new Color(wipe.color.r, wipe.color.g, wipe.color.b, 1.0f);
    }
	private void Update() {
        thisScene = SceneManager.GetActiveScene().name;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            //Start Exit Level
            col.GetComponent<scrEntity>().GetAudioManager().PlayAudio(0, "Clear");
            canvas.GetComponent<LevelClearScript>().isActive = true;
        }
    }

    public void Load() {
        if (Stage_Data.GetCurrentStageIndex() < Stage_Data.GetNumberOfStages()-1) {
            Stage_Data.SetCurrentStageIndex(Stage_Data.GetCurrentStageIndex() + 1);
        }
        StartCoroutine(LoadLevel(sceneToLoad));
    }

    public void Replay() {
        StartCoroutine(LoadLevel(thisScene));
	}

    public void LoadMenu() {
        StartCoroutine(LoadLevel("Initialise"));
	}

    IEnumerator LoadLevel(string st)
    {
        anim.ResetTrigger("FADE");
        anim.SetTrigger("FADE");

        //Real time used as time is paused in the end screen menu
        yield return new WaitForSecondsRealtime(transitionTime);
                    
        Stage_Loader.LoadSceneSafe(st);
    }
}
