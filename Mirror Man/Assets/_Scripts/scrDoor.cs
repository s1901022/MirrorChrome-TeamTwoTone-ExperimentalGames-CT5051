using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDoor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D rb;

    [SerializeField]
    private GameObject trigger;
    private scrButton triggerScript;

    [SerializeField]
    Sprite[] sprites;

    void Start()
    {
        //Get Trigger
        triggerScript = trigger.GetComponent<scrButton>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerScript.flag == true)
        {
            rb.enabled = false;
            spriteRenderer.sprite = sprites[1];          
        }
        else
        {
            rb.enabled = true;
            spriteRenderer.sprite = sprites[0];
        }
    }
}
