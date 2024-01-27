using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    const float IsMovingThreshold = 0.001f;

    [SerializeField]
    private GameSettingsSO settings;
    [SerializeField]
    private Camera playerCamera;

    private CharacterController characterController;
    private float rotationX = 0;

    Player playerData;

    bool isMoving = false;

    public event UnityAction OnStartMoving;
    public event UnityAction OnStopMoving;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerData = Player.Instance;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseLook();
    }

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        float movementMag = movement.magnitude;

        bool prevIsMoving = isMoving;
        isMoving = movementMag > IsMovingThreshold;

        if (!prevIsMoving && isMoving) OnStartMoving?.Invoke();
        else if (prevIsMoving && !isMoving) OnStopMoving?.Invoke();

        Vector3 velocity = movement.normalized * movementMag * playerData.Speed;
        velocity.y = 0;

        velocity = transform.TransformDirection(velocity);
        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * settings.MouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * settings.MouseSensivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
}