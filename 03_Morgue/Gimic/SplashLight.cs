using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLight : MonoBehaviour
{
    private Light theLight;
    private float targetIntensity;
    private float currentIntensity;

    void Start()
    {
        theLight = GetComponent<Light>();
        currentIntensity = theLight.intensity;
        targetIntensity = Random.Range(0f, 0.8f);
    }

    void Update()
    {   
        if (Mathf.Abs(targetIntensity - currentIntensity) >= 0.01)
        {
            if (targetIntensity - currentIntensity >= 0)
            {   
                currentIntensity += Time.deltaTime;
            }
            else
            {   
                currentIntensity -= Time.deltaTime;
            }
            theLight.intensity = currentIntensity;
            theLight.range = currentIntensity + 10;
        }
        else
        {
            targetIntensity = Random.Range(0f, 0.8f);
        }
    }
}
