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
    public AudioClip PlayerFireSFX;
    [SerializeField]
    public AudioClip PlayerHitSFX;
    [SerializeField]
    public AudioClip TankFireSFX;
    [SerializeField]
    public AudioClip TurretFireSFX;
    [SerializeField]
    public AudioClip EnemyHitSFX;
    [SerializeField]
    public AudioClip CreeperDeathSFX;
    [SerializeField]
    public AudioClip TurretDeathSFX;
    [SerializeField]
    public AudioClip TankDeathSFX;

    private void Start()
    {
        AudioSources.Add(gameObject.AddComponent<AudioSource>());
        Tank.TankFire += Tank_TankFire;
        Turret.TurretFire += Turret_TurretFire;
    }

    private void Turret_TurretFire(object sender, EventArgs e)
    {
        TurretFire();
    }

    private void Tank_TankFire(object sender, EventArgs e)
    {
        TankFire();
    }

    public void PlayerFire()
    {
        FindASource(PlayerFireSFX);
    }

    public void PlayerHit()
    {
        FindASource(PlayerHitSFX);
        StartCoroutine(PlayerHitMute());
    }

    public void TurretFire()
    {
        FindASource(TurretFireSFX);
        FindASource(TurretFireSFX);
    }

    public void TankFire()
    {
        FindASource(TankFireSFX);
    
    }

    IEnumerator PlayerHitMute()
    {
        
        MusicPlayer musicPlayer = GetComponent<MusicPlayer>();
        musicPlayer.Player.volume = .25f;

        foreach (AudioSource source in AudioSources)
        {
            if (source.clip != PlayerHitSFX)
            {
                source.mute = true;
            }
        }
        yield return new WaitForSeconds(PlayerHitSFX.length);
        
        foreach (AudioSource source in AudioSources)
        {
   
            source.mute = false;      
        }
        musicPlayer.Player.volume = 1f;
        yield return null;
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
