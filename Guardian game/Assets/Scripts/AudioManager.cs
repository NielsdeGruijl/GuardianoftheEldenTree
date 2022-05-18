using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;

    [SerializeField] List<Sound> sounds = new List<Sound>();
    AudioSource source;

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
        foreach(Sound sound in sounds)
        {
            if (sound.name == clipName)
                source.PlayOneShot(sound.clip);
        }
    }



    AudioSource enemyDeathClip;

    void Start()
    {
        enemyDeathClip = GetComponent<AudioSource>();
    }

    public void PlayEnemyDeathSound()
    {
        enemyDeathClip.PlayOneShot(enemyDeathClip.clip);
    }
}
