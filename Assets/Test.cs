using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float angle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        angle = Mathf.Atan2(InputManager.instance.cursorDir.y, InputManager.instance.cursorDir.x) * Mathf.Rad2Deg;
    }
}

