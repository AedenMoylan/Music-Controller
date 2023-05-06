using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    public AudioSource SoundEffect;
    public AudioSource movementAudio;
    public AudioSource damageAudio;
    public string keyForAudioLowercase;
    public bool willMovementAudioBePlayed;
    public float minSpeedToPlayAudio;
    private bool isMovingAudioPlaying;
    private int previousPlayerLives;
    private int playerLives;
    public string objectHealthName;
    private bool canUpdateStart = false;
    public bool willSoundBePlayedOnDeath;
    public AudioSource deathSound;
    private bool hasPlayerDied = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(assignVariablesAfterAPause());
    }

    // Update is called once per frame
    void Update()
    {
        if (canUpdateStart == true)
        {
            if (Input.GetKeyDown(keyForAudioLowercase))
            {
                AudioClip clip = SoundEffect.clip;
                SoundEffect.PlayOneShot(clip);
            }

            var scripts = GetComponents<MonoBehaviour>();

            for (int i = 0; i < scripts.Length; i++)
            {
                if (scripts[i].GetType().GetField(objectHealthName) != null)
                {
                    playerLives = (int)scripts[i].GetType().GetField(objectHealthName).GetValue(scripts[i]);
                }
            }

            if (previousPlayerLives > playerLives)
            {
                Debug.Log("Cool");
            }
            checkhasHealthBeenReduced();
            previousPlayerLives = playerLives;
            addMovementAudio();

            if (playerLives == 0 && willSoundBePlayedOnDeath == true && hasPlayerDied == false)
            {
                playDeathAudio();
                hasPlayerDied = true;
            }
            else if (playerLives > 0)
            {
                hasPlayerDied = false;
            }
        }
    }

    void addMovementAudio()
    {

        float speed = GetComponent<Rigidbody2D>().velocity.magnitude;

        if (speed > minSpeedToPlayAudio && isMovingAudioPlaying == false)
        {
            movementAudio.Play();
            isMovingAudioPlaying = true;
        }
        else if (speed < minSpeedToPlayAudio)
        {
            isMovingAudioPlaying = false;
            movementAudio.Stop();
        }

    }

    void checkhasHealthBeenReduced()
    {
        if (previousPlayerLives > playerLives)
        {
            damageAudio.Play();
        }
    }

    IEnumerator assignVariablesAfterAPause()
    {
        yield return new WaitForSeconds(0.1f);
        var scripts = GetComponents<MonoBehaviour>();

        for (int i = 0; i < scripts.Length; i++)
        {
            if (scripts[i].GetType().GetField(objectHealthName) != null)
            {
                playerLives = (int)scripts[i].GetType().GetField(objectHealthName).GetValue(scripts[i]);
                previousPlayerLives = playerLives;
            }
        }

        canUpdateStart = true;
    }

    private void playDeathAudio()
    {
        deathSound.Play();
    }
}