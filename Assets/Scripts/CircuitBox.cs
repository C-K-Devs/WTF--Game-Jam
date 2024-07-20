using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBox : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Circuit Box opened.");
    }
}
