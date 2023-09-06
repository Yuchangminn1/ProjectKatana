using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//녹화 데이터 관리 간편하게 만드는 스크립트
public class Recording
{



    //리플레이 데이터 레퍼런스로 사용
    //극한의 최적화 할거면 사용X
    Queue<ReplayData> originalQueue;

    //플레이백에 사용
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
