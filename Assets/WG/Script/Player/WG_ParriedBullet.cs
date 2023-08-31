using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_ParriedBullet : PlayerSlash
{
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Hit_Clone
            = Instantiate(slashHitEffect,
            collision.gameObject.transform.position,
            Quaternion.identity, FXManager.instance.transform);

            Hit_Clone.transform.rotation = Quaternion.Euler(0, 0, parryAngle);
            CameraShakeEffect();

            Destroy(gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    protected override void OntriggerExcute()
    {
    }
}
