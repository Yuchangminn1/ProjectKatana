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
            cmLaserList[i].transform.position = new Vector2(cmLaserList[i].transform.position.x, cmLaserList[i].transform.position.y + 3f);
            ++i;
        }

        StartCoroutine(ShotE());
    }

    IEnumerator ShotNReturn(GameObject _gameObject, float _startTime,int _i)
    {
        float yPos = _gameObject.transform.position.y;
        
        if (_i == laserNum - 1)
        {
            StartCoroutine(ShotE());
        }
        while (true)
        {
            if (yPos < yReturn)
            {
                yPos = shotPos.position.y;
                _gameObject.transform.position = new Vector2(shotPos.position.x, yPos);
                break;
            }
            else yPos = _gameObject.transform.position.y - Time.deltaTime * laserMoveSpeed;
            _gameObject.transform.position = new Vector2(shotPos.position.x, yPos);

            yield return new WaitForFixedUpdate();

        }
    }
    IEnumerator ShotE()
    {
        int i = 0;
      
        while (i < laserNum)
        {
            StartCoroutine(ShotNReturn(cmLaserList[i], 0.2f * i, i));
            ++i;
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

        }

    }
}