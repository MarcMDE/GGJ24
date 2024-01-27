using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePickupBehaviour : InteractiveBehaviourBase
{
    protected override void CompleteInteraction()
    {
        base.CompleteInteraction();

        gameObject.SetActive(false);
    }
}
