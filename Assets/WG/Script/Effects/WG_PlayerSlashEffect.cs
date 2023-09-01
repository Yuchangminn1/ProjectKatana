using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerSlashEffect : WG_Effects
{
    [Header("Slash Effect Info")]
    [SerializeField] protected GameObject playerSlashEffect;
    [SerializeField] protected Transform playerSlashEffectTransform;
    [SerializeField] public float HitInterval = 0.3f;
    public GameObject Instant_slashEffect;

    [Header("Hit TimeStop Info")]
    [SerializeField] public float timeStopDuration = 0.05f;
    [SerializeField] float timer = 0f;
    [SerializeField] float currentTimeSclae;

    [Header("Eneymy Hit Info")]
    public List<GameObject> alreadyHitEnemy = new List<GameObject>();
    public GameObject ClosestEnemyInList;

    [Header("BulletParry Info")]
    [SerializeField] public float ParryToShootSpeed = 10f;

    private void Start()
    {
        currentTimeSclae = Time.timeScale;
    }
    private void Update()
    {
        //FixedUpate은 유니티 엔진 기반이므로 TimeScale 0되면 연산이 멈춰버림
        timer += Time.unscaledDeltaTime;

        //Update만 TimeScale 0일때도 실행되니 따로 메소드로 빼주지않고 여기다가 넣어줌
        //시간 초기화
        if (timer >= timeStopDuration && !WG_PlayerManager.instance.player.isBulletTime
            && !WG_PlayerManager.instance.player.Pause)
            Time.timeScale = currentTimeSclae;
    }
    private void FixedUpdate()
    {

    }
    public void CreateSlashEffect()
    {
        Instant_slashEffect = Instantiate(playerSlashEffect, playerSlashEffectTransform.position,
            Quaternion.identity, playerSlashEffectTransform);
    }

    public void TimeStop()
    {
        timer = 0f;
        Time.timeScale = 0;
    }
}
