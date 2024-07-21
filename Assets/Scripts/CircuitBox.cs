using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBox : MonoBehaviour, IInteractable
{
    public Transform[] screws;
    public int screwsRemoved = 0;
    private float holdDuration = 5f;
    public float holdTimer = 0f;
    private bool isHolding = false;
    private bool camRemove = false;
    public bool isScrewRemoved = false;
    public GameObject wiresPuzzle;
    public int noOfWiresMatched = 0;
    public GameObject dhakkan;
    public void Interact()
    {
        Debug.Log("Trying to circuit box");
        if (!QuestManager.instance.wiresMatched){
            if (!QuestManager.instance.screwDriverFound)
            {
                UIManager.instance.ShowSubtitle("I need a Screwdriver", 5f, true);
            }
            else{
                holdTimer = 0f;
                isScrewRemoved = false;
            }
        }
    }

    public void UnlockScrews(){
        if (isScrewRemoved || screwsRemoved >= 4) return;
        if (!QuestManager.instance.wiresMatched)
        {
            if (QuestManager.instance.screwDriverFound)
            {
                holdTimer += Time.deltaTime;
                isHolding = true;
                if (holdTimer < holdDuration){
                    screws[screwsRemoved].Rotate(-Vector3.up * 360 * Time.deltaTime);
                }
                else
                {
                    isScrewRemoved = true;
                    holdTimer = 0f;
                    StartCoroutine(RemoveScrew(screwsRemoved));
                    screwsRemoved++;
                    if (screwsRemoved >= 4)
                    {
                        wiresPuzzle.SetActive(true);
                        dhakkan.SetActive(false);
                    }
                }
            }
        }
    }

    IEnumerator RemoveScrew(int screwIndex)
    {
        if (screwIndex < 0 || screwIndex >= screws.Length) yield break;

        Transform screw = screws[screwIndex];
        Vector3 initialPosition = screw.position;
        Vector3 targetPosition = initialPosition + new Vector3(0, -3f, 3f);
        float flightDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < flightDuration)
        {
            float t = elapsedTime / flightDuration;
            screw.position = Vector3.Lerp(initialPosition, targetPosition, t) + new Vector3(0, Mathf.Sin(t * Mathf.PI), 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        screw.position = targetPosition;
        yield return new WaitForSeconds(2f);

        screw.gameObject.SetActive(false);
    }

    public void WireMatched(){
        noOfWiresMatched++;
        if (noOfWiresMatched >= 4){
            QuestManager.instance.wiresMatched = true;
            wiresPuzzle.SetActive(false);
            UIManager.instance.ShowSubtitle("Better than nothing.", 5f, true);
            UIManager.instance.ShowSubtitle("I remember Mummy has left me a note in my Laptop.", 5f, true);
            QuestManager.instance.player.torch.SetActive(false);
            QuestManager.instance.mainLight.SetActive(true);
            QuestManager.instance.StartQuest(1);
        }
        
    }
}
