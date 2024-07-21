using System.Collections;
using UnityEngine;

public class Poster : MonoBehaviour, IInteractable
{
    public float moveDuration = 0.5f;
    public Vector3 offsetFromCamera = new Vector3(0, 0, 1.5f);
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public bool isViewingPoster = false;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void Interact()
    {
        if (!isViewingPoster)
        {
            StartCoroutine(MoveToView());
        }
        else
        {
            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    private IEnumerator MoveToView()
    {
        isViewingPoster = true;
        QuestManager.instance.canMove = false;
        QuestManager.instance.canLook = false;
        Transform cameraTransform = Camera.main.transform;
        Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * offsetFromCamera.z +
                                  cameraTransform.right * offsetFromCamera.x +
                                  cameraTransform.up * offsetFromCamera.y;
        Quaternion targetRotation = Quaternion.Euler(-90,0,180);

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / moveDuration);
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        isViewingPoster = false;
        QuestManager.instance.canMove = true;
        QuestManager.instance.canLook = true;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, originalPosition, elapsedTime / moveDuration);
            transform.rotation = Quaternion.Lerp(startRotation, originalRotation, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}