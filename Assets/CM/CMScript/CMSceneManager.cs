using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMSceneManager : MonoBehaviour
{
    public static CMSceneManager instance;
    [SerializeField] RectTransform titleUI;
    [SerializeField] float titleUpSpeed = -0.5f;


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CMStartGame()
    {
        Debug.Log("클릭했음");
        StartCoroutine(CMLoadNextScene("Stage1"));
    }
    public void CMNextScene(string _nextSceneName = "Stage2")
    {
        Debug.Log("클릭했음");
        SceneManager.LoadScene(_nextSceneName);
    }


    IEnumerator CMLoadNextScene(string SceneName)
    {
        float i = 0;

        CMUIManager.Instance.StartFadeOut();
        while (i < 180)
        {
            TitleSceneMove();
            yield return new WaitForFixedUpdate();
            titleUpSpeed +=  + 0.0002f * i;
            i++;
        }
        
        SceneManager.LoadScene(SceneName);
        yield return null;


    }

    void TitleSceneMove()
    {
        Vector2 tmp = new Vector2 (titleUI.position.x, titleUI.position.y + titleUpSpeed);
        titleUI.position = tmp;
        titleUpSpeed += 0.00001f;
        
    }
}
