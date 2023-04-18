using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardTrigger : MonoBehaviour
{
    public static event EventHandler OnCardTriggered;
    [SerializeField]
    Image loadingCircle;
    public TMP_Text Title;
    public TMP_Text Description;
    public GameObject Effect;
    float time = 0f;
    [SerializeField]
    float fillTime;
    Coroutine timer = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= fillTime) {           
            Instantiate(Effect);
            OnCardTriggered(this,EventArgs.Empty);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {

           timer = StartCoroutine(Timer());
        }
    }
    void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            StopCoroutine(timer);
            time = 0;
            loadingCircle.fillAmount = 0;
        }
    }

    IEnumerator Timer() {
        while (true) {
            time += Time.deltaTime;
            loadingCircle.fillAmount = time / fillTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
