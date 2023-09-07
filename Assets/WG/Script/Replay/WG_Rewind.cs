using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WG_Rewind : MonoBehaviour
{
    public float MaxRewindDuration = 60f;
    public float rewindSpeed = 2f;
    public bool isRecordPaused = false;
    public bool isRewinding = false;

    List<TimeSnapShot> timeSnapShots = new List<TimeSnapShot>();

    WG_Player player;
    private void Start()
    {
        player = WG_PlayerManager.instance.player;
        isRewinding = false;
        timeSnapShots.Clear();
    }
    struct TimeSnapShot
    {
        public Vector3 position;
        public Quaternion rotation;
        public Sprite sprite;
        public bool isInputMouse0;
        public float attackAngle;
        //public int FacingDir;


        public TimeSnapShot(Vector3 position, Quaternion rotation, Sprite sprite,
            bool isInputMouse0, float attackAngle)
        {
            this.position = position;
            this.rotation = rotation;
            this.sprite = sprite;
            // this.FacingDir = FacingDir;
            this.isInputMouse0 = isInputMouse0;
            this.attackAngle = attackAngle;
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    StartRewind();
        //}
        //else if (Input.GetKeyUp(KeyCode.R))
        //{
        //    StopRewind();
        //}
    }

    private void LateUpdate()
    {

        if (!isRewinding && WG_GameEventManager.instance.isGameStarted && !isRecordPaused)
            RecordSnapShot();

        else if (isRewinding)
        {
            RewindTime();
        }
    }



    void RecordSnapShot()
    {
        timeSnapShots.Insert(0, new TimeSnapShot(player.transform.position, player.transform.rotation, player.spriteRenderer.sprite
            , player.isAttackForRewind, WG_InputManager.instance.playerLookingCursorAngle));

        //MaxRewindDuration 시간 초과하면 오래된 데이터부터 삭제
        if (timeSnapShots.Count > Mathf.Round(MaxRewindDuration / Time.fixedDeltaTime))
        {
            //List의 가장 마지막 데이터(가장 오래된거)
            timeSnapShots.RemoveAt(timeSnapShots.Count - 1);
        }
    }

    public void PauseRecord()
    {
        isRecordPaused = true;
    }

    void RewindTime()
    {
        for (int i = 0; i < rewindSpeed; i++)
        {
            //데이터 존재하면
            if (timeSnapShots.Count > 0)
            {
                TimeSnapShot snapShot = timeSnapShots[0];
                player.transform.position = snapShot.position;
                player.transform.rotation = snapShot.rotation;
                player.spriteRenderer.sprite = snapShot.sprite;
                //  player.FacingDir = snapShot.FacingDir;

                if (snapShot.isInputMouse0)
                {
                    WG_FXManager.instance.playerSlashEffect.CreateSlashEffect();
                    WG_FXManager.instance.playerSlashEffect.Instant_slashEffect.transform.rotation
                        = Quaternion.Euler(0, 0, snapShot.attackAngle);
                }

                timeSnapShots.RemoveAt(0);
            }
            else
            {
                StopRewind();
                break;
            }
        }

    }

    public void StartRewind()
    {
        WG_FXManager.instance.screenEffect.RetroEffectON();
        WG_SoundManager.instance.BGM_Player.pitch = WG_SoundManager.instance.BGM_Player_RewindPith;

        isRewinding = true;
    }

    public void StopRewind()
    {
        WG_FXManager.instance.screenEffect.RetroEffectOff();
        isRewinding = false;
        isRecordPaused = false;

        //마지막에 일어서서 씬 다시 로드
        player.stateMachine.ChangeState(player.idleState);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
