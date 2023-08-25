using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    int Frame = 0;
    private void Awake()
    {
        //������Ʈ �����ֱ�� Awake, Start�� Update���� ���� �Ͼ�� ������
        //�� ��ũ��Ʈ���� ���� ������ �����Ҷ� ���� ���ϸ鼭 �����°� ������ �Ұ�����
        //InputManager���� ������ Update���� ���ŵǴ°� ���
        transform.rotation = Quaternion.Euler(0, 0, InputManager.instance.playerLookingCursorAngle);
    }

    private void FixedUpdate()
    {
        //�÷��̾� AnimationTrigger���� �Ŵ��� �����ؼ� �������Ѻôµ�
        //���� ������ ���ÿ� ���Ҷ� ���� �ȵǴ� �����־ �׳� ���⼭ ��
        Frame++;

        //ȸ���Ҷ� �ܻ� �����Ϸ��� �� ������ �����ϰ�
        //����Ƽ���� slash �ִϸ��̼� �������� 12�����ӿ��� 10���������� �Ű���
        if (Frame >= 10) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
