using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBox : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (QuestManager.instance.screwDriverFound)
        {
            QuestManager.instance.wiresMatched = true;
            UIManager.instance.ShowSubtitle("I must check mum's mail...", 5f, true);
            QuestManager.instance.player.torch.SetActive(false);
            QuestManager.instance.mainLight.SetActive(true);
            QuestManager.instance.StartQuest(1);
        }
        else
        {
            UIManager.instance.ShowSubtitle("I would need a screwdriver for this...", 5f, true);
        }
    }
}
