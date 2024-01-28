using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractivePickupBehaviour : InteractiveBehaviourBase
{
    [FormerlySerializedAs("noiseCollider")] [SerializeField] private SphereCollider soundRadius;
    protected override void CompleteInteraction()
    {
        base.CompleteInteraction();

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<AudioSource>().enabled = true;
        StartCoroutine(BroadcastNoise());
        Invoke("Destroy", 2.9f);
        
        
    }

    IEnumerator BroadcastNoise()
    {
        soundRadius.gameObject.SetActive(true);
        yield return null;
        yield return null;
        soundRadius.gameObject.SetActive(false);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
