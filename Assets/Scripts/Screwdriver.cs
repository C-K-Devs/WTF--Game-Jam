using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : MonoBehaviour, IInteractable
{
    public Drawer parentDrawer;
    public void Interact(){
        if (parentDrawer.opened == true){
            QuestManager.instance.screwDriverFound = true;
            Destroy(gameObject); 
        }
        else{
            parentDrawer.Interact();
        }
    }
}
