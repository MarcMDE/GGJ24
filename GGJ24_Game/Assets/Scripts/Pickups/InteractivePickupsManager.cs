using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractivePickupsManager : MonoBehaviour
{
    InteractivePickupBehaviour[] pickups;

    public event UnityAction<float> OnInteracting;
    public event UnityAction OnRegionEnter;
    public event UnityAction OnRegionExit;
    public event UnityAction OnComplete;
    public event UnityAction OnCancel;

    void Start()
    {
        pickups = GetComponentsInChildren<InteractivePickupBehaviour>();

        foreach (var pickup in pickups)
        {
            pickup.OnRegionEnter += EnterRegion;
            pickup.OnRegionExit += ExitRegion;
            pickup.OnComplete += Complete;
            pickup.OnCancel += Cancel;
            pickup.OnInteracting += Interacting;
        }
    }

    private void OnDestroy()
    {
        foreach (var pickup in pickups)
        {
            if (pickup)
            {
                pickup.OnRegionEnter -= EnterRegion;
                pickup.OnRegionExit -= ExitRegion;
                pickup.OnComplete -= Complete;
                pickup.OnCancel -= Cancel;
                pickup.OnInteracting -= Interacting;
            }
        }
    }

    void EnterRegion()
    {
        OnRegionEnter?.Invoke();
    }

    void ExitRegion()
    {
        OnRegionExit?.Invoke();
    }

    void Complete()
    {
        OnComplete?.Invoke();
    }

    void Cancel()
    {
        OnCancel?.Invoke();
    }

    void Interacting(float p)
    {
        OnInteracting?.Invoke(p);
    }
}
