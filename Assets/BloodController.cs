using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        Destroy(gameObject, 0.5f);
        rb.AddForce(Vector2.up * 600);   

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
