using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [SerializeField]
    GameObject effectModel;

    Animator effectAnimator;
    AudioSource effectAudioSource;

    [SerializeField] InteractivePickupsManager pickupsManager;

    [SerializeField] float useDuration = 1.1f;


    [SerializeField] Vector2 screenOffsetF;
    [SerializeField] float cameraDistF;

    bool hasEffect = false;

    private void Awake()
    {
        effectAnimator = GetComponentInChildren<Animator>();
        effectAudioSource = GetComponentInChildren<AudioSource>();

        effectAudioSource.enabled = false;
    }

    public event Action OnUse;
    public event Action OnPickup;

    public event Action OnCanUse;
    public event Action OnCantUse;

    bool canUse = false;
    Transform enemy;


    void Start()
    {
        effectModel.SetActive(false);
        enemy = EnemyBehaviour.Instance.transform;
        
        pickupsManager.OnComplete += Pickup;
    }

    void Update()
    {
        if (hasEffect)
        {
            var prevCanUse = canUse;
            canUse = EnemyClose();

            if (!prevCanUse && canUse) OnCanUse?.Invoke();
            else if (prevCanUse && !canUse) OnCantUse?.Invoke();

            if (Input.GetButtonDown("Interact") && canUse)
                Use();
        }
    }

    bool EnemyClose()
    {
        return (Vector3.SqrMagnitude(transform.position - enemy.position) < 300f);
    }

    public void Pickup()
    {
        canUse = false;

        effectModel.transform.position = ComputePosition();

        effectAnimator.SetTrigger("reset");
        hasEffect = true;
        effectAudioSource.enabled = false;
        effectModel.SetActive(true);
        OnPickup?.Invoke();
        OnCantUse?.Invoke();
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
        OnUse?.Invoke();

        //effectAnimator.ResetTrigger("reset");
        effectAnimator.SetTrigger("play");

        effectAudioSource.enabled = true;

        Invoke("DeactivateEffectModel", useDuration);

        EnemyBehaviour.Instance.GetComponent<EnemyEffectsApplier>().ApplyRandomEffect();

        // TODO: Apply effect to enemy
        hasEffect = false;
    }

    void DeactivateEffectModel()
    {
        effectAudioSource.enabled = false;
        //effectAnimator.ResetTrigger("play");
        effectModel.SetActive(false);
    }
}
