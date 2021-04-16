using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    [SerializeField]
    GameObject levelClearGO;

    [SerializeField]
    Button NextLevel;
    [SerializeField]
    Button Replay;
    [SerializeField]
    Button MainMenu;

    enum SELECTIONS {
        NEXT_LEVEL,
        REPLAY,
        MAIN_MENU
    };

    private SELECTIONS selection;
    private int MenuPos;

	private void Start() {
        levelClearGO = GameObject.FindGameObjectWithTag("LEG");
	}

	// Update is called once per frame
	void Update()
    {
		switch (selection) {
			case SELECTIONS.NEXT_LEVEL: {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, NextLevel.gameObject.transform.position.y, gameObject.transform.position.z);

                    if (Selection()) {
                        // Go to next level
                        levelClearGO.GetComponent<scrChangeScene>().Load();
					}
				break;
			}
			case SELECTIONS.REPLAY: {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, Replay.gameObject.transform.position.y, gameObject.transform.position.z);

                    if (Selection()) {
                        // replay level
                        levelClearGO.GetComponent<scrChangeScene>().Replay();
                    }
                    break;
			}
			case SELECTIONS.MAIN_MENU: {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, MainMenu.gameObject.transform.position.y, gameObject.transform.position.z);

                    if (Selection()) {
                        // Main Menu
                        levelClearGO.GetComponent<scrChangeScene>().LoadMenu();
                    }
                    break;
			}
			default: {

				break;
			}
		}

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            MenuPos--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            MenuPos++;
        }

        if (MenuPos < 0) {
            MenuPos = 3;
        } else if (MenuPos > 3) {
            MenuPos = 0;
        }
        selection = (SELECTIONS)MenuPos;
    }

    private bool Selection() {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            return true;
        }
        return false;
    }


}
