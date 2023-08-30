using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraEffect : MonoBehaviour
{
    public CinemachineVirtualCamera cine = null;

    private void Start()
    {
        cine = GameObject.Find("Camera").GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        cine.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y -= 1f;
    }
}
