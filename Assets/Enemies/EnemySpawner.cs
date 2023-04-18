using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> SpawnableEnemies = new List<GameObject>();
    GameManager Manager;
    int Rounds;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GetComponent<GameManager>();
        Rounds = 1;
        //SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Manager.currentState) {
            case GameManager.State.SpawnEnemies:
                Manager.currentState = GameManager.State.WaitForSpawning;
                SpawnEnemies();
                break;
            case GameManager.State.WaitForSpawning:
                Manager.currentState = GameManager.State.Combat;
                break;
            case GameManager.State.Combat:
                if (Manager.EnemyList.Count == 0) {
                    Rounds++;
                    Manager.currentState = GameManager.State.SpawnCards;
                }
                break;
            default:
                break;
        }
    }

    public void SpawnEnemies() {
        for (int i = 0; i < Rounds; i++) {
            float yPositionPixel = Random.Range(0,Manager.maxHeight);
            float xPositionPixel = Random.Range(0,Manager.maxWidth);
            Vector3 temp = new Vector3(xPositionPixel,yPositionPixel,0);
            temp = Manager.camera.ScreenToWorldPoint(temp);
            Vector3 spawnPoint = new Vector3(temp.x,temp.y,0);

            Instantiate(SpawnableEnemies[Random.Range(0,SpawnableEnemies.Count)],spawnPoint,transform.rotation);
        }
       ;
    }
}
