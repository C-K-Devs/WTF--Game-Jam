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
    public Color disabledColor;
    public Color activeColor;
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
        currentQuest = quests[questIndex].questName;
        ActivateInteractables(quests[questIndex]);
    }

    void ActivateInteractables(Quest quest)
    {
        foreach (var interactable in quest.interactables)
        {
            var obj = GameObject.Find(interactable.objectName);
            if (obj != null)
            {
                var renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = interactable.activeColor;
                }
                var collider = obj.GetComponent<Collider>();
                if (collider != null)
                {
                    collider.enabled = true;
                }
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
