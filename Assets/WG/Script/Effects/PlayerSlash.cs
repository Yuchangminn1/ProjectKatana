using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    int Frame = 0;
    GameObject slashHitEffect;
    GameObject Hit_Clone;
    private void Awake()
    {
        //������Ʈ �����ֱ�� Awake, Start�� Update���� ���� �Ͼ�� ������
        //�� ��ũ��Ʈ���� ���� ������ �����Ҷ� ���� ���ϸ鼭 �����°� ������ �Ұ�����
        //InputManager���� ������ Update���� ���ŵǴ°� ���
        transform.rotation = Quaternion.Euler(0, 0, InputManager.instance.playerLookingCursorAngle);
        slashHitEffect = FXManager.instance.playerSlashHitEffect.slashHitEffect;
    }

    private void FixedUpdate()
    {
        //�÷��̾� AnimationTrigger���� �Ŵ��� �����ؼ� �������Ѻôµ�
        //���� ������ ���ÿ� ���Ҷ� ���� �ȵǴ� �����־ �׳� ���⼭ ��
        Frame++;



        //ȸ���Ҷ� �ܻ� �����Ϸ��� �� ������ �����ϰ�
        //����Ƽ���� slash �ִϸ��̼� �������� 12�����ӿ��� 10���������� �Ű���
        if (Frame >= 10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 closePoint = FindClosestPoint(collision.gameObject.transform.position,
                collision.bounds);

            Hit_Clone =
               Instantiate(slashHitEffect, collision.gameObject.transform.position,
               Quaternion.Euler(0, 0, InputManager.instance.playerLookingCursorAngle),
               FXManager.instance.transform);
        }
    }

    Vector2 FindClosestPoint(Vector2 point, Bounds bounds)
    {
        Vector2 closestPoint = point;

        closestPoint.x = Mathf.Clamp(closestPoint.x, bounds.min.x, bounds.max.x);
        closestPoint.y = Mathf.Clamp(closestPoint.y, bounds.min.y, bounds.max.y);

        return closestPoint;
    }
}
