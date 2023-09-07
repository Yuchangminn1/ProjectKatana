using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCol : MonoBehaviour
{
    [SerializeField] Collider2D lasercol;
    void Start()
    {
        lasercol = transform.GetComponent<Collider2D>();
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Debug.Log("플레이어 사망");
    }*/
}
