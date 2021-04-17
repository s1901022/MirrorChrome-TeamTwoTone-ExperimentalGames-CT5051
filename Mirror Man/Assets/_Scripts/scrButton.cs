using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrButton : MonoBehaviour {
    //Buttons that activate other objects
    public bool flag;
    public bool flagDefault;
    public bool alwaysSet;

    [SerializeField]
    Sprite[] sprites;

    void OnTriggerStay2D(Collider2D col) {
        //Handle Death and respawning
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Entity") {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            if (flag == flagDefault) {
                flag = !flag;
            }           
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        if (alwaysSet == false) {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            flag = flagDefault;
        }
    }
}
