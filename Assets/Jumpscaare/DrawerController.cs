using UnityEngine;
using System.Collections;

public class DrawerController : MonoBehaviour
{
    public GameObject[] jumpScares;  // Array to hold the jump scare objects
    public AudioClip scareSound;
    private bool isOpen = false;

    void Start()
    {
        foreach (GameObject scare in jumpScares)
        {
            scare.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (!isOpen)
        {
            OpenDrawer();
        }
    }

    void OpenDrawer()
    {
        // Implement drawer opening animation or logic here
        // For simplicity, we'll just log a message
        Debug.Log("Drawer opened");

        // Trigger the jump scares
        foreach (GameObject scare in jumpScares)
        {
            scare.SetActive(true);
            scare.GetComponent<AudioSource>().PlayOneShot(scareSound);
            StartCoroutine(JumpScareAnimation(scare));
        }

        isOpen = true;
    }

    IEnumerator JumpScareAnimation(GameObject scare)
    {
        RectTransform[] children = scare.GetComponentsInChildren<RectTransform>();
        float duration = 5f; // Total duration for the jump scare to move around
        float fadeDuration = 1f; // Duration to fade out

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            foreach (RectTransform child in children)
            {
                if (child != scare.transform)
                {
                    // Random movement within the canvas bounds
                    child.anchoredPosition = new Vector2(
                        Random.Range(-Screen.width / 2, Screen.width / 2),
                        Random.Range(-Screen.height / 2, Screen.height / 2)
                    );

                    // Random rotation around the Z axis
                    child.localRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
                }
            }
            yield return new WaitForSeconds(0.1f); // Move and rotate every 0.5 seconds

            elapsedTime += 0.5f;
        }

        // Disappearing effect
        CanvasGroup canvasGroup = scare.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = scare.AddComponent<CanvasGroup>();
        }

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - t / fadeDuration;
            yield return null;
        }

        scare.SetActive(false);
    }
}
