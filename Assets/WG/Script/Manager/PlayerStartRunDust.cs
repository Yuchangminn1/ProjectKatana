using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartRunDust : Effects
{
    [Header("Effects")]
    [SerializeField] protected ParticleSystem playerStartRunDust;
    [SerializeField] Transform Emitposition_playerStartRunDust;

    public void playerStartRunDustEmit()
    {
        playerStartRunDust.transform.position = Emitposition_playerStartRunDust.position;
        playerStartRunDust.maxParticles = Random.Range(4, 8);
        playerStartRunDust.gameObject.SetActive(true);
        playerStartRunDust.Play();

        Invoke("playerStartRunDustStop", 1f);
    }

    public void playerStartRunDustStop()
    {
        playerStartRunDust.Stop();
        playerStartRunDust.gameObject.SetActive(false);
    }
}
