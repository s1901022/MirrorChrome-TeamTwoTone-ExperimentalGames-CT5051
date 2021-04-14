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

    //Arrows
    [SerializeField]
    Image leftArrow;
    [SerializeField]
    Image rightArrow;

    //Star drawing
    [SerializeField]
    Image StarFlips;
    [SerializeField]
    Image StarTime;
    [SerializeField]
    Image StarCollectable;

    //Sprites for sprite swaps
    [SerializeField]
    Sprite[] StarGraphics;
    [SerializeField]
    Sprite sprPadlock;
    [SerializeField]
    Sprite sprRArrow;

    //Scrolling options
    [SerializeField]
    float slideSpeed;
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
            levelIcons[i].rectTransform.anchoredPosition = new Vector2(i * 650f, levelIcons[i].rectTransform.anchoredPosition.y);
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
        UpdateArrows();
        UpdateStars();
    }

    private void Update()
    {
        UpdateIcon();
        UpdateSelection();
        Selection();
    }

    private void UpdateSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            //check if next stage is unlocked
            if (currentIndex < Stage_Data.GetProgress())
            {
                currentIndex += 1;
                levelName.text = levels[currentIndex].stageName;
                UpdateArrows();
                UpdateStars();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            //make sure not at start of list
            if (currentIndex != 0)
            {
                currentIndex -= 1;
                levelName.text = levels[currentIndex].stageName;
                UpdateArrows();
                UpdateStars();
            }
        }
    }

    private void UpdateIcon()
    {
        lastIndex = currentIndex - 1;
        nextIndex = currentIndex + 1;

        if (lastIndex >= 0)
        {
            if (Mathf.Round(levelIcons[lastIndex].rectTransform.anchoredPosition.x) > -650f)
            {
                if (levelIcons[lastIndex].rectTransform.anchoredPosition.x < -650f)
                {
                    levelIcons[lastIndex].rectTransform.anchoredPosition += new Vector2(Mathf.Round(slideSpeed * Time.deltaTime), 0f);
                }
                else if (levelIcons[lastIndex].rectTransform.anchoredPosition.x > -650f)
                {
                    levelIcons[lastIndex].rectTransform.anchoredPosition -= new Vector2(Mathf.Round(slideSpeed * Time.deltaTime), 0f);
                }
            }
            else
            {
                levelIcons[lastIndex].rectTransform.anchoredPosition = new Vector2(-650f, levelIcons[lastIndex].rectTransform.anchoredPosition.y);
            }

            int j = 0;
            for (int i = lastIndex-1; i >= 0; i--)
            {
                j++;
                levelIcons[i].rectTransform.anchoredPosition = new Vector2(levelIcons[lastIndex].rectTransform.anchoredPosition.x - (j * 650), levelIcons[lastIndex].rectTransform.anchoredPosition.y);
            }
        }
        if (Mathf.Round(levelIcons[currentIndex].rectTransform.anchoredPosition.x) != 0f)
        {
            if (levelIcons[currentIndex].rectTransform.anchoredPosition.x < 0f)
            {
                levelIcons[currentIndex].rectTransform.anchoredPosition += new Vector2(Mathf.Round(slideSpeed * Time.deltaTime), 0f);
            }
            else if (levelIcons[currentIndex].rectTransform.anchoredPosition.x > 0f)
            {
                levelIcons[currentIndex].rectTransform.anchoredPosition -= new Vector2(Mathf.Round(slideSpeed * Time.deltaTime), 0f);
            }

            if (levelIcons[currentIndex].rectTransform.anchoredPosition.x <= 20f && levelIcons[currentIndex].rectTransform.anchoredPosition.x >= -20f)
            {
                levelIcons[currentIndex].rectTransform.anchoredPosition = new Vector2(0f, levelIcons[currentIndex].rectTransform.anchoredPosition.y);
            }
        }
        else
        {
            levelIcons[currentIndex].rectTransform.anchoredPosition = new Vector2(0f, levelIcons[currentIndex].rectTransform.anchoredPosition.y);
        }

        if (nextIndex < levels.Count)
        {
            if (Mathf.Round(levelIcons[nextIndex].rectTransform.anchoredPosition.x) < 650f)
            {
                if (levelIcons[nextIndex].rectTransform.anchoredPosition.x < 650f)
                {
                    levelIcons[nextIndex].rectTransform.anchoredPosition += new Vector2(Mathf.Round(slideSpeed * Time.deltaTime), 0f);
                }
                else if (levelIcons[nextIndex].rectTransform.anchoredPosition.x > 650f)
                {
                    levelIcons[nextIndex].rectTransform.anchoredPosition -= new Vector2(Mathf.Round(slideSpeed * Time.deltaTime), 0f);
                }
            }
            else
            {
               levelIcons[nextIndex].rectTransform.anchoredPosition = new Vector2(650f, levelIcons[nextIndex].rectTransform.anchoredPosition.y);
            }

            int j = 0;
            for (int i = nextIndex + 1; i < levels.Count; i++)
            {
                j++;
                levelIcons[i].rectTransform.anchoredPosition = new Vector2(levelIcons[nextIndex].rectTransform.anchoredPosition.x + (j * 650), levelIcons[nextIndex].rectTransform.anchoredPosition.y);
            }
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

    void UpdateArrows()
    {
        //Left Arrow
        if (currentIndex != 0)
        {
            leftArrow.gameObject.SetActive(true);
        }
        else
        {
            leftArrow.gameObject.SetActive(false);
        }

        //Right Arrow
        if (currentIndex < levels.Count-1)
        {
            rightArrow.gameObject.SetActive(true);
            if (currentIndex == Stage_Data.GetProgress())
            {
                rightArrow.sprite = sprPadlock;
            }
            else
            {
                rightArrow.sprite = sprRArrow;
            }
        }
        else
        {
            rightArrow.gameObject.SetActive(false);
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
