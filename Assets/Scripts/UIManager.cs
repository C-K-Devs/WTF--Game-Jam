using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public TMP_Text subtitle;
    private float subtitleDuration = 5;
    // Start is called before the first frame update

    void Awake(){
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSubtitle(string sub, float duration)
    {
        StopShowSubtitle();
        subtitle.text = sub;
        subtitleDuration = duration;
        StartCoroutine(SubtitleCoroutine(subtitleDuration));
    }

    private IEnumerator SubtitleCoroutine(float duration)
    {
        subtitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        subtitle.gameObject.SetActive(false);
    }

    public void StopShowSubtitle(){
        StopCoroutine(SubtitleCoroutine(subtitleDuration));
        subtitle.gameObject.SetActive(false);
    }
}
