using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WG_SoundController : MonoBehaviour
{
    AudioSource[] audioSources;

    //����Ʈ ������� ���� �����ϱ� �̸� �ε��ϰ� ��ųʸ��� ����
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

            //����Ÿ�Կ� �Ⱦ��� MaxCount Ÿ�� �����ϱ� -1
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
        //����� �ҽ� ���� ����, ���� ����
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }

        //ȿ���� ��ųʸ� ����
        audioClips_Effect.Clear();
    }

    //�⺻ ����ƮŸ��, ��ġ 1.0 ���� ����
    public void Play(AudioClip audioClip, SoundType type = SoundType.EffectSound, float pitch = 1.0f)
    {
        if (audioClip == null) return;

        //BGM ���
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
    //    //Sounds ���� �ȿ� �ֱ�
    //    if (path.Contains("Sounds/" == false))
    //    {
    //        path = $"Sounds/{path}"; //������ ���� ����
    //    }

    //    AudioClip audioClip = null;

    //    if (type == SoundType.BGM)
    //    {
    //        audioClip =
    //    }
    //}
}
