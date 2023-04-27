using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tank : MonoBehaviour
{
    [SerializeField]
    float FireDelay;
    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    GameObject Target;
    [SerializeField]
    float Speed;
    [SerializeField]
    float Rotation;
    [SerializeField]
    float GunRotation;
    [SerializeField]
    GameObject FiringPoint;
    [SerializeField]
    GameObject Gun;
    bool CanFire = true;

    [SerializeField]
    public static event EventHandler TankFire;
    // Start is called before the first frame update
    void Start()
    {
        Target = FindObjectOfType<Camera>().GetComponent<GameManager>().Player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
        Vector2 targetPos = Target.transform.position;
        Vector2 lookDir = targetPos - new Vector2(transform.position.x,transform.position.y);
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.AngleAxis(-angle,Vector3.forward),Rotation * Time.deltaTime);

        Vector2 lookDirGun = targetPos - new Vector2(Gun.transform.position.x,Gun.transform.position.y);
        float gunAngle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        Gun.transform.rotation = Quaternion.RotateTowards(Gun.transform.rotation,Quaternion.AngleAxis(angle,Vector3.forward),GunRotation * Time.deltaTime);


        RaycastHit2D hit = Physics2D.Raycast(FiringPoint.transform.position,FiringPoint.transform.up);
        Debug.DrawRay(FiringPoint.transform.position,FiringPoint.transform.up,Color.green);
        if (hit) {
            if (hit.transform.gameObject.tag == "Player" & CanFire) {
                Instantiate(Bullet,FiringPoint.transform.position,FiringPoint.transform.rotation);
                StartCoroutine(FiringDelay());
                TankFire?.Invoke(this,EventArgs.Empty);
            }
        }
    }
    IEnumerator FiringDelay() {
        CanFire = false;
        yield return new WaitForSeconds(FireDelay);
        //Debug.Log("Here");
        CanFire = true;
        yield return null;
    }
}
