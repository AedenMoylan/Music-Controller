using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    public AudioSource SoundEffect;
    public string keyForAudioLowercase;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyForAudioLowercase))
        {
            AudioClip clip = SoundEffect.clip;
            SoundEffect.PlayOneShot(clip);
        }
    }
}
