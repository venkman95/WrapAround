using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightGreenLight : MonoBehaviour
{
    float Chance = 0f;
    GameManager manager;
    List<GameObject> bulletList;
    // Start is called before the first frame update
    void Start()
    {
        manager = Camera.main.GetComponent<GameManager>();
        bulletList = manager.BulletList;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject bullet in bulletList)
        {
            if (Random.Range(1f, 100f) < Chance)
            {
                Chance = 0f;
                Bullet temp = bullet.GetComponent<Bullet>();
                if (temp.currentspeed == 0f)
                {
                    temp.currentspeed = temp.maximumspeed;
                }
                else
                {
                    temp.currentspeed = 0f;
                }
            }
            else
            {
                Chance += Time.deltaTime;
            }
        }
    }
}
