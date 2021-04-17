using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour {
    public float maxSize;
    public float shrinkFactor;
    

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D col) {
            StartCoroutine(Scale());
            transform.localScale = new Vector3(1f, 1f, 1f);
    }

   

    IEnumerator Scale() {
        float timer = 0;

        while (true) { // this could also be a condition indicating "alive or dead"
            // we scale all axis, so they will have the same value, 
            // so we can work with a float instead of comparing vectors
            while (maxSize < transform.localScale.x) {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(1, 1, 0) * Time.deltaTime;
                yield return null;

                if(transform.localScale.x > .1f) {
                    //transform.localScale = new Vector3(.1f, .1f, 0);
                    timer = 0;
                }               
            }
            // reset the timer

        }
    }
}
