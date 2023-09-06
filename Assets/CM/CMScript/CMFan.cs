using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMFan : MonoBehaviour
{
    SpriteRenderer sr;
    private void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();

    }
    void RedFan() 
    {
        sr.color = Color.red;
    }
    void NomalFan()
    {
        sr.color = Color.white;
    }
}
