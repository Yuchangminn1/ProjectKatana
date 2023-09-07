using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�̺�Ʈ ����
//Ŭ�������� �ƴ��� ������ �Ǻ�
public class WG_GameEventManager : WG_Managers
{
    public static WG_GameEventManager instance;

    //���࿡ ���� �����ϰ� ������� �ڿ����� �������� ���Ÿ� �ڵ� �����ؾ���
    public bool isGameStarted = false;
    public bool isGameFinished = false;

    protected override void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        else
            instance = this;

        base.Awake();

        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        isGameStarted = true;
        isGameFinished = false;

        WG_SoundManager.instance.PlayBGM("BGM_Bunker");
    }


    private void Update()
    {
    }


    public event Action onGoalReached;
    public void GoalReached()
    {
        if (onGoalReached != null)
            onGoalReached();
    }

    public event Action onRestartLevel;
    public void RestartLevel()
    {
        if (onRestartLevel != null)
            onRestartLevel();
    }

    //public event Action<GameObject> onChangeCameraTarget;
    //public void ChangeCameraTarget(GameObject newTarget)
    //{
    //    if (onChangeCameraTarget != null)
    //        onChangeCameraTarget(newTarget);
    //}

    public event Action onPlayerRespawn;
    public void PlayerRespawn()
    {
        if (onPlayerRespawn != null)
            onPlayerRespawn();
    }
}
