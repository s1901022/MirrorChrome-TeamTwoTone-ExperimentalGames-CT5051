using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
[CreateAssetMenu]
public class AudioGroup : ScriptableObject
{
    public Sound[] sounds;

    public Sound GetSound(string a_soundName) 
    { 
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundName == a_soundName)
            {
                return sounds[i];
            }
        }
        Debug.LogError("Unable to find Sound in Audio Group");
        return null;
    }
}
