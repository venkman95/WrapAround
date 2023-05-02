using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public enum EnemyType
    {
        Creeper,Turret,Tank
    };
    [SerializeField]
    public EnemyType Type;
    public static event EventHandler<EnemyArgs> OnEnemyCreation;
    public static event EventHandler<EnemyArgs> OnEnemyDestruction;
    public class EnemyArgs : EventArgs {
        public GameObject enemy;
    }
    [SerializeField]
    int Health;

    void Start() {
        OnEnemyCreation(this,new EnemyArgs { enemy = gameObject });
    }

    public void Hit() {
       
        Health--;
        
        if (Health <= 0) {
            OnEnemyDestruction(this,new EnemyArgs { enemy = gameObject });
            Destroy(gameObject);
        }
    }
}
