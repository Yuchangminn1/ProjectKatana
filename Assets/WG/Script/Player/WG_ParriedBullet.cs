using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_ParriedBullet : WG_PlayerSlash
{
    Rigidbody2D rb;
    Vector2 parryDir;
    float parryAngle;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        //BulletParentsData에 저장한 총알 생성 될 때 부모 Object를 바라보는 방향값
        parryDir = (GetComponent<WG_BulletParentsData>().Parent.transform.position - transform.position).normalized;

        rb.velocity = parryDir * WG_FXManager.instance.playerSlashEffect.ParryToShootSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            parryAngle = Mathf.Atan2(parryDir.y, parryDir.x) * Mathf.Rad2Deg;

            GameObject Hit_Clone
            = Instantiate(slashHitEffect,
            collision.gameObject.transform.position,
            Quaternion.Euler(0, 0, parryAngle), WG_FXManager.instance.transform);

            CameraShakeEffect();


            //일단 임시
            Destroy(gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
