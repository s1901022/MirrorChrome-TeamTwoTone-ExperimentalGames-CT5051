using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class scrChangeScene : MonoBehaviour
{
    public string sceneToLoad;
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
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        anim.ResetTrigger("FADE");
        anim.SetTrigger("FADE");

        //Real time used as time is paused in the end screen menu
        yield return new WaitForSecondsRealtime(transitionTime);
                    
        Stage_Loader.LoadSceneSafe(sceneToLoad);
    }
}
