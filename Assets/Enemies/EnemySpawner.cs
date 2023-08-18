using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static event EventHandler<RoundArgs> RoundOver;
    public class RoundArgs : EventArgs
    {
        public int Rounds;
    }
   
    [SerializeField]
    List<GameObject> SpawnableEnemies = new List<GameObject>();
    GameManager Manager;
    int Rounds;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GetComponent<GameManager>();
        Rounds = 0;
        //SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Manager.currentState) {
            case GameManager.State.SpawnEnemies:
                Manager.currentState = GameManager.State.WaitForSpawning;
                StartCoroutine(SpawnEnemies());
                //SpawnEnemies();
                break;
            case GameManager.State.WaitForSpawning:
                Manager.currentState = GameManager.State.Combat;
                break;
            case GameManager.State.Combat:
                if (Manager.EnemyList.Count == 0) {
                    Rounds++;
                    RoundOver(this,new RoundArgs { Rounds = Rounds });
                    Manager.currentState = GameManager.State.SpawnCards;
                }
                break;
            default:
                break;
        }
    }

    public IEnumerator SpawnEnemies() {
        for (int i = 0; i < Rounds+1; i++) {
            StartCoroutine(Manager.TeleportToEmptyLocation(SpawnableEnemies[UnityEngine.Random.Range(0, SpawnableEnemies.Count)]));
            yield return new WaitForSeconds(2f);
        }
        yield return null;
    }
}
