using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControl : MonoBehaviour
{
    public GameObject Shadow;
    public GameObject Player;
    float timer;
    public float ShadowSpawnSpeed;
    public float ShadowLifeTime = 0.3f;
    public Color _color;
    Animator anim;

    private void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }
    public void Shadows_Skill()
    {
        timer += ShadowSpawnSpeed * Time.deltaTime;

        if (timer >= ShadowLifeTime)
        {
            timer = 0;

            GameObject clone_Shadow = ObjectPool.instance.ObjectQueue.Dequeue();
            clone_Shadow.transform.position = Player.transform.position;
            clone_Shadow.transform.rotation = Player.transform.rotation;
            clone_Shadow.SetActive(true);

            clone_Shadow.GetComponent<SpriteRenderer>().sprite =
                Player.GetComponentInChildren<SpriteRenderer>().sprite;

            clone_Shadow.GetComponent<GhostTrailTest>()._color = _color;

        }
    }
}
