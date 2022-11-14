using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.Audio;
using System.Reflection;
using System;
using UnityEditor.SearchService;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    //public AudioMixer mixer;
    public float pitchIncreaseSpeed;
    public float maxPitch;
    public float minPitch;
    public bool lowHealthMusicChange;
    public AudioSource lowHealthAudioSource;
    public string objectHealthName;
    private bool isLowHealthMusicPlaying;
    private GameObject player;
    private Rigidbody2D rb;
    private float speed;
    private int playerLives;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        isLowHealthMusicPlaying = false;       
    }

    // Update is called once per frame
    void Update()
    {
        var scripts = GetComponents<MonoBehaviour>();

        for (int i = 0; i < scripts.Length; i++)
        {
            if (scripts[i].GetType().GetField(objectHealthName) != null)
            {
                playerLives = (int)scripts[i].GetType().GetField(objectHealthName).GetValue(scripts[i]);
            }
        }

        speed = rb.velocity.magnitude;
        speed /= 10;
        audioSource.pitch = speed * pitchIncreaseSpeed;

        if (audioSource.pitch > maxPitch)
        {
            audioSource.pitch = maxPitch;
        }
        else if (audioSource.pitch < minPitch)
        {
            audioSource.pitch = minPitch;
        }

        if (isLowHealthMusicPlaying == false && lowHealthMusicChange == true && playerLives == 1)
        {
            changeLowHealthAudio();
        }
        //audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1.5f /*/ audioSource.pitch*/);
    }

    void changeLowHealthAudio()
    {
        if (lowHealthMusicChange == true)
        {
            audioSource.clip = lowHealthAudioSource.clip;
            audioSource.Play();
            isLowHealthMusicPlaying = true;
        }
    }
}
