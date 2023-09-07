using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_InputManager : WG_Managers
{
    public static WG_InputManager instance;
    public Vector2 CurrentMousePosition;
    public GameObject MouseCursorPrefab;
    public Vector2 cursorDir = new Vector2();
    public float playerLookingCursorAngle;
    GameObject cursor;

    protected override void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        else
            instance = this;

        base.Awake();
    }
    void Start()
    {
        Cursor.visible = false;
        cursor = Instantiate(MouseCursorPrefab, CurrentMousePosition, Quaternion.identity, transform);
    }

    void Update()
    {
        CurrentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = CurrentMousePosition;

        playerLookingCursorAngle = Mathf.Atan2(instance.cursorDir.y,
            instance.cursorDir.x) * Mathf.Rad2Deg;

        //플레이어가 커서블 바라보는 벡터2의 방향값
        cursorDir = (CurrentMousePosition - (Vector2)WG_PlayerManager.instance.player.transform.position).normalized;
    }
}
