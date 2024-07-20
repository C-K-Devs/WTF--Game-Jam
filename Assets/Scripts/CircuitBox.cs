using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBox : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Circuit Box opened.");
        if (QuestManager.instance.screwDriverFound){
            QuestManager.instance.wiresMatched = true;
        }
        else{
            UIManager.instance.ShowSubtitle("I would need a screwdriver for this...", 5f);
        }
    }
}
