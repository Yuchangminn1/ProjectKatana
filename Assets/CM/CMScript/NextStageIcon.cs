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



        // ��ǥ ��ġ ���
        Vector2 targetPosition = new Vector2(originX + MaxMove, rectT.position.y);

        // ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵�
        rectT.position = Vector2.Lerp(rectT.position, targetPosition, IconMoveSpeed * Time.deltaTime);

        // �������� ��ǥ ��ġ�� ����� �����ٸ� ó�� ��ġ�� 
        if (Vector2.Distance(rectT.position, targetPosition) < MaxMove/7f)
        {
            rectT.position = new Vector2(originX, rectT.position.y);
        }
    }
}
