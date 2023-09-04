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
        public Vector3 velocity;
        public Vector3 position;
        public Quaternion rotation;
        public Sprite sprite;
        public int FacingDir;
        public bool Input_Mouse0;

        public TimeSnapShot(Vector3 velocity, Vector3 position, Quaternion rotation, Sprite sprite, int FacingDir, bool Input_Mouse0)
        {
            this.velocity = velocity;
            this.position = position;
            this.rotation = rotation;
            this.sprite = sprite;
            this.FacingDir = FacingDir;
            this.Input_Mouse0 = Input_Mouse0;
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
        timeSnapShots.Insert(0, new TimeSnapShot(player.rb.velocity, player.transform.position, player.transform.rotation, player.spriteRenderer.sprite
            , player.FacingDir, Input.GetKeyDown(KeyCode.Mouse0)));

        //MaxRewindDuration �ð� �ʰ��ϸ� ������ �����ͺ��� ����
        if (timeSnapShots.Count > Mathf.Round(MaxRewindDuration / Time.fixedDeltaTime))
        {
            //List�� ���� ������ ������(���� �����Ȱ�)
            timeSnapShots.RemoveAt(timeSnapShots.Count - 1);
        }
    }

    public void PauseRecord()
    {
        isRecordPaused = true;
    }

    void RewindTime()
    {
        //������ �����ϸ�
        if (timeSnapShots.Count > 0)
        {

            TimeSnapShot snapShot = timeSnapShots[0];
            player.rb.velocity = snapShot.velocity;
            player.transform.position = snapShot.position;
            player.transform.rotation = snapShot.rotation;
            player.spriteRenderer.sprite = snapShot.sprite;
            player.FacingDir = snapShot.FacingDir;

            if (snapShot.Input_Mouse0)
                //����Ʈ �����ε� �۾� �ؾ���

            timeSnapShots.RemoveAt(0);
        }
        else
            StopRewind();

    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {

        isRewinding = false;
        isRecordPaused = false;

        //�������� �Ͼ�� �� �ٽ� �ε�
        player.stateMachine.ChangeState(player.idleState);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
