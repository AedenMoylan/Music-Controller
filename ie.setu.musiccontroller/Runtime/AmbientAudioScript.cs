using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioScript : MonoBehaviour
{
    public AudioSource[] ambientAudio;
    public float minTimeForAudio;
    public float maxTimeForAudio;
    private float waitBetweenAudio;
    private bool willAudioBePlayed = false;

    public bool willAudioVolumeBeRandomised;
    public float minVolume
    {
        get { return _min; }
        set { _min = Mathf.Clamp(value, 0, 1); }
    }
    [SerializeField, Range(0, 1)] private float _min;

    public float maxVolume
    {
        get { return _max; }
        set { _max = Mathf.Clamp(value, 0, 1); }
    }
    [SerializeField, Range(0, 1)] private float _max;
    // Start is called before the first frame update
    void Start()
    {
        setNewWaitTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (willAudioBePlayed == false)
        {
            willAudioBePlayed = true;
            StartCoroutine(waitForAudio());
        }
    }

    void playRandomAudio()
    {
        int indexForRandomAudio = Random.Range(0,ambientAudio.Length);

        if (willAudioVolumeBeRandomised == true)
        {
            float randomVolume = Random.Range(minVolume * 100, maxVolume * 100);
            ambientAudio[indexForRandomAudio].volume = randomVolume / 100;

        }
        AudioClip clip = ambientAudio[indexForRandomAudio].clip;
        ambientAudio[indexForRandomAudio].PlayOneShot(clip);

    }

    void setNewWaitTime()
    {
        waitBetweenAudio = Random.Range(minTimeForAudio, maxTimeForAudio);
    }

    IEnumerator waitForAudio()
    {
        yield return new WaitForSeconds(waitBetweenAudio);
        playRandomAudio();
        setNewWaitTime();
        willAudioBePlayed=false;
    }


}
