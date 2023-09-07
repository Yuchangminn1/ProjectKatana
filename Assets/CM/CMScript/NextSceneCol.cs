using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneCol : MonoBehaviour
{
    [SerializeField] string nextSceneName = "Stage2";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (WG_StageManager.instance.EnemyAllDead())
            {
                CMSceneManager.instance.CMNextScene(nextSceneName);

            }

        }
        
    }
}
