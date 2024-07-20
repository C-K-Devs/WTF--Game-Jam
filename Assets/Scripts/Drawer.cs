using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrawerType{
    Empty,
    JumpScare,
    Puzzle
}
public class Drawer : MonoBehaviour, IInteractable
{
    public DrawerType drawerType;
    public void Interact()
    {
        if (drawerType == DrawerType.JumpScare){
            if (!QuestManager.instance.jumpScared_OpenDrawer){
                UIManager.instance.ShowSubtitle("AAAAAAAAAAAAAAAAA!!!", 5f, true);
                QuestManager.instance.jumpScared_OpenDrawer = true;
            }
        }
        if (drawerType == DrawerType.Puzzle){
            QuestManager.instance.screwDriverFound = true;
        }
    }
}
