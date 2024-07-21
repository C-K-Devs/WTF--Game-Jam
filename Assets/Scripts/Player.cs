using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject torch;
    public IInteractable currentInteractable;
    public bool isInteractable = false;
    public float interactRange = 5f;

    void Start()
    {
        torch.SetActive(false);
        UIManager.instance.ShowSubtitle("PRESS 'F' TO TURN ON THE TORCH", 99999f, true);
    }

    void Update()
    {
        if (QuestManager.instance.torchActivated || QuestManager.instance.wiresMatched)
        {
            HandleInteraction();
        }
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (QuestManager.instance.currentQuest)
            {
                case QuestName.Quest1:
                    if (!QuestManager.instance.torchActivated)
                    {
                        torch.SetActive(true);
                        QuestManager.instance.torchActivated = true;
                        UIManager.instance.StopShowSubtitle();
                    }
                    else if (QuestManager.instance.torchActivated && currentInteractable != null)
                    {
                        currentInteractable.Interact();
                        currentInteractable = null;
                    }
                    break;
                case QuestName.Quest2:
                    if (currentInteractable != null){
                        currentInteractable.Interact();
                        currentInteractable = null;
                    }
                    break;
                case QuestName.Quest3:
                    if (currentInteractable != null)
                    {
                        currentInteractable.Interact();
                        currentInteractable = null;
                    }
                    break;
                case QuestName.Quest4:
                    if (currentInteractable != null)
                    {
                        currentInteractable.Interact();
                        currentInteractable = null;
                    }
                    break;
                case QuestName.Quest5:
                    if (currentInteractable != null)
                    {
                        currentInteractable.Interact();
                        currentInteractable = null;
                    }
                    break;
            }
        }
        else if (Input.GetKey(KeyCode.F))
        {
            if (currentInteractable is CircuitBox circuitBox){
                circuitBox.UnlockScrews();
            }
        }
    }

    void HandleInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (currentInteractable != interactable)
                {
                    currentInteractable = interactable;
                    UIManager.instance.ShowSubtitle("PRESS 'F' TO INTERACT", 99999f, false);
                }

            }
            else if (hit.collider.transform.parent.TryGetComponent<IInteractable>(out IInteractable parentInteractable))
            {
                if (currentInteractable != parentInteractable)
                {
                    currentInteractable = parentInteractable;
                    UIManager.instance.ShowSubtitle("PRESS 'F' TO INTERACT", 99999f, false);
                }
            }
            else
            {
                if (currentInteractable != null)
                {
                    currentInteractable = null;
                    UIManager.instance.StopShowSubtitle();
                }
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable = null;
                UIManager.instance.StopShowSubtitle();
            }
        }
    }
}
