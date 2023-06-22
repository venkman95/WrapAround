using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    public static event EventHandler ClearBullets;
    //PlayerAnimator
    [SerializeField]
    Animator PlayerAnimator;
    //State of the game
    public enum State { SpawnEnemies, WaitForSpawning ,Combat,SpawnCards,PickingCards,GameOver};
    //PlayerData
    [SerializeField]
    int MaxPlayerHealth;
    int CurrentPlayerHealth;
    bool PlayerInvulnerability = false;
    //PlayerObject
    [SerializeField]
    public GameObject Player;
    

    //Setup for the WrapAroundSystem
    [SerializeField]
    public Camera camera;
    public List<GameObject> BulletList = new List<GameObject>();
    public List<GameObject> EnemyList = new List<GameObject>();
    public float maxHeight;
    public float maxWidth;
    public State currentState = State.SpawnEnemies;

    // Start is called before the first frame update
    void Start()
    {
        //Sets up the visibility checker used on all objects that wrap around
        
        //Player Health set to max
        CurrentPlayerHealth = MaxPlayerHealth;
        //Sets Up the lists for enemies and bullets
        Bullet.OnBulletCreation += Bullet_OnBulletCreation;
        Bullet.OnBulletDestruction += Bullet_OnBulletDestruction;
        //Sets up what happens when bullet hits player
        Bullet.HitPlayer += Bullet_HitPlayer;
        Enemy.OnEnemyCreation += Enemy_OnEnemyCreation;
        Enemy.OnEnemyDestruction += Enemy_OnEnemyDestruction;
        //Debug.Log(Screen.safeArea);
        //Debug.Log(playerSprite.sprite.rect.size);
        maxHeight = camera.pixelHeight;
        maxWidth = camera.pixelWidth;
    }

    public void ClearAllBullets()
    {
        ClearBullets(this,EventArgs.Empty);
    }

    private void Enemy_OnEnemyDestruction(object sender,Enemy.EnemyArgs e) {
        EnemyList.Remove(e.enemy);
        //Debug.Log(EnemyList.Count);
    }

    void Enemy_OnEnemyCreation(object sender,Enemy.EnemyArgs e) {
        EnemyList.Add(e.enemy);
    }

    private void Bullet_HitPlayer(object sender,EventArgs e) {
        if (!PlayerInvulnerability)
        {
            CurrentPlayerHealth--;
            StartCoroutine(PlayerInvulnerabilityState());
        }
        Debug.Log(CurrentPlayerHealth);
    }

    void Bullet_OnBulletDestruction(object sender,Bullet.BulletArgs e) {
        BulletList.Remove(e.Bullet);   
    }

    private void Bullet_OnBulletCreation(object sender,Bullet.BulletArgs e) {
        BulletList.Add(e.Bullet);
        //Debug.Log(BulletList.Count);
    }

    // Update is called once per frame


    private void Update()
    {
        CheckforWrap(Player);
        foreach (GameObject bullet in BulletList)
        {
            CheckforWrap(bullet);
        }
        foreach (GameObject enemy in EnemyList)
        {
            CheckforWrap(enemy);
        }
    }

    private void CheckforWrap(GameObject Object) {
        Vector2 objectScreenPosition = camera.WorldToScreenPoint(Object.transform.position);
        if (objectScreenPosition.x < 0f) {
            WrapRight(objectScreenPosition,Object);
            //Debug.Log("Touching the edge");
        }
        if (objectScreenPosition.x > maxWidth) {
            
             WrapLeft(objectScreenPosition,Object);
            
            //Debug.Log("Touching the edge");
        }
        if (objectScreenPosition.y < 0f) {
            WrapUp(objectScreenPosition,Object);
            
            //Debug.Log("Touching the edge");
        }
        if (objectScreenPosition.y > maxHeight) {
            
             WrapDown(objectScreenPosition,Object);
            
            //Debug.Log("Touching the edge");
        }
    }

    private void WrapDown(Vector2 objectScreenPosition,GameObject Object) {
        //Debug.Log("Wrap");
        Vector2 newObectY = new Vector2(objectScreenPosition.x,objectScreenPosition.y - maxHeight);
        Object.transform.position = (camera.ScreenToWorldPoint(newObectY));
        Object.transform.position = new Vector3(Object.transform.position.x,Object.transform.position.y,0);
    }

    private void WrapUp(Vector2 objectScreenPosition,GameObject Object) {
        //Debug.Log("Wrap");
        Vector2 newObjectY = new Vector2(objectScreenPosition.x,objectScreenPosition.y + maxHeight);
        Object.transform.position = (camera.ScreenToWorldPoint(newObjectY));
        Object.transform.position = new Vector3(Object.transform.position.x,Object.transform.position.y,0);
    }

    private void WrapLeft(Vector2 objectScreenPosition,GameObject Object) {
        //Debug.Log("Wrap");
        Vector2 newObjectX = new Vector2(objectScreenPosition.x - maxWidth,objectScreenPosition.y);
        Object.transform.position = (camera.ScreenToWorldPoint(newObjectX));
        Object.transform.position = new Vector3(Object.transform.position.x,Object.transform.position.y,0);
    }

    private void WrapRight(Vector2 objectScreenPosition,GameObject Object) {
        //Debug.Log("Wrap");
        Vector2 newObjectX = new Vector2(objectScreenPosition.x + maxWidth,objectScreenPosition.y);
        Object.transform.position = (camera.ScreenToWorldPoint(newObjectX));
        Object.transform.position = new Vector3(Object.transform.position.x,Object.transform.position.y,0);
    }

    IEnumerator PlayerInvulnerabilityState()
    {
        Debug.Log("Invulnerable");
        float InvulTime = 5f;
        PlayerInvulnerability = true;
        StartCoroutine(PlayerInvulenerabilityAnimation(InvulTime + (MaxPlayerHealth - CurrentPlayerHealth)));
        yield return new WaitForSeconds(InvulTime + (MaxPlayerHealth - CurrentPlayerHealth));
        PlayerInvulnerability = false;
        Debug.Log("Vulnerable");
        yield return null;
    }
    IEnumerator PlayerInvulenerabilityAnimation(float WaitTime)
    {
        float currentTime = 0f;
        PlayerAnimator.speed = 1f;
        PlayerAnimator.Play("PlayerHurtAnimation");  
        while (currentTime < WaitTime)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
            //Debug.Log(currentTime);
            PlayerAnimator.speed += Time.deltaTime;
        }
        PlayerAnimator.Play("NormalState");
        yield return null;
    }
}
