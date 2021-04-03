using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearScript : MonoBehaviour {
	public bool isActive = false;

	[SerializeField]
	GameObject[] LevelClearObjects = new GameObject[5];

	// Start is called before the first frame update
	void Start() {
		Time.timeScale = 1.0f;
		foreach (GameObject item in LevelClearObjects) {
			item.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update() {
		if (isActive == true) {
			Time.timeScale = 0.0f;
			foreach (GameObject item in LevelClearObjects) {
				item.SetActive(true);
			}
		}
	}

}
