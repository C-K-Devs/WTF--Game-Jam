using UnityEngine;
using System.Collections.Generic;

public class SwipeManager2D : MonoBehaviour
{
    public Transform puzzleSprite;
    public float swipeThreshold = 50f;
    public float rotationSpeed = 10f;
    public float rotateAmount = 120f; // Amount to rotate beyond 100 degrees
    public float returnSpeed = 1f; // Speed to return to original rotation

    private Vector2 startTouchPosition;
    private List<string> swipePattern = new List<string>();
    private List<string> correctPattern = new List<string> { "right", "right", "left" };
    private Quaternion originalRotation;
    private bool isReturning = false;
    private float returnStartTime;
    private float returnDuration = 1f; // Duration to return to original rotation
    private int swipeCount = 0; // Counter for tracking the number of swipes

    void Start()
    {
        originalRotation = puzzleSprite.rotation;
    }

    void Update()
    {
        DetectSwipe();
        if (isReturning)
        {
            ReturnToOriginalRotation();
        }
    }

    private void DetectSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 endTouchPosition = Input.mousePosition;
            float swipeDistance = endTouchPosition.x - startTouchPosition.x;

            if (Mathf.Abs(swipeDistance) > swipeThreshold)
            {
                if (swipeDistance > 0)
                {
                    swipePattern.Add("right");
                    RotateSprite(-rotateAmount); // Rotate clockwise
                    Debug.Log("Swipe Right detected");
                }
                else
                {
                    swipePattern.Add("left");
                    RotateSprite(rotateAmount); // Rotate counter-clockwise
                    Debug.Log("Swipe Left detected");
                }

                Debug.Log("Current Swipe Pattern: " + string.Join(", ", swipePattern));

                swipeCount++;

                // Check pattern every 3 swipes
                if (swipeCount >= 3)
                {
                    CheckPattern();
                    swipeCount = 0; // Reset swipe count
                }

                isReturning = true;
                returnStartTime = Time.time;
            }
        }
    }

    private void RotateSprite(float angle)
    {
        puzzleSprite.Rotate(Vector3.forward * angle);
    }

    private void ReturnToOriginalRotation()
    {
        float t = (Time.time - returnStartTime) / returnDuration;
        t = Mathf.Clamp01(t);
        puzzleSprite.rotation = Quaternion.Lerp(puzzleSprite.rotation, originalRotation, t);

        if (t >= 1f)
        {
            isReturning = false; // Stop returning once we reach the original rotation
        }
    }

    private void CheckPattern()
    {
        if (swipePattern.Count >= correctPattern.Count)
        {
            // Check if the last N swipes match the correct pattern
            List<string> recentSwipes = swipePattern.Count > correctPattern.Count
                ? swipePattern.GetRange(swipePattern.Count - correctPattern.Count, correctPattern.Count)
                : swipePattern;

            bool patternMatched = true;
            for (int i = 0; i < correctPattern.Count; i++)
            {
                if (recentSwipes[i] != correctPattern[i])
                {
                    patternMatched = false;
                    break;
                }
            }

            if (patternMatched)
            {
                OnPatternComplete();
            }
            else
            {
                Debug.Log("Pattern Mismatch. Resetting swipe pattern.");
                swipePattern.Clear();
            }
        }
    }

    private void OnPatternComplete()
    {
        Debug.Log("Pattern Complete!");
        Debug.Log("Locked Opened");
        gameObject.GetComponent<SwipeManager2D>().enabled = false;
        // Implement what happens when the pattern is complete
        // e.g., open a door, show a message, etc.
    }
}
