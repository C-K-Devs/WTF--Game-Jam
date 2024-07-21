using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text subtitle;
    public bool isSubtitleActive = false;
    private Coroutine currentSubtitleCoroutine;
    public List<GameObject> papers;
    public GameObject finalPaper;

    void Awake()
    {
        instance = this;
    }

    public void ShowSubtitle(string sub, float duration, bool force)
    {
        if (force)
        {
            if (currentSubtitleCoroutine != null)
            {
                StopCoroutine(currentSubtitleCoroutine);
            }
            currentSubtitleCoroutine = StartCoroutine(SubtitleCoroutine(sub, duration));
        }
        else
        {
            if (!isSubtitleActive)
            {
                currentSubtitleCoroutine = StartCoroutine(SubtitleCoroutine(sub, duration));
            }
        }
    }

    private IEnumerator SubtitleCoroutine(string sub, float duration)
    {
        isSubtitleActive = true;
        subtitle.text = sub;
        subtitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        subtitle.gameObject.SetActive(false);
        isSubtitleActive = false;
    }

    public void StopShowSubtitle()
    {
        if (currentSubtitleCoroutine != null)
        {
            StopCoroutine(currentSubtitleCoroutine);
            currentSubtitleCoroutine = null;
        }
        subtitle.gameObject.SetActive(false);
        isSubtitleActive = false;
    }
}
