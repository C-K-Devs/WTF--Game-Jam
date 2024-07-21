using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float minFlickerTime = 0.1f;
    public float maxFlickerTime = 0.5f;

    private Light lightComp;

    void Start()
    {
        lightComp = GetComponent<Light>();
        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minFlickerTime, maxFlickerTime);
            float targetIntensity = Mathf.Clamp(lightComp.intensity + Random.Range(-15, 15), 0, 20);

            float elapsedTime = 0f;
            float initialIntensity = lightComp.intensity;

            while (elapsedTime < waitTime)
            {
                lightComp.intensity = Mathf.Lerp(initialIntensity, targetIntensity, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            lightComp.intensity = targetIntensity;  // Ensure the final value is set
            yield return new WaitForSeconds(waitTime);
        }
    }
}