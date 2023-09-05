using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BGLGreen : LightBlink
{
    
    [Header("BGLGreen")]

    [SerializeField] protected int coroutineNum =0;

    [SerializeField] protected float StartTime = 0;

    [Header("Move 0  circlestart ")]

    [Header("Move 1  Updown ")]
    [SerializeField] protected float originX;
    [SerializeField] protected float originY;

    [SerializeField] protected float startAngle = 1f / 4f;
    [SerializeField] protected float currentAngle = 1f/4f;
    [SerializeField] protected float divAngle = 45;

    [SerializeField] protected float circleR = 3f;
    [Header("Move 2 circle")]
    [SerializeField] protected float maxMove = 3f;

    protected override void Start()
    {
        #region StartSetUp

        Vector2 tmp = transform.position;
        originX = tmp.x;
        originY = tmp.y;
        currentAngle = startAngle;
        #endregion
        
        StartCoroutine(StartTimeSeting());
        
    }

    private void SelectC()
    {
        
        if (coroutineNum == 0)
            StartCoroutine(LightCircleStar());
        else if (coroutineNum == 1)
            StartCoroutine(LightUpDown());
        else if (coroutineNum == 2)
            StartCoroutine(LightCircle());
    }
    



    //현재 높이는 8;

    IEnumerator StartTimeSeting()
    {
        yield return new WaitForSeconds(FRandom());
        currentAngle = FRandom(currentAngle);
        divAngle = FRandom(20f, 61f);
        SelectC();
        yield return null;

    }

    
    #region NomalMode
    #region LightCircleStar
    IEnumerator LightCircleStar()
    {
        Vector2 currentPos = Vector2.zero;
        transform.position = new Vector2(originX + circleR*(float)Math.Sin(startAngle), originY + circleR* (float)Math.Cos(startAngle)) ;

        while (true)
        {
            int count = 0;
            float PlusX = circleR * (float)Math.Sin(currentAngle) / divAngle * 2;
            float PlusY = circleR * (float)Math.Cos(currentAngle) / divAngle * 2;
            currentPos = new Vector2(originX + circleR * (float)Math.Sin(currentAngle), originY + circleR * (float)Math.Cos(currentAngle));

            transform.position = currentPos;

            yield return null;

            while (count < divAngle)
            {
                transform.position = new Vector3(currentPos.x - PlusX, currentPos.y - PlusY);
                currentPos = transform.position;
                ++count;
                //Debug.Log(count);
                yield return null;
            }
            count = 0;
            while (count < divAngle)
            {
                transform.position = new Vector3(currentPos.x + PlusX, currentPos.y + PlusY);
                currentPos = transform.position;

                ++count;
                yield return null;
            }
            currentAngle += 1f/18f;

        }
    }
    #endregion

    #region LightUpDown

    IEnumerator LightUpDown()
    {
        Vector2 currentPos = Vector2.zero;
        Vector3 RetrunPos = new Vector2(originX, originY + maxMove / 2);
        float PlusY = maxMove / divAngle;
        while (true)
        {
            int count = 0;
            
            currentPos = RetrunPos;

            transform.position = currentPos;

            yield return null;

            while (count < divAngle)
            {
                transform.position = new Vector3(currentPos.x, currentPos.y - PlusY);
                currentPos = transform.position;
                ++count;
               // Debug.Log(count);
                yield return null;
            }
            count = 0;
            while (count < divAngle)
            {
                transform.position = new Vector3(currentPos.x, currentPos.y + PlusY);
                currentPos = transform.position;

                ++count;
                yield return null;
            }
            currentAngle += 1f / 18f;

        }
    }
    #endregion

    #region LightCircle

    IEnumerator LightCircle()
    {
        Vector2 currentPos = Vector2.zero;
        transform.position = new Vector2(originX + circleR * (float)Math.Sin(startAngle), originY + circleR * (float)Math.Cos(startAngle));

        while (true)
        {
            currentPos = new Vector2(originX + circleR * (float)Math.Sin(currentAngle), originY + circleR * (float)Math.Cos(currentAngle));

            transform.position = currentPos;

            yield return null;

            
            currentAngle += 1f / 18f;

        }
    }
    #endregion
    #endregion

   
    float FRandom()
    {
        return UnityEngine.Random.Range(0.0f, 1.1f);
    }
    float FRandom(float num)
    {
        return UnityEngine.Random.Range(num, num+100f);
    }
    float FRandom(float num1, float num2)
    {
        return UnityEngine.Random.Range(num1, num2);
    }
    float IRandom()
    {
        return UnityEngine.Random.Range(0, 3);
    }
}
