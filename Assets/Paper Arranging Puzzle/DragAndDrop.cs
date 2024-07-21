using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    private Vector3 initialPosition;
    [HideInInspector] public Transform originalParent;

    void Start()
    {
        initialPosition = transform.position;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(canvas.transform, true);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out Vector3 globalMousePos);
        transform.position = globalMousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == canvas.transform)
        {
            transform.position = initialPosition;
            transform.SetParent(originalParent);
        }
    }

    public void SwapImages(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
    }
}
