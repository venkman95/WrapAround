using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VisibilityChecker : MonoBehaviour
{
   
    bool OnScreen = false;
    public static event EventHandler<VisibilityArgs> OnInvisibility;
    public class VisibilityArgs : EventArgs {
        public GameObject Object;
    }
    void OnBecameInvisible() {
        if (gameObject.activeSelf!= false)
        {
            AnnounceOffScreen();
        }  
    }


    private void AnnounceOffScreen() {
        Debug.Log("OffScreen");
        OnScreen = false;
        StartCoroutine(WaitToComeBack());
        if (gameObject.transform.parent != null) {
            OnInvisibility(this,new VisibilityArgs { Object = gameObject.transform.parent.gameObject });
        } else {
            OnInvisibility(this,new VisibilityArgs { Object = gameObject });
        }
    }
    void OnBecameVisible() {
        OnScreen = true;
    }
    IEnumerator WaitToComeBack() {
        yield return new WaitForSeconds(1f);
        if (!OnScreen) {
            AnnounceOffScreen();
        }
        yield return null;
    }
}
