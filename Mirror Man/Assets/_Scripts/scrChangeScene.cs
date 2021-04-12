using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class scrChangeScene : MonoBehaviour
{
    //public string sceneToLoad;
    private Animator anim;
    [SerializeField] private float transitionTime;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    Image wipe;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        wipe.color = new Color(wipe.color.r, wipe.color.g, wipe.color.b, 1.0f);
        //transition.SetBool("plsWork", false);
    }

    private void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canvas.GetComponent<LevelClearScript>().isActive = true;
        }
    }

    public void Load()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    

   

    IEnumerator LoadLevel(int levelIndex)
    {

        //transition.SetBool("plsWork", true);
        anim.ResetTrigger("FADE");
        anim.SetTrigger("FADE");
        //Real time used as time is paused in the end screen menue
        yield return new WaitForSecondsRealtime(transitionTime);
        //yield return new WaitForSeconds(transitionTime);
        
        print("HAH NOPE");
        
        
        //Stage_Loader.LoadSceneSafe(sceneToLoad);
        SceneManager.LoadScene(levelIndex);
    }
}
