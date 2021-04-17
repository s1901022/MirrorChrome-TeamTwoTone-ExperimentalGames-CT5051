using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoToneLogo : MonoBehaviour
{
    [SerializeField]
    Image logo;
    [SerializeField]
    Text presents;
    [SerializeField]
    Sound menuMusic;

    private float timer;
    // Start is called before the first frame update
    private void Start()
    {
        timer = 5f;
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, 0f);
       // presents.color = new Color(presents.color.r, presents.color.g, presents.color.b, 0f);

        //Music for menus is started here
        Music musicBox = GameObject.FindGameObjectWithTag("Music").GetComponent<Music>();
        musicBox.SetMusic(menuMusic);
        musicBox.PlayMusic();
    }

    private void Update()
    {
        Debug.LogError("Working");
        timer -= 0.01f;
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, logo.color.a + 0.01f );
        //presents.color = new Color(presents.color.r, presents.color.g, presents.color.a, + 0.2f * Time.deltaTime);
        if (timer <= 0f)
        {
            Stage_Loader.LoadSceneSafe("TitleScreen");
        }
    }

}
