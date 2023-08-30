using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashEffect : Effects
{
    [SerializeField] protected GameObject playerSlashEffect;
    [SerializeField] protected Transform playerSlashEffectTransform;
    [SerializeField] public float HitInterval = 0.3f;
    public GameObject Instant_slashEffect;


    public List<GameObject> alreadyHitEnemy = new List<GameObject>();
    public GameObject ClosestEnemyInList;
    public void CreateSlashEffect()
    {
        Instant_slashEffect = Instantiate(playerSlashEffect, playerSlashEffectTransform.position,
            Quaternion.identity, playerSlashEffectTransform);
    }
}
