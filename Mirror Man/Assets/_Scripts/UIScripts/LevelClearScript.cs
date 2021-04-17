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
			if (Stage_Data.GetCurrentStageIndex() < Stage_Data.GetNumberOfStages() -1)
			{
				if (Stage_Data.GetProgress() < Stage_Data.GetStageNumber(Stage_Data.GetCurrentStageIndex()))
				{
				
					Stage_Data.SetProgress(Stage_Data.GetProgress() + 1);
				}
            }
			// Stop time 
			Time.timeScale = 0.0f;
			// loop through the gameobject array
			foreach (GameObject item in LevelClearObjects) {
				// set each objects activity to true
				item.SetActive(true);

				// check the flip data against how many flips the player has done
				if (stageData.targetnumberOfFlips >= flipCounter) {
					// if the number of flips is equal to or less then the recomended
					// turn the star to completed
					LevelClearObjects[0].GetComponent<RawImage>().texture = completedStar.texture;
					// set the stage data flip number to the counter
					stageData.numberOfFlips = flipCounter;
				} else if (stageData.numberOfFlips == 0) {
					// if this is the first time playing the level
					// set the number of flips anyway
					stageData.numberOfFlips = flipCounter;
				}

				// check the timer data against how long the player took
				if (stageData.targetTime >= timer) {
					// if the time is equal or less thatn the recomended
					// turn the star to completed
					LevelClearObjects[1].GetComponent<RawImage>().texture = completedStar.texture;
					// set the stage data time taken to the timer
					stageData.bestTime = timer;
				} else if (stageData.bestTime == 0) {
					// if this is the first time playing the level
					// set the timer anyway
					stageData.bestTime = timer;
				}

				// check the jump data against how many jumps the player has done
				if (stageData.targetJumps >= jumpCounter) {
					// if the number of jumps is equal to or less then the recomended
					// turn the star to completed
					LevelClearObjects[2].GetComponent<RawImage>().texture = completedStar.texture;
					stageData.numberOfJumps = jumpCounter;
				} else if (stageData.numberOfJumps == 0) {
					// if this is the first time playing the level
					// set the number of jumps anyway
					stageData.numberOfJumps = jumpCounter;
				}
				// save the stage data
				SaveSystem.SaveGameData();
			}
		}
	}
}
