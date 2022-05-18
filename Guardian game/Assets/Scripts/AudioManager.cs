using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayEnemyDeathSound()
    {
        audio.PlayOneShot(audio.clip);
    }
}
