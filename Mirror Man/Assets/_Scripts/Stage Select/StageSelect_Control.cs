using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconData
{
    public int stageNumber;
    public string stageName;
    public Sprite stageSelectIcon;

    public float targetTime;
    public int targetnumberOfFlips;

    public int numberOfFlips;
    public bool collectableGot;
    public float bestTime;

   public void SetData(int a_stageNumber, string a_stageName, Sprite a_sprite, float a_targetTime, float a_bestTime, int a_targetFlips, int a_flipCount, bool a_collectableGot)
    {
        stageNumber = a_stageNumber;
        stageName = a_stageName;
        stageSelectIcon = a_sprite;
        targetTime = a_targetTime;
        bestTime = a_bestTime;
        targetnumberOfFlips = a_targetFlips;
        numberOfFlips = a_flipCount;
        collectableGot = a_collectableGot;
    }
}

public class StageSelect_Control : MonoBehaviour
{
    //Level Data
    List<IconData> levels;
    [SerializeField]
    List<Image> levelIcons;
    [SerializeField]
    Text levelName;

    //Star drawing
    [SerializeField]
    Image StarFlips;
    [SerializeField]
    Image StarTime;
    [SerializeField]
    Image StarCollectable;

    [SerializeField]
    Sprite[] StarGraphics;

    //Scrolling options
    int currentIndex;
    int lastIndex;
    int nextIndex;

    private void Start()
    {
        //Load Levels into Stage Select
        levels = new List<IconData>();
        for (int i = 0; i < Stage_Data.GetNumberOfStages(); i++)
        {
            int j = i;
            levels.Add(new IconData());
            levels[i].SetData(Stage_Data.GetStageNumber(j), Stage_Data.GetStageName(j), Stage_Data.GetIconSprite(j), Stage_Data.GetTargetTime(j), Stage_Data.GetBestTime(j), Stage_Data.GetTargetFlips(j), Stage_Data.GetFlips(j), Stage_Data.GetCollectableBool(j));
            levelIcons[i].sprite = levels[i].stageSelectIcon;
            Debug.LogError("Success");
        }

        //Set initial Positions
        currentIndex = Stage_Data.GetProgress();
        lastIndex = currentIndex - 1;
        nextIndex = currentIndex + 1;

        levelIcons[currentIndex].rectTransform.anchoredPosition = new Vector2(0f, 0f);
        if (lastIndex >= 0)
        {
            levelIcons[lastIndex].rectTransform.anchoredPosition = new Vector2(-650f, 0f);
        }
        if (nextIndex < levelIcons.Count)
        {
            levelIcons[nextIndex].rectTransform.anchoredPosition = new Vector2(650f, 0f);
        }

        levelName.text = levels[currentIndex].stageName;
        UpdateIcon();
        UpdateStars();
    }

    private void Update()
    {
        UpdateSelection();
        Selection();
    }

    private void UpdateSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentIndex += 1;
            levelName.text = levels[currentIndex].stageName;
            UpdateIcon();
            UpdateStars();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentIndex -= 1;
            levelName.text = levels[currentIndex].stageName;
            UpdateIcon();
            UpdateStars();
        }
    }

    private void UpdateIcon()
    {
        lastIndex = currentIndex - 1;
        nextIndex = currentIndex + 1;

        levelIcons[currentIndex].rectTransform.anchoredPosition = new Vector2(0f, 0f);
        if (lastIndex >= 0)
        {
            levelIcons[lastIndex].rectTransform.anchoredPosition = new Vector2(-650f, 0f);
        }
        if (nextIndex < levelIcons.Count)
        {
            levelIcons[nextIndex].rectTransform.anchoredPosition = new Vector2(650f, 0f);
        }
    }

    private void UpdateStars()
    {
        //Time
        if (levels[currentIndex].bestTime <= levels[currentIndex].targetTime && levels[currentIndex].bestTime >= 1.0f)
        {
            StarTime.sprite = StarGraphics[1];
        }
        else
        {
            StarTime.sprite = StarGraphics[0];
        }
        //Flips
        if (levels[currentIndex].numberOfFlips <= levels[currentIndex].targetnumberOfFlips)
        {
            StarFlips.sprite = StarGraphics[1];
        }
        else
        {
            StarFlips.sprite = StarGraphics[0];
        }
        //Collectable
        if (levels[currentIndex].collectableGot == true)
        {
            StarCollectable.sprite = StarGraphics[1];
        }
        else
        {
            StarCollectable.sprite = StarGraphics[0];
        }
    }

    private void Selection()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            Stage_Loader.LoadSceneSafe(Stage_Data.GetStageBuildName(currentIndex));
        }
    }
}
