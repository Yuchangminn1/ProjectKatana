using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ִϸ����� �ִ� ������Ʈ�� �޾Ƽ� ���
public class WG_ForAnimationController : MonoBehaviour
{
    void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
