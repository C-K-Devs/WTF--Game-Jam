using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Drawer opened.");
    }
}
