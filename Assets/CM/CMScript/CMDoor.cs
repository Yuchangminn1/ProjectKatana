using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMDoor : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    Animator anima;
    private void Start()
    {
        anima = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<BoxCollider2D>().enabled = false;
                anima.SetBool("Open", true);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = true;

        }
    }

}
