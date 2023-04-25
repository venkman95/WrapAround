using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
[Serializable]
public class SFXManager : MonoBehaviour
{
    List<AudioSource> AudioSources = new List<AudioSource>();
    
    [SerializeField]
    AudioClip PlayerFireSFX;

    private void Start()
    {
        AudioSources.Add(gameObject.AddComponent<AudioSource>());
    }

    public void PlayerFire()
    {
        FindASource(PlayerFireSFX);
    }




    private void FindASource(AudioClip clip)
    {
        bool FoundUnusedAudioSource = false;
        foreach (AudioSource source in AudioSources)
        {
            if (!source.isPlaying)
            {
                PlaySource(source, clip);
                FoundUnusedAudioSource = true;
                break;
            }
        }
        if (FoundUnusedAudioSource == false)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            PlaySource(temp, clip);
            AudioSources.Add(temp);
        }
    }

    private void PlaySource(AudioSource source,AudioClip audioClip)
    {
        source.loop = false;
        source.clip = audioClip;
        source.Play();
    }
}
