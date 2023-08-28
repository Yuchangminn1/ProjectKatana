using UnityEngine;

public class Dagger : MonoBehaviour
{
    public GameObject target; // ��� (�÷��̾� ��)
    public float speed = 5.0f; // �̵� �ӵ�
    private Vector3 direction; // �̵� ����

    private void Start()
    {
        if (target != null)
        {
            // ����� ���� ���� ���͸� ���
            direction = (target.transform.position - transform.position).normalized;

            // ������ ���� ���
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ������Ʈ ȸ��
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Update()
    {
        // ���� �������� ������Ÿ�� �̵�
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   // �÷��̾�� �浹�ϸ�
        if (collision.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }

    }

}

