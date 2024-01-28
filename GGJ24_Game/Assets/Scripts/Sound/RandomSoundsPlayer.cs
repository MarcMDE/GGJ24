using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundsPlayer : MonoBehaviour
{
    [SerializeField] SoundsCollectionSO soundsCollection;

    [SerializeField] bool useDelayBetweenClips;

    [SerializeField] float minDelayBetweenClips, maxDelayBetweenClips;

    [SerializeField] bool useMaxSoundTime;
    [SerializeField] float maxSoundTime;

    AudioSource audioSource;

    public void SetSoundsCollection(SoundsCollectionSO s)
    {
        soundsCollection = s;
    }

    public bool UseMaxSoundTime
    {
        get { return useMaxSoundTime; }
        set { useMaxSoundTime = value; }
    }

    public float MaxSoundTime
    {
        get { return maxSoundTime; }
        set { maxSoundTime = value; }
    }

    public float Volume { set { audioSource.volume = value; } }

    bool shouldPlay = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.clip = soundsCollection.GetClip(0);
    }

    public void Enable()
    {
        shouldPlay = true;
        PlayNewClip();
    }

    public void Disable()
    {
        shouldPlay = false;
        audioSource.Stop();
        StopCoroutine(PlayNewClipDelayedCoroutine());
    }

    void Update()
    {
        if (shouldPlay)
        {
            if (useMaxSoundTime)
            {
                if (audioSource.time > maxSoundTime) audioSource.Stop();
            }
            
            if (!audioSource.isPlaying)
            {
                if (!useDelayBetweenClips)
                {
                    PlayNewClip();
                }
                else
                {
                    StartCoroutine(PlayNewClipDelayedCoroutine());
                }
            }
        }
    }

    IEnumerator PlayNewClipDelayedCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minDelayBetweenClips, maxDelayBetweenClips));
        PlayNewClip();
    }

    void PlayNewClip()
    {
        audioSource.clip = soundsCollection.RandomClip;
        audioSource.Play();
    }
}
