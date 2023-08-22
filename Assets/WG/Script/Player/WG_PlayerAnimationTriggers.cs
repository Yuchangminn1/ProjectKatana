using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerAnimationTriggers : MonoBehaviour
{
    WG_Player player => GetComponentInParent<WG_Player>();
    void AnimationFinishTrigger()
    {
        player.AnimationFinishTrigger();
    }

    void playerStartRunDustEmit()
    {
        FXManager.instance.playerStartRun.playerStartRunDustEmit();
    }

    void playerStartRunDustStop()
    {
        FXManager.instance.playerStartRun.playerStartRunDustStop();
    }
}
