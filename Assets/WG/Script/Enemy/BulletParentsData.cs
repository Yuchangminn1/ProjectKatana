using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParentsData : MonoBehaviour
{
    [SerializeField] public GameObject Parent;
    private void Awake()
    {
        if (Parent != null)
            Parent = transform.parent.gameObject;
    }
}
