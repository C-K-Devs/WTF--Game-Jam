using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public int switchNo = 1;
    public void Interact()
    {
        Debug.Log("Switch" + switchNo + " flipped.");
    }
}
