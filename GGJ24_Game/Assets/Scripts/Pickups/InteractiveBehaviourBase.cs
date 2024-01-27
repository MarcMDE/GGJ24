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
    public event UnityAction OnInteractionRegionEnter;
    public event UnityAction OnInteractionRegionExit;
    public event UnityAction OnInteractionComplete;
    public event UnityAction OnInteractionCancel;

    void Start()
    {
        
    }

    void Update()
    {
        if (isComplete) return;

        if (canInteract)
        {
            if (Input.GetButton("Interact"))
            {
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

    void CancelInteraction()
    {
        isInteracting = false;
        interactionCounter = 0f;
        OnInteractionCancel?.Invoke();
    }    

    void CompleteInteraction()
    {
        isInteracting = false;
        interactionCounter = 0f;
        isComplete = true;
        OnInteractionComplete?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnInteractionRegionEnter?.Invoke();
        canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        OnInteractionRegionExit?.Invoke();
        canInteract = false;
    }
}
