using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_Managers : MonoBehaviour
{
    protected virtual void Awake()
    {
        //Ư�� ������Ʈ�� �ڽĻ��¸� DontDestroyOnLoad �۵�����
        //transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}
