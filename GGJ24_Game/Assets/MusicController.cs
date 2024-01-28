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

    public void PlayTrack(MusicTrackNames trackName)
    {
        foreach (var track in tracks)
        {
            if (track.trackName == trackName)
            {
                audioSource.clip = track.clip;
                audioSource.Play();
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