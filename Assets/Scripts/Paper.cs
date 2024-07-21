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
                if (paperNo == 1)
                {
                    UIManager.instance.ShowSubtitle(" Here’s one.", 5f, true);
                }
                if (paperNo == 2)
                {
                    UIManager.instance.ShowSubtitle(" Found the Second.", 5f, true);
                }
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
                    UIManager.instance.ShowSubtitle(" Finally, I got the third.", 5f, true); 
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
                    UIManager.instance.ShowSubtitle(" I should try a long object.", 5f, true); 
                }
            }
        }
        
    }


}
