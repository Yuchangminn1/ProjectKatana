using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMSceneManager : MonoBehaviour
{
    [SerializeField] RectTransform titleUI;
    [SerializeField] float titleUpSpeed = -0.5f;


    // Start is called before the first frame update
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
        StartCoroutine(CMLoadNextScene("CMTest"));
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
