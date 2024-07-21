using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClosedDrawerType
{
    Normal,
    JumpScare
}
public class ClosedDrawer : MonoBehaviour, IInteractable
{
    public ClosedDrawerType drawerType;
    public float moveDistance = 0.5f;
    public bool opened = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }
    public void Interact()
    {
        if (QuestManager.instance.bunchOfKeysCollected){
            StartCoroutine(OpenAndCloseDrawer());
            if (drawerType == ClosedDrawerType.JumpScare)
            {
                if (!QuestManager.instance.jumpScared_ClosedDrawer)
                {
                    UIManager.instance.ShowSubtitle("AAAAAAAAAAAAAAAAA!!!", 5f, true);
                    QuestManager.instance.jumpScared_ClosedDrawer = true;
                }
            }
        }
        else{
            UIManager.instance.ShowSubtitle("I require keys for these...", 5f, true);
        }
    }

    private IEnumerator OpenAndCloseDrawer()
    {
        print("Opening or Closing");
        Vector3 forwardPosition = initialPosition + transform.right * -moveDistance;
        if (!opened)
        {
            opened = true;
            yield return MoveDrawer(initialPosition, forwardPosition, 0.5f);
        }
        else
        {
            opened = false;
            yield return MoveDrawer(forwardPosition, initialPosition, 0.5f);
        }
    }

    private IEnumerator MoveDrawer(Vector3 start, Vector3 end, float duration)
    {
        print("Moving");

        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
    }
}
