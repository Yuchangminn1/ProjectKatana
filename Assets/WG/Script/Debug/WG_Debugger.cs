using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WG_Debugger : MonoBehaviour
{
    float t, a, currentTime;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        t = currentTime / duration;
        a = Mathf.Lerp(255, 0, t);

        if (currentTime > duration)
        {

        }
        Debug.Log(a);
    }
}
