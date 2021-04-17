using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    //Play clips in game from sound classes
    private AudioSource audioSource;
    private string lastPlayedSound;

    //Allow Modification of accessable Audio groups
    [SerializeField] 
    private AudioGroup[] audioGroups;

    private void Start() {
        //Get the Audio Component
        AudioSource audioSource = GetComponent<AudioSource>();
    }

    public bool PlayAudio(int a_audioGroup, string a_soundName) {
        //Play Sound from Audio group
        Sound sound = audioGroups[a_audioGroup].GetSound(a_soundName);
        if (sound.audioFile != null) {
            //For some reason only works if I get components again
            Debug.Log("Sound: " + sound.soundName);
            Debug.Log("SoundClip: " + sound.audioFile);
            GetComponent<AudioSource>().clip = sound.audioFile;
            GetComponent<AudioSource>().volume = sound.volume;
            GetComponent<AudioSource>().pitch = sound.pitch;
            GetComponent<AudioSource>().Play();
            //Get the last played sound
            lastPlayedSound = sound.soundName;
            return true;
        }
        return false;
    }

    public bool AudioIsPlaying() {
        //Check if Audio is being played
        if (GetComponent<AudioSource>().isPlaying == true) {
            return true;
        }
        return false;
    }

    public void StopAudio(string a_soundName) {
        //Stop Audio
        if (lastPlayedSound == a_soundName) {
            GetComponent<AudioSource>().Stop();
        }
    }
}
