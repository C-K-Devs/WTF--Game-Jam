using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    private Vector3 startPoint;
    private Vector3 startPosition;
    public CircuitBox circuitBox;


    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPosition();

        if (newPosition != Vector3.zero)
        {
            UpdateWire(newPosition);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Transform hitTransform = hit.transform;
                Debug.Log(hitTransform.parent.name);
                if (transform.parent.name.Equals(hitTransform.parent.name))
                {
                    Done();
                }
            }
        }
    }

    void Done()
    {
        lightOn.SetActive(true);
        circuitBox.WireMatched();
        Destroy(this);
    }

    private void OnMouseUp()
    {
        UpdateWire(startPosition);
    }

    void UpdateWire(Vector3 newPosition)
    {
        transform.position = newPosition;

        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector3.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
