using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundTheGameManager : MonoBehaviour
{
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = Camera.main.GetComponent<GameManager>();
        Debug.Log("Found the Manager");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(manager.BulletList.Count);
    }
}
