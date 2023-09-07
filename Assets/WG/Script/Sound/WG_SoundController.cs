using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WG_SoundController : MonoBehaviour
{
    AudioSource[] audioSources;

    //이펙트 사운드들은 자주 나오니까 미리 로드하고 딕셔너리에 보관
    Dictionary<string, AudioClip> audioClips_Effect = new Dictionary<string, AudioClip>();

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(SoundType));

            //사운드타입에 안쓰는 MaxCount 타입 있으니까 -1
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            audioSources[(int)SoundType.BGM].loop = true;
        }
    }

    public void Clear()
    {
        //오디오 소스 전부 정지, 음악 제거
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }

        //효과음 딕셔너리 비우기
        audioClips_Effect.Clear();
    }

    //기본 이펙트타입, 피치 1.0 으로 생성
    public void Play(AudioClip audioClip, SoundType type = SoundType.EffectSound, float pitch = 1.0f)
    {
        if (audioClip == null) return;

        //BGM 재생
        if (type == SoundType.BGM)
        {
            AudioSource audioSource = audioSources[(int)SoundType.BGM];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        if (type == SoundType.EffectSound)
        {
            AudioSource audioSource = audioSources[(int)SoundType.EffectSound];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    //public void Play(string path, SoundType type = SoundType.EffectSound, float pitch = 1.0f)
    //{
    //    AudioClip audioClip = GetOrAddAudioClip(path, type);
    //    Play(audioClip, type, pitch);
    //}

    //AudioClip GetOrAddAudioClip(string path, SoundType type = SoundType.EffectSound)
    //{
    //    //Sounds 폴더 안에 넣기
    //    if (path.Contains("Sounds/" == false))
    //    {
    //        path = $"Sounds/{path}"; //없으면 만들어서 저장
    //    }

    //    AudioClip audioClip = null;

    //    if (type == SoundType.BGM)
    //    {
    //        audioClip =
    //    }
    //}
}
