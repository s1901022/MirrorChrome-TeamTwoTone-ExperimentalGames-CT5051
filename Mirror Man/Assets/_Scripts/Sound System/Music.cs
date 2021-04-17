using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SetMusic(Sound a_sound) { 
        GetComponent<AudioSource>().clip = a_sound.audioFile;
        GetComponent<AudioSource>().volume = a_sound.volume;
        GetComponent<AudioSource>().pitch = a_sound.pitch;
    }

    public void PlayMusic()
    {
        if (GetComponent<AudioSource>().isPlaying) { return; }
        GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }
}
