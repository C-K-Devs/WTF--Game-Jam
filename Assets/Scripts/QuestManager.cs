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
    public Quest currentQuest;
    public List<Quest> quests;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentQuest = quests[0];
    }

    void Update()
    {
        
    }

    // void InvokeInteractablesEvents(Quest quest)
    // {
    //     foreach (var interactable in quest.interactables)
    //     {
    //         interactable.onInteractedEvent?.Invoke();
    //     }
    // }
}
