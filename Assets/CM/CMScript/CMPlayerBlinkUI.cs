using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CMPlayerBlinkUI : MonoBehaviour
{

    [Header("Timer")]
    [SerializeField] Slider timeSlider = null;
    [SerializeField] float TimelimitSetTime = 10;
    [SerializeField] GameObject TimeOverImage = null;
    [SerializeField] GameObject DeadImage = null;

    [Header("0~1,Timer")]
    [Header("2.Item")]
    [Header("3~13.AbilityVar")]
    [Header("14. TimeOverImage")]

    public GameObject[] UIGameObject;

    [SerializeField] float[] startTime;
    [SerializeField] float[] midleTime;
    [SerializeField] public float ABTime = 10f;


    [SerializeField] float darkSpeed = 2f;


    #region WG 추가
    bool isDead = false;
    [SerializeField] public bool isBatteryOff = false;
    [SerializeField] public GameObject[] Batteries;
    float ABtimeMax;
    float startTimelimitSetTime;
    #endregion
    void Start()
    {
        //WG 추가
        ABTime -= 1;
        ABtimeMax = ABTime;

        if (timeSlider == null)
        {
            return;
        }
        #region BlinkSetUp
        int size = UIGameObject.Length;

        startTime = new float[size];
        midleTime = new float[size];



        startTime[0] = 2f;
        midleTime[0] = 3f;

        startTime[1] = 2f;
        midleTime[1] = 3f;

        startTime[2] = 6f;
        midleTime[2] = 4f;

        startTime[3] = 4f;
        midleTime[3] = 3f;

        StartCoroutine(BlinkSprite(startTime[0], midleTime[0], 0, 1));

        StartCoroutine(BlinkSprite(startTime[2], midleTime[2], 2));

        StartCoroutine(BlinkSprite(startTime[3], midleTime[3], 3, 13));
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        if (timeSlider == null)
        {
            return;
        }
        CMDeadImage();
        CMOnTimer();
        WGBatteryTweak();
    }

    public void BlinckUI()
    {
        ;
    }
    #region Blink
    IEnumerator BlinkSprite(float _startTime, float _midleTime, int _num)
    {
        Image thisImage = UIGameObject[_num].GetComponent<Image>();

        if (thisImage == null)
        {

            Text thisText = UIGameObject[_num].GetComponent<Text>();


            while (true)
            {

                int i = 0;
                int maxi = Random.Range(3, 8);
                while (i < maxi)
                {
                    float setA = 1f;
                    while (true)
                    {
                        if (setA <= 0f)
                            break;
                        setA -= 0.1f * darkSpeed;
                        thisText.color = new Color(0.8f, 0, 0.7f, setA);
                        yield return new WaitForSeconds(0.05f);

                    }
                    setA = 0f;
                    while (true)
                    {
                        if (setA >= 1f)
                            break;
                        setA += 0.04f * darkSpeed;
                        thisText.color = new Color(0.8f, 0, 0.7f, setA);

                        yield return new WaitForSeconds(0.05f);



                    }
                    setA = 1f;
                    thisText.color = new Color(0.8f, 0, 0.7f, setA);



                    ++i;
                }
                yield return new WaitForSeconds(0.3f);

            }

        }
        else
        {
            yield return new WaitForSeconds(_startTime);
            while (true)
            {

                int i = 0;
                int maxi = Random.Range(3, 8);
                while (i < maxi)
                {
                    float setA = 1f;
                    while (true)
                    {
                        if (setA <= 0f)
                            break;
                        setA -= 0.1f * darkSpeed;
                        thisImage.color = new Color(1, 1, 1, setA);
                        yield return null;
                    }
                    setA = 0f;
                    while (true)
                    {
                        if (setA >= 1f)
                            break;
                        setA += 0.04f * darkSpeed;
                        thisImage.color = new Color(1, 1, 1, setA);
                        yield return null;

                    }
                    thisImage.color = new Color(1, 1, 1, 1);

                    ++i;
                }
                yield return new WaitForSeconds(_midleTime);

            }
        }

    }

    IEnumerator BlinkSprite(float _startTime, float _midleTime, int _num, int _num2)
    {
        int imageSize = _num2 - _num + 1;
        Image[] thisImage = new Image[imageSize];
        for (int i = 0; i < imageSize; i++)
        {
            thisImage[i] = UIGameObject[_num + i].GetComponent<Image>();
        }

        yield return new WaitForSeconds(_startTime);
        while (true)
        {

            int i = 0;
            int maxi = Random.Range(3, 8);
            while (i < maxi)
            {
                float setA = 1f;
                while (true)
                {
                    if (setA >= 1f)
                        break;
                    setA += 0.04f * darkSpeed;
                    for (int j = 0; j < imageSize; j++)
                    {
                        thisImage[j].color = new Color(1, 1, 1, setA);
                    }

                    yield return null;
                }
                setA = 0f;
                while (true)
                {
                    if (setA >= 1f)
                        break;
                    setA += 0.04f * darkSpeed;
                    for (int j = 0; j < imageSize; j++)
                    {
                        thisImage[j].color = new Color(1, 1, 1, setA);
                    }

                    yield return null;

                }
                for (int j = 0; j < imageSize; j++)
                {
                    thisImage[j].color = new Color(1, 1, 1, 1);
                }

                ++i;
            }
            yield return new WaitForSeconds(_midleTime);

        }
    }
    #endregion


    //타이머 바 줄어들기
    public void CMOnTimer()
    {
        if (!WG_PlayerManager.instance.player.isDead)
        {
            //WG - unscaledDeltaTime로 변경하고 조건문 추가
            if (WG_PlayerManager.instance.player.isBulletTime)
                timeSlider.value -= Time.unscaledDeltaTime / TimelimitSetTime;
            else
                timeSlider.value -= Time.deltaTime / TimelimitSetTime;



            if (timeSlider.value <= 0f)
            {
                CMTimeOverImage();
                StartCoroutine(BlinkSprite(startTime[0], midleTime[0], 14));
                timeSlider.transform.gameObject.SetActive(false);


            }
        }
    }

    // 시간초과 메세지 
    public void CMTimeOverImage()
    {
        TimeOverImage.SetActive(true);

        //WG 추가
        if (!isDead)
        {
            isDead = true;
            WG_PlayerManager.instance.player.stateMachine.ChangeState(WG_PlayerManager.instance.player.deadStartState);
        }
    }
    public void CMDeadImage()
    {
        if(WG_PlayerManager.instance.player.isDead)
            DeadImage.SetActive(true);

    }
    //WG추가
    public void WGBatteryTweak()
    {
        //ABTime은 설정값 - 1 인 상태
        ABTime = Mathf.Clamp(ABTime, 0, ABtimeMax);
        int BatteryCount = Mathf.CeilToInt(ABTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            ABTime -= 2 * Time.unscaledDeltaTime;
            Batteries[BatteryCount].SetActive(false);
            //Batteries[BatteryCount].GetComponentInChildren<Image>().color = Color.red;

        }

        else
        {
            ABTime += 3 * Time.unscaledDeltaTime;
            Batteries[BatteryCount].SetActive(true);
            //Batteries[BatteryCount].GetComponentInChildren<Image>().color = Color.white;

        }

        if (ABTime <= 0)
            isBatteryOff = true;

        else
            isBatteryOff = false;
    }
}
