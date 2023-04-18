using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip TestSong;
    [SerializeField]
    AudioSource Player;
    // Start is called before the first frame update
    void Start()
    {
        Player.clip = TestSong;
        Player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
