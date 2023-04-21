using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardEventSystem : MonoBehaviour
{
    [SerializeField]
    CardTrigger LeftCard;
    [SerializeField]
    CardTrigger RightCard;
    [SerializeField]
    CardData test;
    [SerializeField]
    GameManager manager;
    //CardTrigger RightCard;
    // Start is called before the first frame update
    void Start()
    {
        CardTrigger.OnCardTriggered += CardTrigger_OnCardTriggered;
        LeftCard.Title.text = test.Title;
        LeftCard.Description.text = test.Description;
        LeftCard.Effect = test.Effect;
        RightCard.Title.text = test.Title;
        RightCard.Description.text = test.Description;
        RightCard.Effect = test.Effect;
    }
    private void Update() {
        if (manager.currentState == GameManager.State.SpawnCards) {
            SpawnCards();
        }
    }

    private void SpawnCards() {

        if (manager.BulletList.Count > 0)
        {
            manager.ClearAllBullets();
        }
        
        manager.currentState = GameManager.State.PickingCards;
        LeftCard.gameObject.SetActive(true);
        RightCard.gameObject.SetActive(true);
        
    }

    private void CardTrigger_OnCardTriggered(object sender,EventArgs e) {
        //LeftCard.enabled = false;
        manager.currentState = GameManager.State.SpawnEnemies;
        LeftCard.gameObject.SetActive(false);
        RightCard.gameObject.SetActive(false);
        
    }
}
