using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.UI.Image;

public class BGLGreen : LightBlink
{
    [SerializeField] protected float originX;
    [SerializeField] protected float originY;

    [SerializeField] protected int currentState = 1;
    [SerializeField] protected float stateX = 1;
    [SerializeField] protected float stateY = 1;

    [SerializeField] protected double startAngle = 45;



    /*[SerializeField] protected float currentX;
    [SerializeField] protected float currentY;*/

    [SerializeField] protected float maxMove;

    [SerializeField] protected float circleR = 4f;

    /*[SerializeField] protected float moveX;
    [SerializeField] protected float moveY;*/

    //[SerializeField] protected float maxMovey;

    [SerializeField] protected float moveDistance;
    [SerializeField] protected float moveSpeed =1;

    // Start is called before the first frame update
    

    // Update is called once per frame
    protected override void Start()
    {
        
        Vector2 tmp = transform.position;
        originX = tmp.x;
        originY = tmp.y;
        transform.position = new Vector2(originX * circleR*(float)Math.Sin(startAngle), originY * (float)Math.Cos(startAngle)) ;
        StartCoroutine(LightMovingDown1());
    }
    //현재 높이는 8;
    IEnumerator LightMovingDown1()
    {
        Vector2 currentPos = Vector2.zero;
        while (true)
        {
            int count = 0;
            currentPos = new Vector2(transform.position.x, transform.position.y);

            while (moveDistance < maxMove)
            {

                if (count == 2)
                {
                    yield return null;
                    count = 0;
                }
                moveDistance += Time.deltaTime * moveSpeed;
                transform.position = new Vector3(currentPos.x - moveDistance, currentPos.y - moveDistance);
                ++count;

            }
            moveDistance = maxMove;
            count = 0;
            while (moveDistance >0f)
            {
                if (count == 2)
                {
                    yield return null;
                    count = 0;
                }
                moveDistance -= Time.deltaTime * moveSpeed;
                transform.position = new Vector3(currentPos.x - moveDistance, currentPos.y - moveDistance);

                //transform.position = new Vector3(originX + moveDistance, originY + moveDistance);
                ++count;

            }
            currentPos = new Vector2(transform.position.x, transform.position.y);

            //우 상단 시작기준  1사분면 시작 > 4 > 3 > 2;
/*            if (currentState == 1)
            {
                stateX = circleX / 8f;
                stateY = -circleY / 8f;

                if (currentPos.y <= 0)
                    currentState = 4;
            }
            else if (currentState == 4)
            {
                stateX = -circleX / 8f;
                stateY = -circleY / 8f;

                if (currentPos.x <= 0)
                    currentState = 3;
            }
            else if (currentState == 3)
            {
                stateX = -circleX / 8f;
                stateY = circleY / 8f;

                if (currentPos.y >= 0)
                    currentState = 2;
            }
            else if (currentState == 2)
            {
                stateX = circleX / 8f;
                stateY = circleY / 8f;

                if (currentPos.x >= 0)
                    currentState = 1;
            }*/

            for (int i = 0; i < 10; i++)
            {
                currentPos.x += (stateX / 10f);
                currentPos.y += (stateY / 10f);
                transform.position = currentPos;
                yield return null;

            }

            /*if ()
            circleX / 8f
            circleY / 8f*/

            //16번 돌면 원 위치로 해보자 
            yield return null;
        }


    }



}
