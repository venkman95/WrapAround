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
        Bullet.HitEnemy += Bullet_HitEnemy;
        Bullet.HitPlayer += Bullet_HitPlayer;
        Enemy.OnEnemyDestruction += Enemy_OnEnemyDestruction;
    }

    private void Enemy_OnEnemyDestruction(object sender, Enemy.EnemyArgs e)
    {
        switch (e.enemy.gameObject.GetComponent<Enemy>().Type)
        {
            case Enemy.EnemyType.Creeper:
                CreeperDeath();
                break;
            case Enemy.EnemyType.Turret:
                TurretDeath();
                break;
            case Enemy.EnemyType.Tank:
                TankDeath();
                break;
            default:
                break;
        }
    }

    private void Bullet_HitPlayer(object sender, EventArgs e)
    {
        PlayerHit();
    }

    private void Bullet_HitEnemy(object sender, EventArgs e)
    {
        EnemyHit();
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
        StartCoroutine(ImportantSoundCue(PlayerHitSFX));
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
    public void EnemyHit()
    {
        FindASource(EnemyHitSFX);
    }

    public void CreeperDeath()
    {
        FindASource(CreeperDeathSFX);
        StartCoroutine(ImportantSoundCue(CreeperDeathSFX));
    }
    public void TurretDeath()
    {
        FindASource(TurretDeathSFX);
        StartCoroutine(ImportantSoundCue(TurretDeathSFX));
    }
    public void TankDeath()
    {
        FindASource(TankDeathSFX);
        StartCoroutine(ImportantSoundCue(TankDeathSFX));
    }
    IEnumerator ImportantSoundCue(AudioClip clip)
    {
        
        MusicPlayer musicPlayer = GetComponent<MusicPlayer>();
        musicPlayer.Player.volume = .15f;

        foreach (AudioSource source in AudioSources)
        {
            if (source.clip != clip)
            {
                source.volume = .5f;
            }
        }
        yield return new WaitForSeconds(clip.length);
        
        foreach (AudioSource source in AudioSources)
        {
   
            source.volume = 1f;      
        }
        musicPlayer.Player.volume = .85f;
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
