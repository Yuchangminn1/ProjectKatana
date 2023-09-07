using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerManager : WG_Managers
{
    public static WG_PlayerManager instance;

    //인스펙터에서 달아주기
    public WG_Player player;

    protected override void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        else
            instance = this;

        base.Awake();
    }
}
