using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerJumpAndTumblingDustEffect : MonoBehaviour
{
    public GameObject jumpdust, tumblingdust;
    public Transform jumpdust_transform, tumblingdust_transform;
    private void Awake()
    {
        jumpdust.SetActive(false);
        tumblingdust.SetActive(false);
    }
    public void PlayJumpDust()
    {
        jumpdust.SetActive(true);
        jumpdust.transform.position = jumpdust_transform.position;
    }
    public void PlayTumblingDust()
    {
        tumblingdust.SetActive(true);
        tumblingdust.transform.position = tumblingdust_transform.position;
        tumblingdust.transform.rotation = Quaternion.Euler(0,
            WG_PlayerManager.instance.player.FacingDir == 1 ? 0 : 180, 0);
    }

    public void InstantiateTumblingDust()
    {
        GameObject go = Instantiate(tumblingdust, tumblingdust_transform.position,
            Quaternion.Euler(0, WG_PlayerManager.instance.player.FacingDir == 1 ? 0 : 180, 0), transform);
        Destroy(go, 1f);
    }
}
