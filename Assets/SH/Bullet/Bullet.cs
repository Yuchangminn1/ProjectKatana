using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer spr;
    public GameObject target; // 대상 (플레이어 등)
    public float speed = 5.0f; // 이동 속도
    private Vector2 direction; // 이동 방향
    private Rigidbody2D rb2D;

    //WG 추가
    float Temp_Speed;
    float start_Speed;
    private void Awake()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        Temp_Speed = speed;
        start_Speed = speed;
    }

    private void Start()
    {
        WG_SoundManager.instance.PlayEffectSound("Sound_Enemy_Fire");
    }

    private void FixedUpdate()
    {
        // 계산된 방향으로 프로젝타일 이동


        if (gameObject.tag == "EnemyBullet") //WG 추가한 코드
        {
            if (WG_PlayerManager.instance.player.isBulletTime)
                speed = Temp_Speed * 0.5f;
            else
                speed = start_Speed;

            rb2D.velocity = direction * speed;
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 5f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌하면
        if (collision.tag == "Player" || collision.tag == "Door" || collision.tag == "Ground")
        {

            Destroy(gameObject);
        }
    }
}
