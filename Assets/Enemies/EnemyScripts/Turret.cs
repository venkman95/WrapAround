using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turret : MonoBehaviour
{
    [SerializeField]
    GameObject Target;
    [SerializeField]
    float Speed;
    [SerializeField]
    GameObject RaycastStartingPoint;
    [SerializeField]
    GameObject FiringPoint;
    [SerializeField]
    GameObject FiringPoint2;
    [SerializeField]
    GameObject Bullet;
    bool CanFire = true;
    [SerializeField]
    float FireDelay;
    [SerializeField]
    public static event EventHandler TurretFire;
    // Start is called before the first frame update
    void Start()
    {
        Target = FindObjectOfType<Camera>().GetComponent<GameManager>().Player;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 targetPos = Target.transform.position;
        Vector2 lookDir = targetPos - new Vector2(transform.position.x,transform.position.y);
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.AngleAxis(angle,Vector3.forward),Speed * Time.deltaTime);



       RaycastHit2D hit = Physics2D.Raycast(RaycastStartingPoint.transform.position,transform.up);
        Debug.DrawRay(transform.position,transform.up,Color.green);
        if (hit) {
            if (hit.transform.gameObject.tag == "Player" & CanFire) {
                Instantiate(Bullet,FiringPoint.transform.position,FiringPoint.transform.rotation);
                Instantiate(Bullet,FiringPoint2.transform.position,FiringPoint.transform.rotation);
                StartCoroutine(FiringDelay());
                TurretFire?.Invoke(this,EventArgs.Empty);
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
