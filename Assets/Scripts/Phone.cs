using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    public GameObject screen;
    public bool switchedOn = true;
    public void Interact(){
        if (QuestManager.instance.cupboardOpened){
            if (!QuestManager.instance.phoneConnectedButFailed)
            {
                if (switchedOn)
                {
                    QuestManager.instance.phoneDead = true;
                    UIManager.instance.ShowSubtitle("Shit, the phone battery is dead.", 5f, true); 
                    screen.SetActive(false);
                    switchedOn = false;
                }
                else
                {
                    UIManager.instance.ShowSubtitle("I should charge the phone first.", 5f, true);
                }
            }
            
        }
    }
}
