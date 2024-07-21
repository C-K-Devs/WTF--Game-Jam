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
                UIManager.instance.ShowSubtitle("Got the charger", 5f, true);
                QuestManager.instance.chargerCollected = true;
                Destroy(gameObject);
            }
            else
            {
                UIManager.instance.ShowSubtitle("Hmm... What would I do with this?", 5f, true);
            }
        }
        
    }

    
}
