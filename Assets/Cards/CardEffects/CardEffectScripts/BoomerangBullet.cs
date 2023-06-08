using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : MonoBehaviour
{
    Dictionary<GameObject, Vector2> firingPoint = new Dictionary<GameObject, Vector2>();
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
            if (firingPoint.ContainsKey(bullet))
            {
                
                Vector2 targetPos = firingPoint[bullet];
                Vector2 lookDir = targetPos - new Vector2(bullet.transform.position.x, bullet.transform.position.y);
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward),(bullet.GetComponent<Bullet>().currentspeed * Vector3.Distance(bullet.transform.position,targetPos) * 2f) * Time.deltaTime);
            }
            else
            {
                firingPoint.Add(bullet, bullet.transform.position);
            }
        }
        List<GameObject> ToBeCleared = new List<GameObject>();
        foreach (GameObject bullet in firingPoint.Keys)
        {
            if (bullet == null)
            {
                ToBeCleared.Add(bullet);
            }
        }
        foreach (GameObject bullet in ToBeCleared)
        {
            firingPoint.Remove(bullet);
        }
        ToBeCleared.Clear();
    }
}
