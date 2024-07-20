using UnityEngine;
using System.Collections;

public class SpiderJumpScare : MonoBehaviour
{
    public Transform targetPosition;  // The middle-center position
    public float moveDuration = 2f;  // Duration of the spider's movement
    public float shakeDuration = 1f;  // Duration of the camera shake
    public float shakeMagnitude = 5f;  // Magnitude of the camera shake (rotation)
    public float triggerDistance = 5f;  // Distance to trigger the jump scare

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private bool isJumpScareTriggered = false;
    private Transform cameraTransform;
    private Transform playerTransform;
    private Quaternion cameraOriginalRotation;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // Assuming the player has the tag "Player"
        originalPosition = transform.position;
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
        cameraOriginalRotation = cameraTransform.localRotation;
    }

    void Update()
    {
        if (!isJumpScareTriggered && Vector3.Distance(playerTransform.position, targetPosition.position) <= triggerDistance && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpiderMovement());
        }
    }

    IEnumerator SpiderMovement()
    {
        isJumpScareTriggered = true;
        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * 2f;  // Scale the spider to twice its size
        Quaternion startRotation = transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            // Move the spider
            transform.position = Vector3.Lerp(startPosition, targetPosition.position, elapsedTime / moveDuration);
            // Scale the spider
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / moveDuration);
            // Rotate the spider randomly
            transform.rotation = startRotation * Quaternion.Euler(Random.Range(315f, 370f) ,0 ,0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition.position;
        transform.localScale = endScale;

        // Shake the camera
        StartCoroutine(CameraShake());

        yield return new WaitForSeconds(shakeDuration);

        // Reset the spider to its original position, scale, and rotation
        transform.position = originalPosition;
        transform.localScale = originalScale;
        transform.rotation = originalRotation;

        isJumpScareTriggered = false;
    }

    IEnumerator CameraShake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float yShake = Mathf.Sin(Time.time * shakeMagnitude) * shakeMagnitude;
            cameraTransform.localRotation = cameraOriginalRotation * Quaternion.Euler(0, yShake, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localRotation = cameraOriginalRotation;
    }
}
