using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    [SerializeField] RandomSoundsPlayer stepsSound;

    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        DisableStepsSound();

        playerController.OnStartMoving += EnableStepsSound;
        playerController.OnStopMoving += DisableStepsSound;
    }

    void EnableStepsSound()
    {
        stepsSound.Enable();
    }

    void DisableStepsSound()
    {
        stepsSound.Disable();
    }
    
}
