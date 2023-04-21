using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event EventHandler<BulletArgs> OnBulletCreation;
    public static event EventHandler<BulletArgs> OnBulletDestruction;
    public static event EventHandler HitPlayer;
    public class BulletArgs: EventArgs {
        public GameObject Bullet;
    }
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        OnBulletCreation(this,new BulletArgs {Bullet = gameObject });
        GameManager.ClearBullets += GameManager_ClearBullets;
    }

    private void GameManager_ClearBullets(object sender, EventArgs e)
    {
        DestroyBullet();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (transform.up * speed) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.gameObject.name);
        if (collision.transform.tag == "Player") {
            HitPlayer(this,EventArgs.Empty);
        }
        if (collision.transform.parent.tag == "Enemy") {
            collision.transform.parent.GetComponent<Enemy>().Hit();
        }
        DestroyBullet();
    }
    public void DestroyBullet()
    {
        GameManager.ClearBullets -= GameManager_ClearBullets;
        OnBulletDestruction(this, new BulletArgs { Bullet = gameObject });
        Destroy(gameObject);
    }


}
