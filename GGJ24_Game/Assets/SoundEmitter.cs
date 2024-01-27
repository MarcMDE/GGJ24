using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof( SphereCollider ), typeof( AudioSource ))]
public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private SphereCollider soundRadius;

    private AudioSource audioSource;
    
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator BroadcastSound()
    {
        soundRadius.gameObject.SetActive(true);
        yield return null;
        yield return null;
        soundRadius.gameObject.SetActive(false);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        var combinedMask = playerLayer | enemyLayer;
        
        if ((combinedMask & (1 << other.gameObject.layer)) > 0)
        {
            audioSource.Play();
            if ((playerLayer & (1 << other.gameObject.layer)) > 0)
            {
                StartCoroutine(BroadcastSound());
            }
        }
        
    }
}
