using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioClipInfo : MonoBehaviour
{
    public EnemyAudio AudioType;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Play()
    {
        audioSource.Play();
    }
}

public enum EnemyAudio
{
    Scream,
    BucketScream,
    Walk,
    WalkNoisy,
    Attack,
    AttackBalloon,
}
    