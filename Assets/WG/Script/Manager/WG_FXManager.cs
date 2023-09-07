using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_FXManager : WG_Managers
{
    public static WG_FXManager instance;
    public WG_Effects effects { get; private set; }
    public WG_PlayerStartRunDust playerStartRun { get; private set; }
    public WG_PlayerSlashEffect playerSlashEffect { get; private set; }
    public WG_PlayerSlashHitEffect playerSlashHitEffect { get; private set; }
    public WG_GhostControl ghostControl { get; private set; }
    public WG_CameraEffect cameraEffect { get; private set; }
    public WG_ScreenEffect screenEffect { get; private set; }
    public WG_PlayerJumpAndTumblingDustEffect jumpAndtumblingDustEffect { get; private set; }
    protected override void Awake()
    {
        if (instance != null) 
            Destroy(instance.gameObject);

        else 
            instance = this;

        base.Awake();
    }

    private void Start()
    {
        effects = GetComponent<WG_Effects>();
        playerStartRun = GetComponent<WG_PlayerStartRunDust>();
        playerSlashEffect = GetComponent<WG_PlayerSlashEffect>();
        playerSlashHitEffect = GetComponent<WG_PlayerSlashHitEffect>();
        ghostControl = GetComponent<WG_GhostControl>();
        cameraEffect = GetComponent<WG_CameraEffect>();
        screenEffect = GetComponent<WG_ScreenEffect>();
        jumpAndtumblingDustEffect = GetComponent<WG_PlayerJumpAndTumblingDustEffect>();

    }
}
