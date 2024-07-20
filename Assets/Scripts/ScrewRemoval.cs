using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewRemoval : MonoBehaviour
{
    public GameObject[] screws; // Assign screw GameObjects in the inspector
    public GameObject circuitBoxPanel; // The panel that contains the wire puzzle

    private int screwsRemoved = 0;

    public void RemoveScrew()
    {
        if (screwsRemoved < screws.Length)
        {
            Destroy(screws[screwsRemoved]); // Remove the screw
            screwsRemoved++;
            if (screwsRemoved == screws.Length)
            {
                OpenCircuitBox();
            }
        }
    }

    public void OpenCircuitBox()
    {   
        Debug.Log("All screws removed. Opening circuit box.");
        circuitBoxPanel.SetActive(true); // Activate the wire puzzle UI
        Cursor.lockState = CursorLockMode.None;
    }
}
