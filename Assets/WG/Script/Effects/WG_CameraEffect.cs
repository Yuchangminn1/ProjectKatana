using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(CinemachineVirtualCamera))]
public class WG_CameraEffect : MonoBehaviour
{
    public CinemachineVirtualCamera cine;
    public CinemachineFramingTransposer cine_FramingTransposer;
    // public CinemachineTransposer cine_Transposer;
    [Header("Shake Info")]
    public float ShakeIntervalSec = 0.1f;
    public float ShakeDuration = 0.25f;
    public float ShakeTimer = 0f;
    [Range(0.01f, 1000)] public float ShakeRange = 1f;
    float ShakeRangeMirror;
    public bool isShaking = false;
    public bool ShakePreview_Debug = false;

    private void Start()
    {
        cine = Camera.main.transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
        cine_FramingTransposer = cine.GetCinemachineComponent<CinemachineFramingTransposer>();
        // cine_Transposer = cine.GetCinemachineComponent<CinemachineTransposer>();


    }

    private void Update()
    {
        //cine_Transposer.m_FollowOffset.x = Random.Range(-ShakeIntense, ShakeIntense);
        //cine_Transposer.m_FollowOffset.y = Random.Range(-ShakeIntense, ShakeIntense);

        if (ShakePreview_Debug)
            Shake();

        if (!isShaking)
            ShakeRangeMirror = ShakeRange;
    }

    private void FixedUpdate()
    {
        ShakeTimer += Time.fixedDeltaTime;
    }


    IEnumerator HitShake()
    {
        isShaking = true;
        ShakeTimer = 0f;

        while (ShakeTimer <= ShakeDuration)
        {
            Shake();
            yield return new WaitForSeconds(ShakeIntervalSec);
        }
        ResetCameraPostion();
        isShaking = false;
        yield return null;
    }

    private void Shake()
    {
        float redutctionFactor = 1f - (ShakeTimer / ShakeDuration);
        ShakeRange *= redutctionFactor;

        cine_FramingTransposer.m_TrackedObjectOffset.x = Random.value * ShakeRange * 2 - ShakeRange;
        cine_FramingTransposer.m_TrackedObjectOffset.y = Random.value * ShakeRange * 2 - ShakeRange;
    }

    public void ResetCameraPostion()
    {
        ShakeRange = ShakeRangeMirror;
        cine_FramingTransposer.m_TrackedObjectOffset.x = 0;
        cine_FramingTransposer.m_TrackedObjectOffset.y = 0;
    }
}
