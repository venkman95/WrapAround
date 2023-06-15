using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTripleShot : MonoBehaviour
{
    [SerializeField]
    GameObject TripleShot;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = Camera.main.GetComponent<GameManager>();
        PlayerController player = manager.Player.GetComponent<PlayerController>();
        player.Bullet = TripleShot;
        Destroy(gameObject);
    }
}
