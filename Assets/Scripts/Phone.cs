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
                    screen.SetActive(false);
                    switchedOn = false;
                }
                else
                {
                    UIManager.instance.ShowSubtitle("I need to get this thing charged...", 5f, true);
                }
            }
            
        }
    }
}
