using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMSoundManager : MonoBehaviour
{
    public static CMSoundManager instance;
    public AudioSource CMAudioSource;

    public AudioClip[] CMSound;

    public GameObject AudioGame;
    // Start is called before the first frame update

    private void Start()
    {
        CMAudioSource = transform.GetComponent<AudioSource>();
        CMSoundPlayer(CMSound[0]);
    }
    

    // Update is called once per frame
   
    void CMSoundPlayer(AudioClip _sound)
    {
        
        CMAudioSource.clip = _sound;
        CMAudioSource.Play();


    }
}
