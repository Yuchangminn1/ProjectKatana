using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class LineThrowHitEffect : MonoBehaviour
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

        go = Instantiate(EffectBullet, StartPoint.position, Quaternion.identity);
        go.transform.rotation = transform.rotation;

        tempV = InputManager.instance.cursorDir * speed;
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
               = tempV;

        }
    }
}
