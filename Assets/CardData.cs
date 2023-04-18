using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public string Title;
    public string Description;
    public bool EnabledOnStartup =true ;
    public List<CardData> EnableCards = new List<CardData>();
    public List<CardData> DisableCards = new List<CardData>();
    public GameObject Effect;
}
