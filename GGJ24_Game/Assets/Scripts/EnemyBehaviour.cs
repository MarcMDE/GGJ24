using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    EnemyStateSO currentState;

    bool isStateInitialized = false;
    private delegate void voidDelegate();

    private Vector3 lastNoiseHeardPos;

    [SerializeField] private LayerMask noiseLayer;
    [SerializeField] private LayerMask playerLayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var nextState = currentState.NextState();
        if (nextState != null)
        {
            currentState = nextState;
            isStateInitialized = false;
        }

        switch (currentState.State) 
        {
            case EnemyStates.WALK:
                ApplyState(InitWalk,UpdateWalk);
                break;

            // TODO: Add states

            default: break;
        }
    }

    private void ApplyState(voidDelegate init, voidDelegate update)
    {
        if (!isStateInitialized)
        {
            isStateInitialized = true;
            init();
        }
        update();
    }
    
    //Walk
    void InitWalk()
    {
        
    }
    void UpdateWalk()
    {
        // TODO: Walk logic
    }
    
    //Frenzy
    void InitFrenzy()
    {
        
    }
    void UpdateFrenzy()
    {

    }
    
    //Attack
    void InitAttack()
    {
        
    }
    void UpdateAttack()
    {
        
    }
    
    //Flank
    void InitFlank()
    {
        
    }
    void UpdateFlank()
    {
        
    }
    
    //TP
    void InitTp()
    {
        
    }
    void UpdateTp()
    {
        
    }
    
    //Noise
    void InitNoise()
    {
        
    }
    void UpdateNoise()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            EnemyTransitionConditionsContainer.Instance.Values.Melee = TriState.TRUE;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            EnemyTransitionConditionsContainer.Instance.Values.Melee = TriState.FALSE;
        }
        
    }
}
