using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerManager : MonoBehaviour
{
    public static WG_PlayerManager instance;

    //�ν����Ϳ��� �޾��ֱ�
    public WG_Player player;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
}
