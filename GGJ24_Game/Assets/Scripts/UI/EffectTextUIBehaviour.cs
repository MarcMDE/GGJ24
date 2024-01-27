using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTextUIBehaviour : MonoBehaviour
{
    EffectsController effectsController;
    [SerializeField] GameObject container;
    void Start()
    {
        effectsController = Player.Instance.GetComponent<EffectsController>();
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
