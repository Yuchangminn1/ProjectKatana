using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrailTest : MonoBehaviour
{
    SpriteRenderer sr;
    Shader shader;
    public Color _color;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        shader = Shader.Find("GUI/Text Shader");

    }

    private void Update()
    {
        ColorSprite();
    }

    void ColorSprite()
    {
        sr.material.shader = shader;
        sr.color = _color;
    }

    public void ShadowLifeOver()
    {
        ObjectPool.instance.ObjectQueue.Enqueue(gameObject);
        gameObject.SetActive(false);
    }

    public void ShadowLifeOver_TimeControllable()
    {
        StartCoroutine(Shadow_Excute(GameObject.FindObjectOfType<GhostControl>().ShadowLifeTime));
    }

    IEnumerator Shadow_Excute(float Lifetime)
    {
        yield return new WaitForSeconds(Lifetime);
        ObjectPool.instance.ObjectQueue.Enqueue(gameObject);
        gameObject.SetActive(false);

    }
}
