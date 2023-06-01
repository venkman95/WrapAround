using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject OneRoundStar;
    [SerializeField]
    GameObject FiveRoundStar;
    [SerializeField]
    GameObject TenRoundStar;
    [SerializeField]
    GameManager Manager;
    void Start()
    {
        EnemySpawner.RoundOver += EnemySpawner_RoundOver;
    }

    private void EnemySpawner_RoundOver(object sender, EnemySpawner.RoundArgs e)
    {
        if (e.Rounds % 10 == 0)
        {
            float yPositionPixel = UnityEngine.Random.Range(0, Manager.maxHeight);
            float xPositionPixel = UnityEngine.Random.Range(0, Manager.maxWidth);
            Vector3 temp = new Vector3(xPositionPixel, yPositionPixel, 0);
            temp = Manager.camera.ScreenToWorldPoint(temp);
            Vector3 spawnPoint = new Vector3(temp.x, temp.y, 0);

            Instantiate(TenRoundStar, spawnPoint, transform.rotation);
        }
        else if (e.Rounds % 5 == 0)
        {
            float yPositionPixel = UnityEngine.Random.Range(0, Manager.maxHeight);
            float xPositionPixel = UnityEngine.Random.Range(0, Manager.maxWidth);
            Vector3 temp = new Vector3(xPositionPixel, yPositionPixel, 0);
            temp = Manager.camera.ScreenToWorldPoint(temp);
            Vector3 spawnPoint = new Vector3(temp.x, temp.y, 0);

            Instantiate(FiveRoundStar, spawnPoint, transform.rotation);
        }
        else
        {
            float yPositionPixel = UnityEngine.Random.Range(0, Manager.maxHeight);
            float xPositionPixel = UnityEngine.Random.Range(0, Manager.maxWidth);
            Vector3 temp = new Vector3(xPositionPixel, yPositionPixel, 0);
            temp = Manager.camera.ScreenToWorldPoint(temp);
            Vector3 spawnPoint = new Vector3(temp.x, temp.y, 0);

            Instantiate(OneRoundStar, spawnPoint, transform.rotation);

        }

    }
}
