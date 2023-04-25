using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
public class PlayerController : MonoBehaviour
{
    [SerializeField] private UnityEvent PlayerFire;
    [SerializeField]
    float Speed;
    [SerializeField]
    GameObject BulletSpawnPoint;
    [SerializeField]
    GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            transform.position = transform.position + (transform.up*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.position = transform.position + (-transform.right * Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.position = transform.position + (-transform.up * Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position = transform.position + (transform.right * Time.deltaTime*Speed);
        }


        if (Input.GetMouseButtonDown(0)) {
            Instantiate(Bullet,BulletSpawnPoint.transform.position,BulletSpawnPoint.transform.rotation);
            PlayerFire.Invoke();
        }
        if (Input.GetMouseButtonDown(1)) {
            Instantiate(Bullet,BulletSpawnPoint.transform.position,BulletSpawnPoint.transform.rotation);
            GameObject temp = Instantiate(Bullet,BulletSpawnPoint.transform.position,BulletSpawnPoint.transform.rotation);
            GameObject temp2 = Instantiate(Bullet,BulletSpawnPoint.transform.position,BulletSpawnPoint.transform.rotation);
            temp.transform.Rotate(new Vector3(0,0,-15));
            temp2.transform.Rotate(new Vector3(0,0,15));
        }
    }
}
