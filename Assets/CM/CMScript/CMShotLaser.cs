using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMShotLaser : MonoBehaviour
{
    [SerializeField] Transform shotPos;
    [SerializeField] GameObject cmLaser;
    [SerializeField] GameObject[] cmLaserList;
    [SerializeField] int laserNum = 15;
    [SerializeField] float laserMoveSpeed = 1;
    [SerializeField] Vector3 laserScale;
    [SerializeField] float yReturn = -1.360f;

    private void Start()
    {
        if (laserScale == null || laserScale == Vector3.zero ){
            laserScale = Vector3.one;
        }
        int i = 0;
        cmLaserList = new GameObject[laserNum];
        while (i < laserNum)
        {
            cmLaserList[i] = Instantiate(cmLaser);

            cmLaserList[i].transform.parent = transform;
            cmLaserList[i].transform.localScale = laserScale;
            StartCoroutine(ShotNReturn(cmLaserList[i], 0.2f * i ));
            
            ++i;

        }
    }

    IEnumerator ShotNReturn(GameObject _gameObject, float _startTime)
    {
        float yPos = _gameObject.transform.position.y;
        yield return new WaitForSeconds(_startTime);
        while (true)
        {
            //Debug.Log(_startTime);
            if (yPos < yReturn)
            {
                yPos = shotPos.position.y;
                Debug.Log("yRewturn");
            }
            else yPos = _gameObject.transform.position.y - Time.deltaTime * laserMoveSpeed;
            _gameObject.transform.position = new Vector2(shotPos.position.x, yPos);

            yield return null;

            
        }

        yield return null;
    }

}
