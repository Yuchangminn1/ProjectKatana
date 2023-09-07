using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_StageManager : WG_Managers
{
    public static WG_StageManager instance;
    [SerializeField] GameObject[] Enemies;

    //CM 코드추가
    [SerializeField] bool isEnemiesAllDead = false;


    protected override void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        else
            instance = this;

        base.Awake();
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (Enemies.Length == 0)
            isEnemiesAllDead = true;


    }

    //CM 코드추가
    public bool EnemyAllDead()
    {
        return isEnemiesAllDead;
    }
}