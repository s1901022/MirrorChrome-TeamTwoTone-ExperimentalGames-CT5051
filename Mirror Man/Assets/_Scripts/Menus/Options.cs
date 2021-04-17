using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    //Get Sprites
    [SerializeField]
    private Image Cursor;
    [SerializeField]
    private Sprite[] cursorSprites;

    //Create Enum of Options
    enum OPTIONS {
        CLEARDATA,
        BACK
    };
    private OPTIONS selection;
    private int menuPos;

    private void Start() {
        //Set the Default Option
        menuPos = 1;
        selection = OPTIONS.BACK;
    }

    private void Update() {
        //Options selections
        switch(selection)
        {
            case OPTIONS.CLEARDATA:
                {
                    Cursor.sprite = cursorSprites[0];
                    Cursor.rectTransform.anchoredPosition = new Vector2(Cursor.rectTransform.anchoredPosition.x, 40f);
                    if (Selection()) {
                        //Clear save data
                        ClearData();
                    }
                    break;
                }
            case OPTIONS.BACK:
                {
                    Cursor.sprite = cursorSprites[1];
                    Cursor.rectTransform.anchoredPosition = new Vector2(Cursor.rectTransform.anchoredPosition.x, -200f);
                    if (Selection()) {
                        //return to title screen
                        Back();
                    }
                    break;
                }
            default:
                break;
        }

        //Move Cursor
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            menuPos--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            menuPos++;
        }
        //Loop Cursor
        if (menuPos < 0) {
            menuPos = 1;
        }
        else if (menuPos > 1) {
            menuPos = 0;
        }
        selection = (OPTIONS)menuPos;


        //Exit Game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    private void ClearData() {
        SaveSystem.DeleteSaveData();
        menuPos = 1;
    }

    private void Back() {
        Stage_Loader.LoadSceneSafe("TitleScreen");
    }

    private bool Selection() {
        //Choice Made
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            return true;
        }
        return false;
    }
}
