using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageIcon : MonoBehaviour
{

    [Header("Next Stage Icon")]
    [SerializeField] RectTransform rectT;
    [SerializeField] float originX = 0;
    [SerializeField] float IconMoveSpeed = 1f;
    [SerializeField] float MaxMove = 2f;

    private void Start()
    {
        rectT = GetComponent<RectTransform>();
        originX = rectT.position.x;

    }
    private void Update()
    {
        NextIconMove();
    }

    void NextIconMove()
    {



        // 목표 위치 계산
        Vector2 targetPosition = new Vector2(originX + MaxMove, rectT.position.y);

        // 현재 위치에서 목표 위치로 부드럽게 이동
        rectT.position = Vector2.Lerp(rectT.position, targetPosition, IconMoveSpeed * Time.deltaTime);

        // 아이콘이 목표 위치에 충분히 가깝다면 처음 위치로 
        if (Vector2.Distance(rectT.position, targetPosition) < MaxMove/7f)
        {
            rectT.position = new Vector2(originX, rectT.position.y);
        }
    }
}
