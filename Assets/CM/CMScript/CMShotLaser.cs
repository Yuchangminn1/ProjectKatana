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

    [Header("Switch")]
    public bool isLaserStop = false; //On이 레이저 종료

    [Header("HitLaser")]
    [SerializeField] GameObject hitLaser;
    [SerializeField] float hitLaserPosY = 1f;
    [SerializeField] float DisSpeed = 0.05f;
    [SerializeField] float hitLaserScaley = 0f;


    private void Start()
    {
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

        StartCoroutine(ShotE());
    }

    IEnumerator ShotNReturn(GameObject _gameObject, float _startTime,int _i)
    {

        float yPos = _gameObject.transform.position.y;
        while (true)
        {
            _gameObject.GetComponent<SpriteRenderer>().enabled = !isLaserStop;
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
            //Debug.Log($"W {i}");
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();


        }
        yield return null;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"태그이름 {collision.tag}");
        
        if (collision.tag == "Player" && !isLaserStop)
        {
            Debug.Log("플레이어 사망");
            //플레이어 죽는 코드 




            isLaserStop = true;
            GameObject _hitLaser = Instantiate(hitLaser, new Vector2(transform.position.x, transform.position.y + hitLaserPosY), Quaternion.identity);
            _hitLaser.transform.localScale = new Vector2(_hitLaser.transform.localScale.x, _hitLaser.transform.localScale.y + hitLaserScaley);
            



            StartCoroutine(HitLaserDis(_hitLaser));


        }
    }

    IEnumerator HitLaserDis(GameObject _hit)
    {
        SpriteRenderer sr = _hit.GetComponent<SpriteRenderer>();
        float _redColor = 0.1f;
        Vector2 tmp = _hit.transform.localScale;
        while(tmp.x - 0.1f >0)
        {
            tmp.x -= DisSpeed;
            _hit.transform.localScale = tmp;
            sr.color = new Color(1- _redColor, 1,1,1);
            _redColor += DisSpeed;
            yield return new WaitForFixedUpdate();

        }
        isLaserStop = false;

        Destroy( _hit );
    }
    
}