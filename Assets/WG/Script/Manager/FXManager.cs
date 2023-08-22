using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager instance;
    public Effects effects { get; private set; }
    public PlayerStartRunDust playerStartRun { get; private set; }


    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        else instance = this;
    }

    private void Start()
    {
        effects = GetComponent<Effects>();
        playerStartRun = GetComponent<PlayerStartRunDust>();
    }
}
