using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTextUIBehaviour : MonoBehaviour
{
    [SerializeField] EffectsController effectsController;
    [SerializeField] GameObject container;
    void Start()
    {
        container.SetActive(false);

        effectsController.OnPickup += Show;
        effectsController.OnUse += Hide;
    }

    void Update()
    {
        
    }

    void Show()
    {
        container.SetActive(true);
    }

    void Hide()
    {
        container.SetActive(false);
    }
}
