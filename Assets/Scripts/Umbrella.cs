using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour, IInteractable
{
    public Transform attachPoint;
    public void Interact(){
        if (QuestManager.instance.phoneConnectedButFailed){
            transform.parent = attachPoint;
            transform.position = new Vector3(0, 0, 0);
            QuestManager.instance.umbrellaCollected = true;
        }
    }
}
