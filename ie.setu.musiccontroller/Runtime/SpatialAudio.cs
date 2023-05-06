using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;

public class SpatialAudio : MonoBehaviour
{
    public GameObject originObjectForAudio;
    public GameObject[] objectsForSpatialAudio;

    public string[] nameOfSpatialAudio;
    public float maxDistanceForSpatialAudio;
    public bool playAudioUnderCertainDistance;
    public string[] nameOfAudioToPlayAtCertainDistance;
    public float distanceToChangeSoundEffect;
    public List<float> distance = new List<float>();

    private List<bool> isCloseDistanceAudioPlaying = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < objectsForSpatialAudio.Length; i++)
        {
            isCloseDistanceAudioPlaying.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        changeVolumeBasedOnDistance();
    }

    void changeVolumeBasedOnDistance()
    {
        for (int i = 0; i < objectsForSpatialAudio.Length; i++)
        {
            List<AudioSource> audioSources = new List<AudioSource>();
            for (int j = 0; j < nameOfSpatialAudio.Length; j++)
            {
                foreach (var component in objectsForSpatialAudio[i].GetComponents<AudioSource>())
                {
                    if (component.clip.name == nameOfSpatialAudio[j])
                    {
                        AudioSource sourceToAdd = component;
                        audioSources.Add(sourceToAdd);
                    }
                }
            }
            Vector2 playerPosition = originObjectForAudio.transform.position;
            Vector2 enemyPosition = objectsForSpatialAudio[i].transform.position;
            float part1 = (playerPosition.y - playerPosition.x) * (playerPosition.y - playerPosition.x);
            float part2 = (enemyPosition.y - enemyPosition.x) * (enemyPosition.y - enemyPosition.x);
            distance.Add(Mathf.Sqrt(part1 + part2));

            if (distance.ElementAt(i) < 0)
            {
                distance.ElementAt(i).Equals(0);
            }

            if (distance.ElementAt(i) < distanceToChangeSoundEffect)
            {
                changeSoundEffect(objectsForSpatialAudio[i], i);
            }
            else
            {
                if (isCloseDistanceAudioPlaying[i] == true)
                {
                    stopCloseAudio(objectsForSpatialAudio[i], i);
                }
                if (distance.ElementAt(i) <= maxDistanceForSpatialAudio)
                {
                    
                    foreach (AudioSource source in audioSources)
                    {
                        source.volume = (maxDistanceForSpatialAudio - distance.ElementAt(i)) / maxDistanceForSpatialAudio;
                    }
                }
                else
                {
                    foreach (AudioSource source in audioSources)
                    {
                        source.volume = 0;
                    }
                }
            }
        }
        distance.Clear();
    }

    private void changeSoundEffect(GameObject _object, int _index)
    {
        foreach (var component in _object.GetComponents<AudioSource>())
        {
            for (int i = 0; i < nameOfAudioToPlayAtCertainDistance.Length; i++)
            {
                if (component.clip.name != nameOfAudioToPlayAtCertainDistance[i])
                {
                    component.Stop();
                }
                else
                {
                    if (isCloseDistanceAudioPlaying[_index] == false)
                    {
                        isCloseDistanceAudioPlaying[_index] = true;
                        component.Play();
                    }
                }
            }
        }
    }

    private void stopCloseAudio(GameObject _object, int _index)
    {
        foreach (var component in _object.GetComponents<AudioSource>())
        {
            for (int i = 0; i < nameOfAudioToPlayAtCertainDistance.Length; i++)
            {
                if (component.clip.name != nameOfAudioToPlayAtCertainDistance[i])
                {
                    component.Play();
                }
                else if (component.clip.name == nameOfAudioToPlayAtCertainDistance[i])
                {
                    isCloseDistanceAudioPlaying[_index] = false;
                    component.Stop();
                }
            }
        }
    }
}
