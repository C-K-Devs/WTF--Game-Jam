using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunchOfKeys : MonoBehaviour, IInteractable
{
    public Safe safe;
    public void Interact()
    {
        if (safe.isOpen){
            if (QuestManager.instance.safeUnlocked)
            {
                QuestManager.instance.bunchOfKeysCollected = true;
            }
        }
    }
}
