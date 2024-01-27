using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractionUIBehaviour : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private InteractivePickupsManager pickupsManger;
    [SerializeField] private DoorSwitchBehaviour switchBehaviour;
    [SerializeField] private GameObject container;

    void Start()
    {
        container.SetActive(false);

        pickupsManger.OnComplete += Complete;
        pickupsManger.OnCancel += Cancel;
        pickupsManger.OnInteracting += Interacting;
        pickupsManger.OnRegionEnter += EnterRegion;
        pickupsManger.OnRegionExit += ExitRegion;

        switchBehaviour.OnComplete += Complete;
        switchBehaviour.OnCancel += Cancel;
        switchBehaviour.OnInteracting += Interacting;
        switchBehaviour.OnRegionEnter += EnterRegion;
        switchBehaviour.OnRegionExit += ExitRegion;
    }

    void EnterRegion()
    {
        container.SetActive(true);
    }

    void ExitRegion()
    {
        container.SetActive(false);
    }

    void Complete()
    {
        container.SetActive(false);
        image.fillAmount = 0f;
    }

    void Cancel()
    {
        image.fillAmount = 0f;
    }

    void Interacting(float p)
    {
        image.fillAmount = p;
    }
}
