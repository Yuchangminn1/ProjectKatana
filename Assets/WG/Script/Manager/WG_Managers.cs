using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_Managers : MonoBehaviour
{
    protected virtual void Awake()
    {
        //특정 오브젝트의 자식상태면 DontDestroyOnLoad 작동안함
        //transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}
