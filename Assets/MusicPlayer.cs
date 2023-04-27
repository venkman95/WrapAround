using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip TestSong;
    [SerializeField]
    public AudioSource Player;
    [SerializeField]
    float Volume = 1;
    // Start is called before the first frame update
    void Start()
    {
        Player.clip = TestSong;
        Player.Play();
        Player.volume = Volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
