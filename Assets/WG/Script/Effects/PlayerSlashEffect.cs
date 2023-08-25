using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashEffect : Effects
{
    [SerializeField] protected GameObject playerSlashEffect;
    [SerializeField] protected Transform playerSlashEffectTransform;
    public GameObject Instant_slashEffect;
    public void CreateSlashEffect()
    {
        Instant_slashEffect = Instantiate(playerSlashEffect, playerSlashEffectTransform.position, Quaternion.identity, playerSlashEffectTransform);
    }
}
