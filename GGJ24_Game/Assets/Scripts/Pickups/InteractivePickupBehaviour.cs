using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePickupBehaviour : InteractiveBehaviourBase
{
    protected override void CompleteInteraction()
    {
        base.CompleteInteraction();

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<AudioSource>().enabled = true;

        Invoke("Destroy", 2.9f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
