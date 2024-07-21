using UnityEngine;
using System.Collections;

public class SpiderJumpScare : MonoBehaviour
{
    public Transform targetPosition;
    public float moveDuration = 2f;
    public float shakeDuration = 1f;
    public float shakeMagnitude = 5f;
    private Transform originalParent;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    public bool isJumpScareTriggered = false;
    private Transform cameraTransform;
    private Quaternion cameraOriginalRotation;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        originalPosition = transform.position;
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
        cameraOriginalRotation = cameraTransform.localRotation;
    }


    public IEnumerator SpiderMovement()
    {
        QuestManager.instance.canMove = false;
        QuestManager.instance.canLook = false;
        isJumpScareTriggered = true;
        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * 2f;  // Scale the spider to twice its size
        Quaternion startRotation = transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, cameraTransform.position, elapsedTime / moveDuration);
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / moveDuration);
            transform.rotation = startRotation * Quaternion.Euler(Random.Range(315f, 370f) ,0 ,0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = cameraTransform.position;
        transform.localScale = endScale;

        
        StartCoroutine(QuestManager.instance.CameraShake(shakeDuration, shakeMagnitude));

        yield return new WaitForSeconds(shakeDuration);

        transform.position = originalPosition;
        transform.localScale = originalScale;
        transform.rotation = originalRotation;
        QuestManager.instance.canMove = true;
        QuestManager.instance.canLook = true;
        isJumpScareTriggered = false;
    }
    public IEnumerator SpiderFlew()
    {
        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * 2f;
        Quaternion startRotation = transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition.position, elapsedTime / moveDuration);
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / moveDuration);
            transform.rotation = startRotation * Quaternion.Euler(Random.Range(315f, 370f), 0, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition.position;
        transform.localScale = endScale;
    }

    
}
