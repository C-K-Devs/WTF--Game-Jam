using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;

    void OnMouseDown()
    {
        if (onInteract != null)
        {
            onInteract.Invoke();
        }
    }
}
