using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class WG_SoundManager : MonoBehaviour
{
    public static WG_SoundManager instance;

    [Header("BGM Info")]
    [SerializeField] Sound[] BGM;
    [SerializeField] public AudioSource BGM_Player;
    [SerializeField][Range(-3f, 3f)] public float BGM_Player_RewindPith = -2f;

    [Header("SFX Info")]
    [SerializeField] Sound[] EffectSound;
    [SerializeField] AudioSource[] EffectSound_Player;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        else
            instance = this;
    }

    private void Start()
    {
    }

    public void PlayBGM(string bgmName, float pitch = 1.0f, bool isMute = false)
    {
        for (int i = 0; i < BGM.Length; i++)
        {
            if (bgmName == BGM[i].name)
            {
                BGM_Player.clip = BGM[i].clip;
                BGM_Player.pitch = pitch;
                BGM_Player.mute = isMute;
                BGM_Player.loop = true;
                BGM_Player.Play();
            }
        }
    }

    public void StopBGM()
    {
        BGM_Player.Stop();
    }

    public void PlayEffectSound(string soundName, float Volume = 1.0f)
    {
        for (int i = 0; i < EffectSound.Length; i++)
        {
            if (EffectSound[i].name == soundName)
            {
                for (int j = 0; j < EffectSound_Player.Length; j++)
                {
                    if (!EffectSound_Player[j].isPlaying)
                    {
                        EffectSound_Player[j].clip = EffectSound[i].clip;
                        EffectSound_Player[j].volume = Volume;
                        EffectSound_Player[j].Play();
                        //EffectSound_Player[j].PlayOneShot(EffectSound[i].clip);
                        return;
                    }
                    EffectSound_Player[j].volume = 1f;
                }
                Debug.Log("모든 사운드가 재생중");
                return;
            }
        }
        Debug.Log(soundName + "이름의 효과음이 존재하지않음");
        return;
    }
}
