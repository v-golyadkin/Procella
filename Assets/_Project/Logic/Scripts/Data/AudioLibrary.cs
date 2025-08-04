using System;
using UnityEngine;


[Serializable]
public struct AudioData
{
    public string name;
    public AudioClip clip;
    public float volume;
}

public class AudioLibrary
{
    private AudioData[] _audios;

    public AudioLibrary(AudioData[] audioData)
    {
        _audios = audioData;
    }

    public AudioClip GetClipFromName(string name)
    {
        foreach (var audio in _audios)
        {
            if(audio.name == name)
            {
                return audio.clip;
            }
        }

        return null;
    }

    public float GetVolume(string name)
    {
        foreach (var audio in _audios)
        {
            if(audio.name == name)
            {
                return audio.volume;
            }
        }

        return 0;
    }
}
