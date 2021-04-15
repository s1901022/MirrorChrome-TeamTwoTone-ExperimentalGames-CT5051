using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelClearScript : MonoBehaviour {
	public bool isActive = false;

	[SerializeField]
	GameObject[] LevelClearObjects = new GameObject[5];

	[SerializeField]
	int flips;

	int flipCounter = 0;
	[SerializeField]
	float time;

	float timer = 0.0f;
	[SerializeField]
	int jumps;

	int jumpCounter = 0;

	[SerializeField]
	Sprite completedStar;

	[SerializeField] StageData stageData;

	// Start is called before the first frame update
	void Start() {
		// Make time equal to 1 at the start
		Time.timeScale = 1.0f;
		// loop through the gameobject array
		foreach (GameObject item in LevelClearObjects) {
			// set each objects activity to false
			item.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update() {

		if (Input.GetKeyDown(KeyCode.UpArrow) && Time.timeScale == 1) {
			jumpCounter++;
		}
		if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1) {
			flipCounter++;
		}
		timer += Time.deltaTime;

		// if the end of the level is active
		if (isActive == true) {
			// Stop time 
			Time.timeScale = 0.0f;
			// loop through the gameobject array
			foreach (GameObject item in LevelClearObjects) {
				// set each objects activity to true
				item.SetActive(true);

				if (stageData.targetnumberOfFlips >= flipCounter) {
					LevelClearObjects[0].GetComponent<RawImage>().texture = completedStar.texture;
					stageData.numberOfFlips = flipCounter;
				} else if (stageData.numberOfFlips == 0) {
					stageData.numberOfFlips = flipCounter;
				}

				if (stageData.targetTime >= timer) {
					LevelClearObjects[1].GetComponent<RawImage>().texture = completedStar.texture;
					stageData.bestTime = timer;
				} else if (stageData.bestTime == 0) {
					stageData.bestTime = timer;
				}

				if (stageData.targetJumps >= jumpCounter) {
					LevelClearObjects[2].GetComponent<RawImage>().texture = completedStar.texture;
					stageData.numberOfJumps = jumpCounter;
				} else if (stageData.numberOfJumps == 0) {
					stageData.numberOfJumps = jumpCounter;
				}

				SaveSystem.SaveGameData();
			}
		}
	}
}
