using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviourBase : MonoBehaviour
{
    [SerializeField] GameObject modelContainer;

    protected void Start()
    {

    }

    public void Pickup()
    {
        modelContainer.SetActive(false);
    }

    public virtual void Use()
    {

    }
}
