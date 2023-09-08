using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer spr;
    public GameObject target; // ��� (�÷��̾� ��)
    public float speed = 5.0f; // �̵� �ӵ�
    private Vector2 direction; // �̵� ����
    private Rigidbody2D rb2D;

    //WG �߰�
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
        // ���� �������� ������Ÿ�� �̵�


        if (gameObject.tag == "EnemyBullet") //WG �߰��� �ڵ�
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
        // �÷��̾�� �浹�ϸ�
        if (collision.tag == "Player" || collision.tag == "Door" || collision.tag == "Ground")
        {

            Destroy(gameObject);
        }
    }
}
