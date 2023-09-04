using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBlink : MonoBehaviour
{
    [SerializeField] protected Light2D light;
    [SerializeField] protected float setR;
    [SerializeField] protected float setG;
    [SerializeField] protected float setB;
    [SerializeField] protected float getintensity;
    protected virtual void Start()
    {
        light = GetComponent<Light2D>();
        getintensity = light.intensity;

        StartCoroutine(linkRed());
    }

    
    
    IEnumerator linkRed()
    {
        light.color = new Color(1, 0.16f, 0.16f);
        setR = 1f;
        setG = 0.16f;
        setB = 0.16f;
        while(true)
        {
            int count = 0;
            while (setR > 0.16f)
            {
                if(count == 2)
                {
                    yield return null;
                    count = 0;
                }
                
                setR -= Time.deltaTime;
                light.intensity = setR * getintensity;
                light.color = new Color(setR, setG, setB);
                ++count;

            }
            count = 0;
            while (setR < 1f)
            {
                if (count == 2)
                {
                    yield return null;
                    count = 0;
                }
                ++count;
                setR += Time.deltaTime;
                light.intensity = setR * getintensity;
                light.color = new Color(setR, setG, setB);


            }
            yield return null;
        }
        

    }
}
