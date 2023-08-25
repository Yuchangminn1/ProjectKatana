using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public Vector2 CurrentMousePosition;
    public GameObject MouseCursorPrefab;
    GameObject cursor;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        cursor = Instantiate(MouseCursorPrefab, CurrentMousePosition, Quaternion.identity, this.transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {

        cursor.transform.position = CurrentMousePosition;


    }
}
