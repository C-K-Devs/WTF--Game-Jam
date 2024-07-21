using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrawerType{
    Empty,
    JumpScare,
}
public class Drawer : MonoBehaviour, IInteractable
{
    public DrawerType drawerType;
    public float moveDistance = 0.5f;
    public bool opened = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }
    public void Interact()
    {
        StartCoroutine(OpenAndCloseDrawer());
        if (drawerType == DrawerType.JumpScare){
            if (!QuestManager.instance.jumpScared_OpenDrawer){
                UIManager.instance.ShowSubtitle("Aaaaa… Aah Aah Aah…", 5f, true);
                QuestManager.instance.jumpScared_OpenDrawer = true;
            }
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
