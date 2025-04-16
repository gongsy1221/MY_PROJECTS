using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light lightSource;
    private float minIntensity = 0.0f;
    private float maxIntensity = 0.82f;
    private float flickerSpeed = 3.0f;

    void Awake()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();
        }
    }

    void Update()
    {
        lightSource.intensity = Mathf.PingPong(Time.time * flickerSpeed, maxIntensity - minIntensity) + minIntensity;
    }
}
