using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
    //Plays Background music

    private void Awake() {
        //is Persistant
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SetMusic(Sound a_sound) { 
        //Set Current Music
        GetComponent<AudioSource>().clip = a_sound.audioFile;
        GetComponent<AudioSource>().volume = a_sound.volume;
        GetComponent<AudioSource>().pitch = a_sound.pitch;
    }

    public void PlayMusic() {
        //Play Music
        if (GetComponent<AudioSource>().isPlaying) { return; }
        GetComponent<AudioSource>().Play();
    }

    public void StopMusic() {
        //Stop Music
        GetComponent<AudioSource>().Stop();
    }
}
