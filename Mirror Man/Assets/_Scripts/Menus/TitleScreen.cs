using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    Image Play;
    [SerializeField]
    Image Options;
    [SerializeField]
    Image About;
    [SerializeField]
    Image Exit;

    [SerializeField]
    Image Cursor;
    [SerializeField]
    Sprite[] sCursor = new Sprite[2];

    [SerializeField]
    Sprite[] sPlay = new Sprite[2];
    [SerializeField]
    Sprite[] sOptions = new Sprite[2];
    [SerializeField]
    Sprite[] sAbout = new Sprite[2];
    [SerializeField]
    Sprite[] sExit = new Sprite[2];

    enum SELECTIONS
    { 
        PLAY,
        OPTIONS,
        ABOUT,
        EXIT
    };
    private SELECTIONS selection;
    private int MenuPos;

    private void Update()
    {
        switch(selection)
        {
            case SELECTIONS.PLAY:
                {
                    //Cursor Set
                    Cursor.gameObject.transform.position = new Vector3(Cursor.gameObject.transform.position.x, Play.gameObject.transform.position.y, Cursor.gameObject.transform.position.z);
                    Cursor.sprite = sCursor[0];
                    //Sprite Set
                    Play.sprite     = sPlay[1];
                    Options.sprite  = sOptions[0];
                    About.sprite    = sAbout[0];
                    Exit.sprite     = sExit[0];
                    if (Selection())
                    {
                        StartPlay();
                    }
                    break;
                }
            case SELECTIONS.OPTIONS:
                {
                    //Cursor Set
                    Cursor.gameObject.transform.position = new Vector3(Cursor.gameObject.transform.position.x, Options.gameObject.transform.position.y, Cursor.gameObject.transform.position.z);
                    Cursor.sprite = sCursor[1];
                    //Sprite Set
                    Play.sprite = sPlay[0];
                    Options.sprite = sOptions[1];
                    About.sprite = sAbout[0];
                    Exit.sprite = sExit[0];
                    break;
                }
            case SELECTIONS.ABOUT:
                {
                    //Cursor Set
                    Cursor.gameObject.transform.position = new Vector3(Cursor.gameObject.transform.position.x, About.gameObject.transform.position.y, Cursor.gameObject.transform.position.z);
                    Cursor.sprite = sCursor[1];
                    //Sprite Set
                    Play.sprite = sPlay[0];
                    Options.sprite = sOptions[0];
                    About.sprite = sAbout[1];
                    Exit.sprite = sExit[0];
                    if (Selection())
                    {
                        StartAbout();
                    }
                    break;
                }
            case SELECTIONS.EXIT:
                {
                    //Cursor Set
                    Cursor.gameObject.transform.position = new Vector3(Cursor.gameObject.transform.position.x, Exit.gameObject.transform.position.y, Cursor.gameObject.transform.position.z);
                    Cursor.sprite = sCursor[1];
                    //Sprite Set
                    Play.sprite = sPlay[0];
                    Options.sprite = sOptions[0];
                    About.sprite = sAbout[0];
                    Exit.sprite = sExit[1];
                    if (Selection())
                    {
                        StartQuit();
                    }
                    break;
                }
            default:
                //Sprite Set
                Play.sprite = sPlay[0];
                Options.sprite = sOptions[0];
                About.sprite = sAbout[0];
                Exit.sprite = sExit[0];
                break;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            MenuPos--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            MenuPos++;
        }

        if (MenuPos < 0)
        {
            MenuPos = 3;
        }
        else if (MenuPos > 3)
        {
            MenuPos = 0;
        }
        selection = (SELECTIONS)MenuPos;
    }

    private bool Selection()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

    void StartPlay()
    {
        SaveSystem.LoadSaveData();
        Stage_Loader.LoadSceneSafe("StageSelect");
    }

    void StartAbout()
    {
        Stage_Loader.LoadSceneSafe("About");
    }

    void StartQuit()
    {
        Application.Quit();
    }

}
