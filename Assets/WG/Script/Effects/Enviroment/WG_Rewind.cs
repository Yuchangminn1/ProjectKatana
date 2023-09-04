using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_Rewind : MonoBehaviour
{
    public float MaxRewindDuration = 60f;
    public float rewindSpeed = 2f;
    public bool isRewinding = false;
    List<TimeSnapShot> timeSnapShots = new List<TimeSnapShot>();

    WG_Player player;

    private void Start()
    {
        player = WG_PlayerManager.instance.player;
    }
    struct TimeSnapShot
    {
        public Vector3 position;
        public Quaternion rotation;
        public Sprite sprite;
        public int FacingDir;
        public TimeSnapShot(Vector3 position, Quaternion rotation, Sprite sprite, int FacingDir)
        {
            this.position = position;
            this.rotation = rotation;
            this.sprite = sprite;
            this.FacingDir = FacingDir;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRewind();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            StopRewind();
        }
    }

    private void LateUpdate()
    {

        if (!isRewinding)
            RecordSnapShot();

        else
            RewindTime();
    }



    void RecordSnapShot()
    {

        timeSnapShots.Insert(0, new TimeSnapShot(player.transform.position, player.transform.rotation, player.spriteRenderer.sprite
            , player.FacingDir));

        //MaxRewindDuration �ð� �ʰ��ϸ� ������ �����ͺ��� ����
        if (timeSnapShots.Count > Mathf.Round(MaxRewindDuration / Time.fixedDeltaTime))
        {
            //List�� ���� ������ ������(���� �����Ȱ�)
            timeSnapShots.RemoveAt(timeSnapShots.Count - 1);
        }
    }

    void RewindTime()
    {
        //������ �����ϸ�
        if (timeSnapShots.Count > 0)
        {
            TimeSnapShot snapShot = timeSnapShots[0];
            player.transform.position = snapShot.position;
            player.transform.rotation = snapShot.rotation;
            player.spriteRenderer.sprite = snapShot.sprite;
            player.FacingDir = snapShot.FacingDir;

            timeSnapShots.RemoveAt(0);
        }
        else
            StopRewind();
    }

    void StartRewind()
    {
        isRewinding = true;
    }

    void StopRewind()
    {
        isRewinding = false;
    }
}
