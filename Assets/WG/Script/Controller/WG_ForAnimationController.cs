using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//애니메이터 있는 오브젝트에 달아서 사용
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
