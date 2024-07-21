using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDoor : MonoBehaviour, IInteractable
{
    public GameObject smashOpener;
    public Transform[] screws;
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    public float angleOfRotation = 90;
    public float doorOpenTime = 2f;
    public int screwsRemoved = 0;
    public bool isOpen = false;

    void Start(){
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(0, angleOfRotation, 0));
    }
    public void Interact(){
        if (QuestManager.instance.masterKeyCollected){
            if (QuestManager.instance.metalScaleCollected)
            {
                // Hold to remove screw in order
                if (!isOpen){
                    isOpen = true;
                    UIManager.instance.ShowSubtitle("FINALLY! I AM OUT.", 5f, true);
                    StartCoroutine(OpenDoor());
                }
            }
            else{
                UIManager.instance.ShowSubtitle("Hmm... It still needs something more.", 5f, true);
            }
        }
        else{
            UIManager.instance.ShowSubtitle("THE MASTER KEY. I need it!", 5f, true);
        }
    }

    private IEnumerator OpenDoor()
    {
        float elapsedTime = 0f;
        while (elapsedTime < doorOpenTime)
        {
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / doorOpenTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
