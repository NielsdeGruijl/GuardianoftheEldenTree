using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;

    [SerializeField] List<Sound> sounds = new List<Sound>();
    AudioSource source;

    public float SFXVolume;
    public float MusicVolume;

    private void Awake()
    {
        if (manager != null && manager != this)
            Destroy(gameObject);
        else
            manager = this;

        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
    }

    public void PlaySFX(string clipName)
    {
        source.loop = false;

        foreach(Sound sound in sounds)
        {
            if (sound.name == clipName)
            {
                AdjustSoundValues(source, sound);
                source.PlayOneShot(sound.clip);
            }
                
        }
    }

    public void PlayOnLoop()
    {
        source.loop = true;

        

    }

    private void AdjustSoundValues(AudioSource source, Sound sound)
    {
        source.volume = sound.volume;
    }
}
