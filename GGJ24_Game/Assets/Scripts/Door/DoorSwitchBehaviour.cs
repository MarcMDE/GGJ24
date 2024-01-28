using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorSwitchBehaviour : InteractiveBehaviourBase
{
    private AudioSource audioSource;
    [SerializeField] SphereCollider soundRadius;
    
    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }
    protected override void CompleteInteraction()
    {
        // TODO: Door behaviour
        Debug.Log("The door is open, you can escape!");

        base.CompleteInteraction();
        
    }

    protected override void StartInteraction()
    {
        base.StartInteraction();
        audioSource.Play();
        StartCoroutine(BroadcastNoise());
    }

    IEnumerator BroadcastNoise()
    {
        soundRadius.gameObject.SetActive(true);
        yield return null;
        yield return null;
        soundRadius.gameObject.SetActive(false);
    }
}
