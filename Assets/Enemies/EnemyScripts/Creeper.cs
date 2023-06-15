using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper : MonoBehaviour
{
    enum CreeperStates
    {
        Follow,Flee
    }
    [SerializeField]
    GameObject Clockwise;
    [SerializeField]
    GameObject Counter;
    [SerializeField]
    GameObject Target;
    [SerializeField]
    float speed;
    CreeperStates currentState = CreeperStates.Follow;

    List<Vector3> InsidePoints = new List<Vector3>();
    // Start is called before the first frame update
    void Start() {
        Target = Camera.main.GetComponent<GameManager>().Player;
        StartCoroutine(InsideRadius());
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case CreeperStates.Follow:
                Vector3 FollowVector = (Target.transform.position - transform.position);
                transform.position += FollowVector.normalized * speed * Time.deltaTime;
                Clockwise.transform.Rotate(new Vector3(0, 0, -100) * Time.deltaTime);
                Counter.transform.Rotate(new Vector3(0, 0, 99) * Time.deltaTime);
                break;
            case CreeperStates.Flee:
                Vector3 temp = new Vector3(0,0,0);
                foreach (Vector3 point in InsidePoints)
                {
                    temp += point;
                }
                Vector3 AveragedVector = temp / InsidePoints.Count;
                Vector3 FleeVector = (-AveragedVector + transform.position);
                transform.position += FleeVector.normalized * (speed*2.5f) * Time.deltaTime;
                Clockwise.transform.Rotate(new Vector3(0, 0, 99) * Time.deltaTime);
                Counter.transform.Rotate(new Vector3(0, 0, -100) * Time.deltaTime);
                break;
            default:
                break;
        }


        
    }


    IEnumerator InsideRadius()
    {
        while (true)
        {
            List<GameObject> Bullets = Camera.main.GetComponent<GameManager>().BulletList;
            foreach (GameObject bullet in Bullets)
            {
                if ((bullet.transform.position.x - transform.position.x) * (bullet.transform.position.x - transform.position.x) + (bullet.transform.position.y - transform.position.y) * (bullet.transform.position.y - transform.position.y) <= 2.5 * 2.5)
                {
                    InsidePoints.Add(bullet.transform.position);
                    //Debug.Log("Inside Radius");
                }
            }
            if (InsidePoints.Count > 0)
            {
                currentState = CreeperStates.Flee;
            }
            else
            {
                currentState = CreeperStates.Follow;
            }
            yield return new WaitForSeconds(.1f);
            InsidePoints.Clear();
        }
        

        yield return null;
    }

}
