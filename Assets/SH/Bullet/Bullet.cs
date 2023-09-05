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
    }


    private void FixedUpdate()
    {
        // ���� �������� ������Ÿ�� �̵�
        

        if(gameObject.tag == "EnemyBullet") //WG �߰��� �ڵ�
        rb2D.velocity = direction * speed;

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹�ϸ�
        if (collision.tag == "Player")
        {
            //��� ����
            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
