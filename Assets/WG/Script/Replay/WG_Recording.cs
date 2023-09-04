using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ȭ ������ ���� �����ϰ� ����� ��ũ��Ʈ
public class Recording
{



    //���÷��� ������ ���۷����� ���
    //������ ����ȭ �ҰŸ� ���X
    Queue<ReplayData> originalQueue;

    //�÷��̹鿡 ���
    Queue<ReplayData> replayQueue;

    public Recording(Queue<ReplayData> recordingQueue)
    {
        originalQueue = new Queue<ReplayData>(recordingQueue);
        replayQueue = new Queue<ReplayData>(recordingQueue);
    }

    public void RestartFromBeginning()
    {
        replayQueue = new Queue<ReplayData>(originalQueue);
    }

    public bool PlayNextFrame()
    {
        bool hasMoreFrames = false;
        if (replayQueue.Count != 0)
        {
            ReplayData data = replayQueue.Dequeue();
            hasMoreFrames = true;
        }
        return hasMoreFrames;
    }

}
