using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserSwich : MonoBehaviour
{
    [SerializeField] CMShotLaser[] lasers;
    public bool laserOn = true;

    // Start is called before the first frame update
    void LaserOnOff(bool _on)
    {
        int i = 0;

        if (_on)
        {
            while (lasers.Length > i)
            {
                lasers[i].laserOn = laserOn;
                //Debug.Log($"laserOn!@!@!@ = {laserOn}");
                lasers[i].LaserStart();
                ++i;
            }
        }
        else 
        {
            while (lasers.Length > i)
            {
                lasers[i].laserOn = laserOn;
                //Debug.Log($"laserOn!@!@!@ = {laserOn}");
                lasers[i].LaserStop();
                ++i;

            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           // Debug.Log("�÷��̾� ����ġ �浹");
            //����ġ �̹��� Ȱ��ȭ
            if (Input.GetKeyDown(KeyCode.C))
            {
              //  Debug.Log("�÷��̾� ����ġ �۵�");

                LaserOnOff(laserOn);
              //  Debug.Log($"laserOn = {laserOn}");
                laserOn = !laserOn;
            }
        }
    }
}
