using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShaderType
{
    NoShader,
    Sprite_Default,
    Sprite_URP_Lit,
    Custom
}
public class WG_GhostControl : MonoBehaviour
{
    public ShaderType shaderType = ShaderType.Sprite_Default;
    [SerializeField] public Shader CustomShader;
    ShaderType shaderTypeChecker;

    public GameObject Shadow;
    public GameObject Player;
    float timer;
    public float ShadowSpawnSpeed;
    public float ShadowLifeTime = 0.3f;
    public Color _color;
    [Range(0.000f, 1.000f)] public float MaxAlpha = 1.0f;
    [Range(0.000f, 1.000f)] public float MinAlpha = 0f;
    Animator anim;

    private void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }
    public void Shadows_Skill()
    {
        timer += ShadowSpawnSpeed * Time.deltaTime;

        if (timer >= ShadowLifeTime)
        {
            timer = 0;

            GameObject clone_Shadow = WG_ObjectPool.instance.ObjectQueue.Dequeue();
            clone_Shadow.transform.position = Player.transform.position;
            clone_Shadow.transform.rotation = Player.transform.rotation;
            clone_Shadow.SetActive(true);

            clone_Shadow.GetComponent<SpriteRenderer>().sprite =
                Player.GetComponentInChildren<SpriteRenderer>().sprite;

            clone_Shadow.GetComponent<WG_GhostTrailTest>()._color = _color;

        }
    }

    private void OnValidate()
    {
        //ObjectPool instance 값 가져가기전에 실행되는것 방지
        if (WG_ObjectPool.instance != null)
            WG_ObjectPool.instance.RefreshTheQueue(0);
    }
}
