using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_GhostTrailTest : MonoBehaviour
{
    SpriteRenderer sr;
    Shader shader;
    public Color _color;
    float currentTime;
    float ShadowLifeTime;
    float interpolatedValue = 0;
    float t;

    ShaderType shaderType;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        shaderType = WG_FXManager.instance.ghostControl.shaderType;

        //유니티 기본 쉐이더
        //이 쉐이더를 스프라이트에 덮어서 플레이어의 형상만나오고
        //색이 진하게 들어간 그림자로 나오는방식
        if (shaderType == ShaderType.Sprite_Default)
            shader = Shader.Find("GUI/Text Shader");

        if (shaderType == ShaderType.Sprite_URP_Lit)
            shader = Shader.Find("Universal Render Pipeline/Lit");

        if (shaderType == ShaderType.Custom)
        {
            if (WG_FXManager.instance.ghostControl.CustomShader != null)
                shader = WG_FXManager.instance.ghostControl.CustomShader;

            else
            {
                Debug.Log("쉐이더 지정되지않음");
                shaderType = ShaderType.Sprite_Default;
            }
        }

    }

    private void Update()
    {
        ColorSprite();
    }

    void ColorSprite()
    {
        currentTime += Time.deltaTime;
        t = currentTime / ShadowLifeTime;
        interpolatedValue = Mathf.Lerp(WG_FXManager.instance.ghostControl.MaxAlpha, WG_FXManager.instance.ghostControl.MinAlpha, t);

        if (WG_FXManager.instance.ghostControl.shaderType != ShaderType.NoShader)
            //material에 붙어있는 쉐이더 설정
            sr.material.shader = shader;


        sr.color = _color;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, interpolatedValue);
    }

    public void ShadowLifeOver()
    {
        WG_ObjectPool.instance.ObjectQueue.Enqueue(gameObject);
        gameObject.SetActive(false);
    }

    public void ShadowLifeOver_TimeControllable()
    {
        StopAllCoroutines();
        StartCoroutine(Shadow_Excute());
    }

    IEnumerator Shadow_Excute()
    {
        yield return new WaitForSeconds(ShadowLifeTime);
        WG_ObjectPool.instance.ObjectQueue.Enqueue(gameObject);
        gameObject.SetActive(false);
    }

    //오브젝트 풀 방식이니까 켤때 초기화
    private void OnEnable()
    {
        currentTime = 0f;
        ShadowLifeTime = WG_FXManager.instance.gameObject.GetComponent<WG_GhostControl>().ShadowLifeTime;
    }
}
