using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkBullet : MonoBehaviour
{
    [SerializeField]
    float DegreeRange;
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
        foreach (GameObject bullet in bulletList) {
            bullet.transform.Rotate(transform.forward * Random.Range(-DegreeRange,DegreeRange) * Time.deltaTime*10);
        }
    }
}
