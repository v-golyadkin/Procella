using System.Collections;
using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioData[] _sfxData;

    private AudioLibrary _sfxLibrary;

    private void Start()
    {
        _sfxLibrary = new(_sfxData);
    }

    public void PlaySFX(string name)
    {   
        sfxSource.PlayOneShot(_sfxLibrary.GetClipFromName(name), _sfxLibrary.GetVolume(name));
    }
}
