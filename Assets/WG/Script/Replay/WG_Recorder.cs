using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//������ �������� Object�� Attach
public class Recorder : MonoBehaviour
{

    public Queue<ReplayData> recordingQueue { get; private set; }
    Recording recording;

    bool isDoingReplay = false;

    private void Awake()
    {
        recordingQueue = new Queue<ReplayData>();
    }
    private void Start()
    {
        WG_GameEventManager.instance.onGoalReached += OnGoalReached;
        WG_GameEventManager.instance.onRestartLevel += OnRestartLevel;
    }

    private void OnDestroy()
    {
        WG_GameEventManager.instance.onGoalReached -= OnGoalReached;
        WG_GameEventManager.instance.onRestartLevel -= OnRestartLevel;
    }

    void OnGoalReached()
    {
        StartReplay();
    }

    void OnRestartLevel()
    {
        Reset();
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.R))
            StartReplay();

        if (!isDoingReplay)
            return;

        bool hasMoreFrames = recording.PlayNextFrame();

        //����ŸƮ ������ ���������� ����ŸƮ ����
        if (!hasMoreFrames)
        {
            RestartReplay();
        }

    }
    public void RecordReplayFrame(ReplayData replayData)
    {
        recordingQueue.Enqueue(replayData);
        Debug.Log("Recorded data : " + replayData.position);
    }

    void StartReplay()
    {
        isDoingReplay = true;
        recording = new Recording(recordingQueue);
        //����
        recordingQueue.Clear();
    }

    void RestartReplay()
    {
        isDoingReplay = true;
        recording.RestartFromBeginning();
    }

    private void Reset()
    {
        isDoingReplay = false;
        recordingQueue.Clear();

        //�ʱ�ȭ
        recording = null;
    }
}
