using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMFadeInOut : MonoBehaviour
{
    [Header("FadeIn out ")]
    public Image fadeImage = null;
    [SerializeField] float fadeTime = 2f;

    [SerializeField] bool isFading = false;
    void Start()
    {
        StartFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFadeIn()
    {
        if (!isFading)
        {
            StartCoroutine(Fade(1.0f, 0.0f));
        }
    }

    // ���̵�ƿ� ȿ���� �����մϴ�.
    public void StartFadeOut()
    {
        if (!isFading)
        {
            StartCoroutine(Fade(0.0f, 1.0f));
        }
    }


    private IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        isFading = true;

        Color currentColor = fadeImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            // �ð��� ���� ���� ���� �����Ͽ� �����մϴ�.
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeTime);
            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���̵� �Ϸ� �� ���¸� �����մϴ�.
        fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
        isFading = false;
    }
}
