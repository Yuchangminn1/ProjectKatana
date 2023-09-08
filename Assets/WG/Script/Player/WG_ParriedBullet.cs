using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_ParriedBullet : WG_PlayerSlash
{
    Rigidbody2D rb;
    Vector2 parryDir;
    float parryAngle;
    bool isHit;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;


        isHit = false;

        //BulletParentsData�� ������ �Ѿ� ���� �� �� �θ� Object�� �ٶ󺸴� ���Ⱚ
        parryDir = (GetComponent<WG_BulletParentsData>().Parent.transform.position - transform.position).normalized;
        parryAngle = Mathf.Atan2(parryDir.y, parryDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, parryAngle);

        rb.velocity = parryDir * WG_FXManager.instance.playerSlashEffect.ParryToShootSpeed;

        WG_SoundManager.instance.PlayEffectSound("Sound_Player_Reflect");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isHit)
        {
            isHit = true;

            GameObject Hit_Clone
            = Instantiate(slashHitEffect,
            collision.gameObject.transform.position,
            Quaternion.Euler(0, 0, parryAngle), WG_FXManager.instance.transform);

            CameraShakeEffect();

            //�ϴ� �ӽ�
            Destroy(gameObject);
        }
    }
}
