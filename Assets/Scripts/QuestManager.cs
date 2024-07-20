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
    public string objectName;
    public GameObject obj;
    public bool isInteractable = false;
    public Color disabledColor = Color.white;
    public Color activeColor = Color.green;
    public UnityEvent onInteractedEvent;
}
public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
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
    public bool umbrellaCollected = false;
    public bool threePapersCollected = false;
    public bool safeUnlocked = false;
    public bool bunchOfKeysCollected = false;
    public bool jumpScared_ClosedDrawer = false;
    public bool masterKeyCollected = false;
    public bool doorOpened = false;
    

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
        if (questIndex > 0){
            DeactivateInteractables(quests[questIndex - 1]);
        }
        currentQuest = quests[questIndex].questName;
        ActivateInteractables(quests[questIndex]);
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
                interactable.isInteractable = true;
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
