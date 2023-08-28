using UnityEngine;

public class Dagger : MonoBehaviour
{
    public GameObject target; // 대상 (플레이어 등)
    public float speed = 5.0f; // 이동 속도
    private Vector3 direction; // 이동 방향

    private void Start()
    {
        if (target != null)
        {
            // 대상을 향한 방향 벡터를 계산
            direction = (target.transform.position - transform.position).normalized;

            // 방향의 각도 계산
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 오브젝트 회전
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Update()
    {
        // 계산된 방향으로 프로젝타일 이동
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   // 플레이어와 충돌하면
        if (collision.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }

    }

}

