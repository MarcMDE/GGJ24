using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioClipInfo : MonoBehaviour
{
    [SerializeField] SoundsCollectionSO sounds = null;

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
        if (sounds != null)
        {
            audioSource.clip = sounds.RandomClip;
        }

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
    