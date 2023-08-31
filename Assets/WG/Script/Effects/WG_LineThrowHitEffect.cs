using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WG_LineThrowHitEffect : MonoBehaviour
{
    public Transform StartPoint, EndPoint;
    public GameObject EffectBullet;
    public float LifeTime = 0.3f;
    TrailRenderer trr;
    GameObject go;
    Vector2 tempV;
    public float speed = 50f;
    private void Start()
    {
        //발사체 생성
        go = Instantiate(EffectBullet, StartPoint.position, Quaternion.identity);

        //플레이어 칼질 방향과 같게 만들어줌
        go.transform.rotation = transform.rotation;

        //StartPoint에서 EndPoint를 바라보는 방향값
        tempV = (EndPoint.position - StartPoint.position).normalized;

        trr = go.GetComponent<TrailRenderer>();
        trr.time = LifeTime;

        Destroy(go, LifeTime);
        Destroy(gameObject.transform.parent.gameObject, LifeTime);
    }

    private void Update()
    {
        if (go != null)
        {
            go.GetComponent<Rigidbody2D>().velocity
               = tempV * speed;
        }
    }
}
