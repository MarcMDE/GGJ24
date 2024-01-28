using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    
    [SerializeField] private MusicTrack[] tracks;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayTrack(MusicTrackNames.Ambience);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTrack(MusicTrackNames trackName, bool usePreviousDelay = false)
    {
        foreach (var track in tracks)
        {
            if (track.trackName == trackName)
            {
                audioSource.clip = track.clip;
                if (usePreviousDelay)
                {
                    var time = audioSource.time;
                    audioSource.Play((ulong)time);
                }
                else
                {
                    audioSource.Play();
                }
                
            }
        }
    }
}




[Serializable]
public class MusicTrack
{
    public MusicTrackNames trackName;
    public AudioClip clip;
}

public enum MusicTrackNames
{
    Ambience,
    Chase,
    TerrorFunny,
    Funny,
}