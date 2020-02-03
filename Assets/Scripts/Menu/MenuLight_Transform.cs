using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLight_Transform : MonoBehaviour
{
    private new Light light;
    bool increasingIntensity = true;
    
    void Start()
    {
        light = GetComponent<Light>();
        light.color = Color.yellow;
    }

    void Update()
    {

        if (increasingIntensity)
        {
            if (light.intensity > 2.0F) increasingIntensity = false;
            light.intensity += 0.003F;
            
        }
       else 
        {
            if (light.intensity < 0.7F) increasingIntensity = true;
            light.intensity -= 0.003F;
        }
    }
}
