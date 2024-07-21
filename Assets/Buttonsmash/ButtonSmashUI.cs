using UnityEngine;
using UnityEngine.UI;

public class ButtonSmashUI : MonoBehaviour
{
    public Text counterText; // Reference to the UI Text component for button smash counter
    public Text holdText;    // Reference to the UI Text component for hold countdown
    private int count = 0;
    private float decreaseTimer = 1.0f; // Time before the count starts decreasing
    private float timeSinceLastPress = 0f;

    private float holdDuration = 5f;    // Duration to hold the E key
    private float holdTimer = 0f;
    private bool isHolding = false;
    private bool isCaseOpened = false;
    private bool isScrewRemoved = false;

    void Start()
    {
        // Initialize the counter displays
        UpdateCounterText();
        UpdateHoldText();
    }

    void Update()
    {
        HandleButtonSmash();
        HandleHoldMechanism();
    }

    void HandleButtonSmash()
    {
        if (isCaseOpened) return;

        // Check for space bar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeSinceLastPress = 0f; // Reset the timer when space is pressed

            if (count < 15)
            {
                count++;
                UpdateCounterText();
            }

            if (count >= 15) //buttonsmash results & applications ....
            {
                isCaseOpened = true;
                counterText.text = "Case Opened";
            }
        }

        // Timer logic for decreasing the count
        if (count > 0 && !isCaseOpened)
        {
            timeSinceLastPress += Time.deltaTime;

            if (timeSinceLastPress >= decreaseTimer)
            {
                count--;
                timeSinceLastPress = 0f; // Reset the timer when count decreases
                UpdateCounterText();
            }
        }
    }

    void HandleHoldMechanism()
    {
        if (isScrewRemoved) return;

        // Check if the E key is being held down
        if (Input.GetKey(KeyCode.E))
        {
            holdTimer += Time.deltaTime;
            isHolding = true;

            if (holdTimer >= holdDuration)
            {
                holdTimer = holdDuration; // Cap the hold timer at holdDuration
                isScrewRemoved = true; // hold results & applications ....
                holdText.text = "Screw Removed";
            }
            else
            {
                UpdateHoldText();
            }
        }
        else
        {
            if (isHolding)
            {
                // Reset the timer if the E key is released before reaching the hold duration
                holdTimer = 0f;
                isHolding = false;
                UpdateHoldText();
            }
        }
    }

    void UpdateCounterText()
    {
        if (!isCaseOpened)
        {
            counterText.text = count + "/15";
        }
    }

    void UpdateHoldText()
    {
        if (!isScrewRemoved)
        {
            holdText.text = "Hold E: " + (holdDuration - holdTimer).ToString("F1") + "s";
        }
    }
}
