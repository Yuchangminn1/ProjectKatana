using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleUI : MonoBehaviour
{
    
    [Header("FadeIn out ")]
    public Image fadeImage = null;
    [SerializeField] float fadeTime = 2f;

    [SerializeField] bool isFading = false;

    // Start is called before the first frame update
    
    public void StartFadeIn()
    {
        if (!isFading)
        {
            StartCoroutine(Fade(1.0f, 0.0f));
        }
    }

    // 페이드아웃 효과를 시작합니다.
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
            // 시간에 따라 알파 값을 보간하여 변경합니다.
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeTime);
            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 페이드 완료 후 상태를 리셋합니다.
        fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
        isFading = false;
    }

    
}
