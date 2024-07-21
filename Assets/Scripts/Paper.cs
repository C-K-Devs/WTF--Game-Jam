using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Paper : MonoBehaviour, IInteractable
{
    public int paperNo = 1;
    
    public void Interact(){
        if (QuestManager.instance.phoneConnectedButFailed){
            if (paperNo == 1 || paperNo == 2)
            {
                QuestManager.instance.papersCollected[paperNo - 1] = true;
                UIManager.instance.papers[paperNo - 1].SetActive(true);
                if (QuestManager.instance.papersCollected.All(p => p.Value == true))
                {
                    QuestManager.instance.threePapersCollected = true;
                    // Enable PuzzleManager
                }
                Destroy(gameObject);
            }
            if (paperNo == 3)
            {
                if (QuestManager.instance.umbrellaCollected)
                {
                    StartCoroutine(QuestManager.instance.spiderJumpScare.SpiderFlew());
                    QuestManager.instance.papersCollected[2] = true;
                    UIManager.instance.papers[2].SetActive(true);
                    if (QuestManager.instance.papersCollected.All(p => p.Value == true))
                    {
                        QuestManager.instance.threePapersCollected = true;
                    }
                    Destroy(FindAnyObjectByType<Umbrella>().gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    UIManager.instance.StopShowSubtitle();
                    StartCoroutine(QuestManager.instance.spiderJumpScare.SpiderMovement());
                }
            }
        }
        
    }


}
