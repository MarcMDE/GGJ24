using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    const int NEffects = 5;

    [SerializeField]
    GameObject effectModel;

    [SerializeField] InteractivePickupsManager pickupsManager;


    [SerializeField] Vector2 screenOffsetF;
    [SerializeField] float cameraDistF;

    EffectsEnum currentEffect = default;

    public event Action OnUse;
    public event Action OnPickup;


    void Start()
    {
        currentEffect = EffectsEnum.NONE;
        effectModel.SetActive(false);
        
        pickupsManager.OnComplete += Pickup;
    }

    void Update()
    {
        if (currentEffect != EffectsEnum.NONE && Input.GetButtonDown("Interact"))
        {
            Use();
        }
    }

    public void Pickup()
    {
        currentEffect = (EffectsEnum)UnityEngine.Random.Range(1, NEffects + 1);
        effectModel.transform.position = ComputePosition();
        effectModel.SetActive(true);
        OnPickup?.Invoke();
    }

    private Vector3 ComputePosition()
    {
        // Obtén las dimensiones de la pantalla
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector3 nuevaPosicion = new Vector3(screenWidth - screenWidth * screenOffsetF.x, screenHeight * screenOffsetF.y, cameraDistF);

        Vector3 posicionEnMundo = Camera.main.ScreenToWorldPoint(nuevaPosicion);
        return posicionEnMundo;
    }

    public void Use()
    {
        effectModel.SetActive(false);
        OnUse?.Invoke();
        // TODO: Play animation

        // TODO: Apply effect to enemy
        currentEffect = EffectsEnum.NONE;
    }
}
