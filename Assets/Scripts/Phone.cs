using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour, IInteractable
{
    public GameObject screen;
    public bool switchedOn = true;
    public void Interact(){
        if (switchedOn){
            screen.SetActive(false);
            switchedOn = false;
        }
        UIManager.instance.ShowSubtitle("I need to get this thing charged...", 5f, true);
    }
}
