using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchBehaviour : InteractiveBehaviourBase
{
    protected override void CompleteInteraction()
    {
        // TODO: Door behaviour
        Debug.Log("The door is open, you can escape!");

        base.CompleteInteraction();

    }
}
