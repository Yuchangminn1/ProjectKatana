using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMUIManager : MonoBehaviour
{
    private static CMUIManager instance = null;
    public static CMUIManager Instance;
    public Sprite[] originSprite { get; private set; }
    public Sprite[] blinkSprite { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = new CMUIManager();
            Instance = instance;
        }
        else 
            Destroy(instance.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlinckUI()
    {
        ;
    }
    IEnumerator BlinkSprite(float _startTime, float _blinktTime)
    {
        yield return new WaitForSeconds(_startTime);
        yield return null;
    }
    IEnumerator BlinkColor(float _startTime, float _blinktTime)
    {
        yield return new WaitForSeconds(_startTime);
        yield return null;
    }
}
