using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    //public AudioMixer mixer;
    public float pitchIncreaseSpeed;
    public float maxPitch;
    public float minPitch;
    private GameObject player;
    private Rigidbody2D rb;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        //audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1.5f /*/ audioSource.pitch*/);
    }
}
