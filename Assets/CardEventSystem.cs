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
    List<CardData> CardList = new List<CardData>();
    [SerializeField]
    GameManager manager;
    //CardTrigger RightCard;
    // Start is called before the first frame update
    void Start()
    {
        CardTrigger.OnCardTriggered += CardTrigger_OnCardTriggered;
        SelectCards();
    }
    void SelectCards()
    {
        CardData temp = CardList[UnityEngine.Random.Range(0, CardList.Count)];
        LeftCard.Title.text = temp.Title;
        LeftCard.Description.text = temp.Description;
        LeftCard.Effect = temp.Effect;
        temp = CardList[UnityEngine.Random.Range(0, CardList.Count)];
        RightCard.Title.text = temp.Title;
        RightCard.Description.text = temp.Description;
        RightCard.Effect = temp.Effect;
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
        SelectCards();
    }
}
