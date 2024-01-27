using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehaviour))]
public class EnemyConditionSetter : MonoBehaviour
{
    [SerializeField] private LayerMask noiseLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask scenarioLayer;
    [SerializeField] private LayerMask enemyLayer;

    private EnemyBehaviour enemyBehaviour;

    private float playerFarDistance;
    private float playerHorizontalFov;
    
    
    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        CheckViewPlayer();
        CheckViewedByPlayer();
        Debug.Log("c1: "+EnemyTransitionConditionsContainer.Instance.Values[0]+", "+"c2: "+EnemyTransitionConditionsContainer.Instance.Values[0]);
        
    }
    
    public void Init()
    {
        for (int i = 0; i < EnemyTransitionConditions.Length; i++)
        {
            EnemyTransitionConditionsContainer.Instance.Values[i] = TriState.FALSE;
        }
        
        playerHorizontalFov = Camera.main.fieldOfView * Camera.main.aspect/2;
        playerFarDistance = GetCameraDistance();
        
        Debug.Log("dist: "+playerFarDistance);
        Debug.Log("fov: "+playerHorizontalFov);
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            EnemyTransitionConditionsContainer.Instance.Values.Melee = TriState.TRUE;
        }
        if ((noiseLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            EnemyTransitionConditionsContainer.Instance.Values.NoiseHeard = TriState.TRUE;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            EnemyTransitionConditionsContainer.Instance.Values.Melee = TriState.FALSE;
        }
    }

    private void CheckViewPlayer()
    {
        var gazeVector = Player.Instance.Position - transform.position;
        var angle = Vector3.Angle(gazeVector, transform.forward);

        var viewPlayer = false;
        var rayMask = scenarioLayer | playerLayer;
        
        Debug.Log("angle: "+angle);
        
        if (angle < enemyBehaviour.EnemyFovAngle && Physics.Raycast(transform.position, gazeVector, out RaycastHit hit, enemyBehaviour.EnemyVisionRange,rayMask))
        {
            // Check if the ray hits the player
            if (((1 << hit.collider.gameObject.layer) & playerLayer.value) != 0)
            {
                viewPlayer = true;
            }
        }
        EnemyTransitionConditionsContainer.Instance.Values.ViewPlayer = viewPlayer ? TriState.TRUE : TriState.FALSE;
    }
    
    private void CheckViewedByPlayer()
    {
        var gazeVector = transform.position - Player.Instance.Position;

        var horizontalFov = Camera.main.fieldOfView * Camera.main.aspect;
        
        var angle = Vector3.Angle(gazeVector, Player.Instance.Forward);

        var viewedByPlayer = false;
        var rayMask = scenarioLayer | enemyLayer;
        if (angle < horizontalFov && Physics.Raycast(Player.Instance.Position, gazeVector, out RaycastHit hit, playerFarDistance, rayMask))
        {
            // Check if the ray hits the player
            if (((1 << hit.collider.gameObject.layer) & playerLayer.value) != 0)
            {
                viewedByPlayer = true;
            }
        }
        EnemyTransitionConditionsContainer.Instance.Values.ViewedByPlayer = viewedByPlayer ? TriState.TRUE : TriState.FALSE;
    }

    private float GetCameraDistance()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        // Check if the main camera is not null
        if (mainCamera == null) return 0;
        
        // Get the camera's position in world space
        Vector3 cameraPosition = mainCamera.transform.position;

        // Get the camera's forward direction
        Vector3 cameraForward = mainCamera.transform.forward;

        // Get the far clip plane distance
        float farClipPlaneDistance = mainCamera.farClipPlane;

        // Calculate the position of the far clipping plane in world space
        Vector3 farClipPlanePosition = cameraPosition + cameraForward * farClipPlaneDistance;

        // Calculate the distance from the camera to the far clipping plane
        return Vector3.Distance(cameraPosition, farClipPlanePosition);
    }
    
}
