using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public enum QuestName{
    Quest1,
    Quest2,
    Quest3,
    Quest4,
    Quest5
}

[Serializable]
public class Quest{
    public QuestName questName;
    public List<Interactables> interactables = new List<Interactables>();
}

[Serializable]
public class Interactables{
    public GameObject obj;
    public bool isInteractable = false;
    public Color disabledColor = Color.white;
    public Color activeColor = Color.green;
}
public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public Player player;
    public QuestName currentQuest;
    public List<Quest> quests;
    public bool torchActivated = false;
    public bool jumpScared_OpenDrawer = false;
    public bool screwDriverFound = false;
    public bool wiresMatched = false;
    public bool mailChecked = false;
    public bool cupboardKeyFound = false;
    public bool cupboardOpened = false;
    public bool phoneDead = false;
    public bool chargerCollected = false;
    public bool phoneConnectedButFailed = false;
    public bool umbrellaCollected = false;
    public bool threePapersCollected = false;
    public bool safeUnlocked = false;
    public bool bunchOfKeysCollected = false;
    public bool jumpScared_ClosedDrawer = false;
    public bool masterKeyCollected = false;
    public bool metalScaleCollected = false;
    public bool doorOpened = false;
    public GameObject mainLight;

    public bool canMove = true;
    public bool canLook = true;

    public Dictionary<int, bool> papersCollected = new Dictionary<int, bool>();

    public SpiderJumpScare spiderJumpScare;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        StartQuest(0);
        
    }

    public void StartQuest(int questIndex)
    {
        currentQuest = quests[questIndex].questName;
        
        if (questIndex == 3)
        {
            StartCoroutine(LookAtFirstInteractable(quests[questIndex].interactables[0].obj.transform));
            for (int i = 1; i <= 3; i++)
            {
                papersCollected.Add(i, false);
            }
        }
    }

    private IEnumerator LookAtFirstInteractable(Transform target)
    {
        canMove = false;
        canLook = false;
        target.GetComponent<Phone>()?.screen.SetActive(true);
        target.GetComponent<Phone>().switchedOn = true;
        Transform playerCamera = Camera.main.transform;
        StartCoroutine(CameraShake(0.3f, 0.3f));

        // Wait for the shake to finish
        yield return new WaitForSeconds(0.3f);

        Quaternion initialRotation = playerCamera.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - playerCamera.position);

        float elapsedTime = 0f;
        float duration = 0.3f;

        while (elapsedTime < duration)
        {
            playerCamera.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.rotation = targetRotation;

        player.GetComponent<FirstPersonController>().UpdateRotationValues(targetRotation);
        canMove = true;
        canLook = true;
    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {
        Transform playerCamera = Camera.main.transform;
        Vector3 originalPosition = playerCamera.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 shakeOffset = UnityEngine.Random.insideUnitSphere * magnitude;
            playerCamera.localPosition = originalPosition + shakeOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.localPosition = originalPosition;
    }
    void ActivateInteractables(Quest quest)
    {
        foreach (var interactable in quest.interactables)
        {
            if (interactable.obj != null)
            {
                if (interactable.obj.TryGetComponent<Renderer>(out Renderer renderer)){
                    renderer.material.color = interactable.activeColor;
                }
                interactable.isInteractable = true;
            }
        }
    }

    void DeactivateInteractables(Quest quest)
    {
        foreach (var interactable in quest.interactables)
        {
            if (interactable.obj != null)
            {
                if (interactable.obj.TryGetComponent<Renderer>(out Renderer renderer))
                {
                    renderer.material.color = interactable.disabledColor;
                }
                interactable.isInteractable = false;
            }
        }
    }


    // void InvokeInteractablesEvents(Quest quest)
    // {
    //     foreach (var interactable in quest.interactables)
    //     {
    //         interactable.onInteractedEvent?.Invoke();
    //     }
    // }
}
