using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public int switchNo = 1;
    public void Interact()
    {
        Debug.Log("Switch" + switchNo + " flipped.");
        UIManager.instance.ShowSubtitle("The Switches are not working, I think I should find the Circuit box.", 5f, true);
    }
}
