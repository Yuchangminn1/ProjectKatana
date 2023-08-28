using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrailTest : MonoBehaviour
{
    SpriteRenderer sr;
    Shader shader;
    public Color _color;
    float currentTime;
    float ShadowLifeTime;
    float interpolatedValue;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        shader = Shader.Find("GUI/Text Shader");
        currentTime = 0f;
        ShadowLifeTime = FindObjectOfType<GhostControl>().ShadowLifeTime;
    }

    private void Update()
    {
        ColorSprite();


        if (currentTime < ShadowLifeTime)
        {
            currentTime += Time.deltaTime;

            float t = currentTime / ShadowLifeTime;

            interpolatedValue = Mathf.Lerp(255, 0, t);
        }

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, interpolatedValue);
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
        StartCoroutine(Shadow_Excute(ShadowLifeTime));
    }

    IEnumerator Shadow_Excute(float Lifetime)
    {
        yield return new WaitForSeconds(Lifetime);
        ObjectPool.instance.ObjectQueue.Enqueue(gameObject);
        gameObject.SetActive(false);

    }
}
