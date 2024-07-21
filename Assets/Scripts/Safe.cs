using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour, IInteractable
{
    public GameObject radialLock;
    public Transform door;
    public float openDoorTime = 0.5f;
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    public float angleOfRotation = 90;
    public bool isOpen = true;
    
    void Start(){
        originalRotation = door.rotation;
        targetRotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(0, angleOfRotation, 0));
    }
    public void Interact(){
        if (QuestManager.instance.phoneConnectedButFailed){
            Debug.Log("Charger connected");
            UIManager.instance.ShowSubtitle("I think I wrote its password on a piece of paper.", 5f, true); 
            if (QuestManager.instance.threePapersCollected){
                
                 Debug.Log("Papers collected and solved");
                // QuestManager.instance.canLook = false;
                // QuestManager.instance.canMove = false;
                // radialLock.SetActive(true);
                QuestManager.instance.safeUnlocked = true;
                if (!isOpen){
                    Debug.Log("was not open");
                    isOpen = true;
                    StartCoroutine(OpenSafeDoor());
                     
                }
            }
    
        }
        else{
            UIManager.instance.ShowSubtitle("I should charge my phone first.", 5f, true);
        }
    }

    private IEnumerator OpenSafeDoor()
    {
        Debug.Log("Opening Safe door");
        float elapsedTime = 0f;
        while (elapsedTime < openDoorTime)
        {
            door.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / openDoorTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        door.rotation = targetRotation;
    }
}
