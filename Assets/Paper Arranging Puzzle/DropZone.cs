using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public DragAndDrop correctImage;
    private PuzzleManager puzzleManager;

    void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragAndDrop draggedImage = eventData.pointerDrag.GetComponent<DragAndDrop>();

        if (draggedImage != null)
        {
            Transform previousParent = draggedImage.originalParent;
            DragAndDrop previousImage = GetComponentInChildren<DragAndDrop>();

            if (draggedImage == correctImage)
            {
                draggedImage.SwapImages(transform);
                if (previousImage != null)
                {
                    previousImage.SwapImages(previousParent);
                }
                puzzleManager.CheckPuzzleCompletion();
            }
            else
            {
                draggedImage.transform.SetParent(draggedImage.originalParent);
                draggedImage.transform.localPosition = Vector3.zero;
            }

            EnsureAllImagesHaveParent();
        }
    }

    private void EnsureAllImagesHaveParent()
    {
        DragAndDrop[] allImages = FindObjectsOfType<DragAndDrop>();
        foreach (DragAndDrop image in allImages)
        {
            if (image.transform.parent == image.canvas.transform)
            {
                foreach (DropZone dropZone in puzzleManager.dropZones)
                {
                    if (dropZone.transform.childCount == 0)
                    {
                        image.SwapImages(dropZone.transform);
                        break;
                    }
                }
            }
        }
    }
}
