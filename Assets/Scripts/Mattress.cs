using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mattress : MonoBehaviour, IInteractable
{
    public Transform topRightCorner;
    public float cornerRadius = 2f;

    public void Interact()
    {
        if (QuestManager.instance.mailChecked && !QuestManager.instance.cupboardKeyFound){
            if (IsPlayerInCorner())
            {
                QuestManager.instance.cupboardKeyFound = true;
                UIManager.instance.ShowSubtitle("Haash! Got the key...", 5f, true);
            }
            else
            {
                UIManager.instance.ShowSubtitle("Ahh... There's nothing under this corner.", 5f, true);
            }
        }
    }

    private bool IsPlayerInCorner()
    {
        Transform playerTransform = QuestManager.instance.player.transform;
        float distance = Vector3.Distance(playerTransform.position, topRightCorner.position);
        return distance <= cornerRadius;
    }

    private void DoSomething()
    {
        // Implement the behavior you want when the player is in the corner and interacts
        Debug.Log("Player is in the corner and interacted with the mattress.");
        // Add your custom logic here
    }
}
