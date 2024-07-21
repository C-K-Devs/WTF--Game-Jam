using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugSocket : MonoBehaviour, IInteractable
{
    public GameObject charger;
    public void Interact(){
        if (QuestManager.instance.chargerCollected){
            charger.SetActive(true);
            UIManager.instance.ShowSubtitle("Fuck! The power outage. What a fool I am!", 5f, true);
            QuestManager.instance.phoneConnectedButFailed = true;
        }
        else
        {
            UIManager.instance.ShowSubtitle("Hmm... What would I do with this?", 5f, true);
        }
    }
}
