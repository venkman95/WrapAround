using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject Clockwise;
    [SerializeField]
    GameObject Counter;
    [SerializeField]
    GameObject Target;
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start() {
        Target = FindObjectOfType<Camera>().GetComponent<GameManager>().Player;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 DifferenceVector = (Target.transform.position - transform.position);
        transform.position += DifferenceVector.normalized * speed * Time.deltaTime;
            Clockwise.transform.Rotate(new Vector3(0,0,-100) * Time.deltaTime);
            Counter.transform.Rotate(new Vector3(0,0,99) * Time.deltaTime);
    }
}
