using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterKey : MonoBehaviour, IInteractable
{
    public ClosedDrawer parentDrawer;
    
    public void Interact(){
        if (parentDrawer.opened){
            QuestManager.instance.masterKeyCollected = true;
            UIManager.instance.StopShowSubtitle();
            Destroy(gameObject);
        }
        else{
            parentDrawer.Interact();
        }
    }
}
