using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneCol : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(collision.gameObject.tag == "Player")
        {
            if (WG_StageManager.instance.EnemyAllDead())
            {
                CMSceneManager.instance.CMNextScene(nextSceneName);

            }

        }
        
    }
}
