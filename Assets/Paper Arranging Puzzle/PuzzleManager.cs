using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public DropZone[] dropZones;
    public GameObject finalImage;

    void Start()
    {
        // Initially hide the final image
        finalImage.SetActive(false);
    }

    public void CheckPuzzleCompletion()
    {
        foreach (DropZone dropZone in dropZones)
        {
            if (dropZone.correctImage.transform.parent != dropZone.transform)
                return;
        }

        // Hide all clue images
        foreach (DropZone dropZone in dropZones)
        {
            dropZone.correctImage.gameObject.SetActive(false);
        }

        // Show the final image
        finalImage.SetActive(true);
    }
}
