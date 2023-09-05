using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutObject : MonoBehaviour
{
    [SerializeField] int FadeNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(FadeNum == 0)
        {
            if(CMUIManager.Instance == null)
            {
                Debug.Log("매니저없음 ");
            }
            else 
                CMUIManager.Instance.StartFadeOut();
        }
        else if (FadeNum == 1)
        {
            if (CMUIManager.Instance == null)
            {
                Debug.Log("매니저없음 ");
            }
            else 
                CMUIManager.Instance.StartFadeIn();
        }
    }

    
}


