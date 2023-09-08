using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMDoor : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] BoxCollider2D box;
    Animator anima;

    private void Start()
    {
        anima = GetComponent<Animator>();

    }
    private void Update()
    {
        if (WG_PlayerManager.instance.player.isDead)
        {
            return;
        }
        if (isPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                box.GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<EdgeCollider2D>().enabled = false;

                anima.SetBool("Open", true);

                WG_SoundManager.instance.PlayEffectSound("Sound_Door");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = true;

        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayer = false;

        }


    }

}
