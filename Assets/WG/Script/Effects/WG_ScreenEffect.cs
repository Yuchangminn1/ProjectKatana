using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WG_ScreenEffect : MonoBehaviour
{
    public GameObject ScreenController;
    public Camera MainCamera;
    public RenderTexture renderTexture;
    private void Awake()
    {
        RetroEffectOff();
    }

    public void RetroEffectOff()
    {
        MainCamera.targetTexture = null;
        ScreenController.SetActive(false);
    }

    public void RetroEffectON()
    {
        MainCamera.targetTexture = renderTexture;
        ScreenController.SetActive(true);
    }
}
