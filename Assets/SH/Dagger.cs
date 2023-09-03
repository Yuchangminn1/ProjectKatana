using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Cinemachine.CinemachineOrbitalTransposer;

public class Dagger : MonoBehaviour
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

        //�����ڵ�
        //target = GameObject.FindGameObjectWithTag("Player");

        target = GameObject.Find("Player");

        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Start()
    {

        if (target.transform.position.x < transform.position.x)
        {
            spr.flipX = false;
        }
        else
            spr.flipX = true;
    }

    private void FixedUpdate()
    {
        // ���� �������� ������Ÿ�� �̵�

        //�߰��� �ڵ�
        if (gameObject.tag == "EnemyBullet")
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