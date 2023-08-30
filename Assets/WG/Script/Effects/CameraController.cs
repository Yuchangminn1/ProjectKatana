using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera cine = null;
    void Start()
    {
        cine = FXManager.instance.cameraEffect.cine;
    }

    void Update()
    {

    }
}
