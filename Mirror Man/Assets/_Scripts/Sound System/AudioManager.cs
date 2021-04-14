using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private string lastPlayedSound;

    [SerializeField] 
    private AudioGroup[] audioGroups;

    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
    }
    public bool PlayAudio(int a_audioGroup, string a_soundName)
    {
        Sound sound = audioGroups[a_audioGroup].GetSound(a_soundName);
        if (sound.audioFile != null)
        {
            if (GetComponent<AudioSource>().isPlaying != true || sound.soundName != lastPlayedSound)
            {
                Debug.Log("Sound: " + sound.soundName);
                Debug.Log("SoundClip: " + sound.audioFile);
                GetComponent<AudioSource>().clip = sound.audioFile;
                GetComponent<AudioSource>().volume = sound.volume;
                GetComponent<AudioSource>().pitch = sound.pitch;
                GetComponent<AudioSource>().Play();
                lastPlayedSound = sound.soundName;
                return true;
            }
        }
        return false;
    }

    public void StopAudio()
    {
        GetComponent<AudioSource>().Stop();
    }
}
