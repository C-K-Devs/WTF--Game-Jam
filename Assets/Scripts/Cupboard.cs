using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboard : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    public bool openCupboard = false;
    public Transform door1;
    public Transform door2;
    public float angleOfRotation;
    public float cupboardOpenTime;
    private Quaternion originalRotation1;
    private Quaternion targetRotation1;
    private Quaternion originalRotation2;
    private Quaternion targetRotation2;

    void Start(){
        originalRotation1 = door1.rotation;
        targetRotation1 = Quaternion.Euler(originalRotation1.eulerAngles + new Vector3(0, angleOfRotation, 0));
        originalRotation2 = door2.rotation;
        targetRotation2 = Quaternion.Euler(originalRotation2.eulerAngles - new Vector3(0, angleOfRotation, 0));
    }
    public void Interact()
    {
        if (!openCupboard){
            if (QuestManager.instance.cupboardKeyFound)
            {
                if (!isOpen)
                {
                    isOpen = true;
                    UIManager.instance.ShowSubtitle("Haash... Finally!", 5f, true);
                    StartCoroutine(OpenClosedCupboard());
                }
            }
            else
            {
                UIManager.instance.ShowSubtitle("I need to get the keys first...", 5f, true);
            }
        }
        else{
            if (!isOpen){
                isOpen = true;
                StartCoroutine(OpenOpenCupboard());
            }
            else
            {
                isOpen = false;
                StartCoroutine(CloseOpenCupboard());
            }

        }

    }

    private IEnumerator OpenClosedCupboard()
    {
        float elapsedTime = 0f;
        while (elapsedTime < cupboardOpenTime)
        {
            door1.rotation = Quaternion.Lerp(originalRotation1, targetRotation1, elapsedTime / cupboardOpenTime);
            door2.rotation = Quaternion.Lerp(originalRotation2, targetRotation2, elapsedTime / cupboardOpenTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        door1.rotation = targetRotation1;
        door2.rotation = targetRotation2;
        QuestManager.instance.cupboardOpened = true;
        QuestManager.instance.StartQuest(3);
    }
    private IEnumerator OpenOpenCupboard()
    {
        float elapsedTime = 0f;
        while (elapsedTime < cupboardOpenTime)
        {
            door1.rotation = Quaternion.Lerp(originalRotation1, targetRotation1, elapsedTime / cupboardOpenTime);
            door2.rotation = Quaternion.Lerp(originalRotation2, targetRotation2, elapsedTime / cupboardOpenTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        door1.rotation = targetRotation1;
        door2.rotation = targetRotation2;
    }
    private IEnumerator CloseOpenCupboard()
    {
        float elapsedTime = 0f;
        while (elapsedTime < cupboardOpenTime)
        {
            door1.rotation = Quaternion.Lerp(targetRotation1, originalRotation1, elapsedTime / cupboardOpenTime);
            door2.rotation = Quaternion.Lerp(targetRotation2, originalRotation2, elapsedTime / cupboardOpenTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        door1.rotation = originalRotation1;
        door2.rotation = originalRotation2;
    }
}
