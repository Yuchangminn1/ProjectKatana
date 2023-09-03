using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Cinemachine.CinemachineOrbitalTransposer;

public class Dagger : MonoBehaviour
{
    private SpriteRenderer spr;
    public GameObject target; // 대상 (플레이어 등)
    public float speed = 5.0f; // 이동 속도
    private Vector2 direction; // 이동 방향
    private Rigidbody2D rb2D;

    private void Awake()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();

        //원래코드
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
        // 계산된 방향으로 프로젝타일 이동

        //추가한 코드
        if (gameObject.tag == "EnemyBullet")
            rb2D.velocity = direction * speed;

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌하면
        if (collision.tag == "Player")
        {
            //잠시 꺼둠
            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}