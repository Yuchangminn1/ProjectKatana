using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

using UnityEngine.UI;

public class CMUIManager : MonoBehaviour
{
    private static CMUIManager instance;
    public static CMUIManager Instance;

    // Next Stage Icon
    [Header("Next Stage Icon")]
    [SerializeField] GameObject nextIcon;
    [SerializeField] Vector2 nextIconTOrigin;
    [SerializeField] float nextIconMoveSpeed = 0.1f;
    [SerializeField] bool isNextIconOn = false;


    [Header("FadeIn out ")]
    public Image fadeImage = null;
    [SerializeField] float fadeTime = 2f;

    [SerializeField] bool isFading = false;
    


    //WG�ڵ��߰�
    public CMPlayerBlinkUI cmPlayerBlinkUI;



    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Instance = instance;

        }
        else
            Destroy(instance.gameObject);

    }

    private void Start()
    {
        cmPlayerBlinkUI = GetComponent<CMPlayerBlinkUI>();
        Debug.Log(WG_StageManager.instance.EnemyAllDead());

    }

    private void Update()
    {
        if (isNextIconOn)
        {
            if (WG_PlayerManager.instance.player.isDead)
            {
                NextStageIcon(false);

            }
            return;
        }
        else if (WG_StageManager.instance.EnemyAllDead())
        {
            NextStageIcon();
        }
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

    private void NextStageIcon()
    {
        nextIcon.SetActive(true);
        isNextIconOn = true;
    }
    private void NextStageIcon(bool _isOn)
    {
        nextIcon.SetActive(_isOn);
    }
}
