using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMLaserAnima : MonoBehaviour
{
    [SerializeField] GameObject[] laser;
    [SerializeField] float MoveY;
    [SerializeField] float DivY;
    [SerializeField] Transform StartPos;

    // 
    

    private void Start()
    {
        
        StartCoroutine(LaserAnima());
    }



    IEnumerator LaserAnima()
    {
        float MoveX = StartPos.position.x;
        MoveY /= DivY;
        Debug.Log("MoveY = " + MoveY);
        laser[0].transform.position = StartPos.position;
        laser[1].transform.position = StartPos.position;
        while (true)
        {
            int count = 0;
            laser[0].transform.position = StartPos.position;
            laser[1].transform.position = StartPos.position;
            while (count<DivY)
            {
                laser[1].transform.position = new Vector2(MoveX, laser[1].transform.position.y - MoveY);
                ++count;
                yield return null;
                //Debug.Log("count = " + count);

            }
            count = 0;

            //while (count < DivY)
            //{
            //    laser[0].transform.position = new Vector2(MoveX, laser[0].transform.position.y - MoveY);
            //    ++count;

            //    yield return null;
            //    Debug.Log("count = " + count);

            //}



            yield return null;

        }




        yield return null;
    }
}
