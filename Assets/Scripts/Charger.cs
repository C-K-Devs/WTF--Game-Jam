using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour, IInteractable
{
    public Cupboard parentCupboard;
    public void Interact()
    {
        if (parentCupboard.isOpen){
            if (QuestManager.instance.phoneDead)
            {
                QuestManager.instance.chargerCollected = true;
                Destroy(gameObject);
            }
            else
            {
                UIManager.instance.ShowSubtitle("What would I do with this?", 5f, true);
            }
        }
        else
        {
            UIManager.instance.ShowSubtitle("What would I do with this?", 5f, true);
        }
        
    }

    
}
