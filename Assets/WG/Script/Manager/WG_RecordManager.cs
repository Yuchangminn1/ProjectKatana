using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WG_RecordManager : MonoBehaviour
{
    public static WG_RecordManager instance;

    public WG_Rewind Player_rewind { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        else
            instance = this;
    }

    private void Start()
    {
        Player_rewind = GetComponent<WG_Rewind>();
    }
}
