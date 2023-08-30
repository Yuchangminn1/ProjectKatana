using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraEffect : MonoBehaviour
{
    public CinemachineVirtualCamera cine;
    public CinemachineFramingTransposer cine_FramingTransposer;
    // public CinemachineTransposer cine_Transposer;
    [Header("Shake Info")]
    public float ShakeIntervalSec = 0.1f;
    public float ShakeDuration = 0.25f;

    public float ShakeIntense = 1f;
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
        {
            Shake();
        }
    }

    private void Shake()
    {
        cine_FramingTransposer.m_TrackedObjectOffset.x = Random.Range(-ShakeIntense, ShakeIntense);
        cine_FramingTransposer.m_TrackedObjectOffset.y = Random.Range(-ShakeIntense, ShakeIntense);
    }

    IEnumerator HitShake()
    {
        float shakeTimer = 0f;
        shakeTimer += Time.deltaTime;
        while (shakeTimer <= ShakeDuration)
        {
            Shake();
            yield return new WaitForSeconds(ShakeIntervalSec);
        }
        yield return null;
    }
}
