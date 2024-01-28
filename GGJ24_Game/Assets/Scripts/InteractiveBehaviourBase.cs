using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveBehaviourBase : MonoBehaviour
{
    [SerializeField] private float interactionDuration;

    private float interactionCounter = 0f;

    private bool canInteract = false;
    private bool isInteracting = false;
    private bool isComplete = false;

    public event UnityAction<float> OnInteracting;
    public event UnityAction OnRegionEnter;
    public event UnityAction OnRegionExit;
    public event UnityAction OnComplete;
    public event UnityAction OnCancel;

    protected virtual void Start()
    {
        
    }

    void Update()
    {
        if (isComplete) return;

        if (canInteract)
        {
            if (Input.GetButton("Interact"))
            {
                if (!isInteracting) StartInteraction();
                
                isInteracting = true;

                interactionCounter += Time.deltaTime;

                if (interactionCounter > interactionDuration)
                {
                    CompleteInteraction();
                }
                else
                {
                    OnInteracting?.Invoke(interactionCounter /  interactionDuration);
                }
            }
            else if (isInteracting)
            {
                CancelInteraction();
            }
        }
        else if (isInteracting)
        {
            CancelInteraction();
        }
    }

    protected virtual void StartInteraction()
    {
        
    }
    

    protected virtual void CancelInteraction()
    {
        isInteracting = false;
        interactionCounter = 0f;
        OnCancel?.Invoke();
    }    

    protected virtual void CompleteInteraction()
    {
        isInteracting = false;
        interactionCounter = 0f;
        isComplete = true;
        OnComplete?.Invoke();
    }

    protected void EnterRegion()
    {
        if (isComplete) return;

        OnRegionEnter?.Invoke();
        canInteract = true;
    }

    protected void ExitRegion()
    {
        if (isComplete) return;

        CancelInteraction();

        OnRegionExit?.Invoke();
        canInteract = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnterRegion();
    }

    private void OnTriggerExit(Collider other)
    {
        ExitRegion();
    }
}
