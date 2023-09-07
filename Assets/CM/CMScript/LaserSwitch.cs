using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserSwitch : MonoBehaviour
{
    [SerializeField] CMShotLaser[] onOffLasers;
    public bool laserOn = true;
    [SerializeField] SpriteRenderer spaceIcon;

    private void Start()
    {
        OnOffLaser();
    }
    
    private void Update()
    {
        if (spaceIcon.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnOffLaser();

            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            spaceIcon.enabled = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spaceIcon.enabled = false;

        }
    }
    private void OnOffLaser()
    {
        for (int i = 0; i < onOffLasers.Length; i++)
        {
            onOffLasers[i].isLaserStop = !laserOn;

        }
        laserOn = !laserOn;
    }
}
