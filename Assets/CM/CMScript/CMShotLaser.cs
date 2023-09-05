using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMShotLaser : MonoBehaviour
{

    [SerializeField] Transform shotPos;
    [SerializeField] GameObject cmLaser;
    [SerializeField] GameObject[] cmLaserList;




    [SerializeField] int laserNum = 15;
    [SerializeField] bool isShoot = true;
    [SerializeField] float laserMoveSpeed = 1;
    [SerializeField] Vector3 laserScale;
    [SerializeField] float yReturn = -1.360f;
    [SerializeField] float cmy = 0;
    public bool laserOn  = true;

    [SerializeField] LaserSwich laserSwich;


   // Coroutine LaserShoot;
    private void Start()
    {
        laserSwich = GameObject.Find("LaserSwitch").GetComponent<LaserSwich>();
        if (laserSwich == null)
            Debug.Log("아 안들어간다고 ");
        if (laserSwich.laserOn == null)
            Debug.Log("아 안들어간다고2 ");
        if (laserScale == null || laserScale == Vector3.zero)
        {
            laserScale = Vector3.one;
        }
        int i = 0;
        cmLaserList = new GameObject[laserNum];
        while (i < laserNum)
        {
            cmLaserList[i] = Instantiate(cmLaser);
            cmLaserList[i].transform.parent = transform;
            cmLaserList[i].transform.localScale = laserScale;
            cmLaserList[i].transform.position = cmLaser.transform.position;
            ++i;
        }

        LaserStart();
    }

    public void LaserStart()
    {
       

        laserOn = laserSwich.laserOn;
     //   StartCoroutine(ShotE());

    }
    public void LaserStop()
    {
        
        laserOn = laserSwich.laserOn;
        

       // StopCoroutine(ShotE());
    }
    IEnumerator ShotNReturn(GameObject _gameObject, float _startTime,int _i)
    {
        float yPos = _gameObject.transform.position.y;
        while (laserOn)
        {
            //yield return null;
            yield return new WaitForFixedUpdate();

            if (yPos < yReturn)
            {
                cmy *= -1;

                if (cmy < 0)
                {
                    yPos = shotPos.position.y + 1f + cmy;
                }
                else
                    yPos = shotPos.position.y + 1f;


                _gameObject.transform.position = new Vector2(shotPos.position.x, yPos);

                
            }
            else yPos = _gameObject.transform.position.y - Time.deltaTime * laserMoveSpeed;
            _gameObject.transform.position = new Vector2(shotPos.position.x, yPos);
        }
    }

    IEnumerator ShotE()
    {
        //yield return new WaitForFixedUpdate();
        int i = 0;
        while (i < laserNum)
        {
            
            cmy *= -1f;
            StartCoroutine(ShotNReturn(cmLaserList[i], 0.2f * i, i));
            ++i;
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();


        }
        yield return null;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.tag == "Player")
        {
            Debug.Log("플레이어 사망");
            
        }
    }
}