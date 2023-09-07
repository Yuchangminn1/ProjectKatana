using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMTitleBlink : MonoBehaviour
{
    [SerializeField] Image blinkImage;
    private void Start()
    {
        blinkImage = GetComponent<Image>();
        StartCoroutine("BlinkTitle");
    }
    IEnumerator BlinkTitle()
    {
        while(true)
        {
            blinkImage.enabled = false;
            yield return new WaitForSeconds(0.2f);

            blinkImage.enabled = true;
            int i = 0;
            while (i < 4)
            {
                blinkImage.enabled = false;
                yield return new WaitForSeconds(0.05f);

                blinkImage.enabled = true;
                yield return new WaitForSeconds(0.05f);
                ++i;
            }
            yield return new WaitForSeconds(3f);


        }


    }
}
