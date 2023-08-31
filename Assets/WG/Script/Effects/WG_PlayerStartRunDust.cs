using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerStartRunDust : WG_Effects
{
    [SerializeField] protected ParticleSystem playerStartRunDust;
    [SerializeField] protected Transform Emitposition_playerStartRunDust;

    public ParticleSystem go;
    private void Start()
    {
    }
    public void playerStartRunDustEmit()
    {
        go = Instantiate(playerStartRunDust, Emitposition_playerStartRunDust.position, Quaternion.identity, WG_FXManager.instance.transform);
        Destroy(go.gameObject, 0.5f);
    }
}
