using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof(NavMeshController))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] 
    EnemyStateSO startState;
    EnemyStateSO currentState;
    
    private bool isStateInitialized = false;
    
    private delegate void voidDelegate();

    private NavMeshController navMeshController;
    void Start()
    {
        navMeshController = GetComponent<NavMeshController>();
        currentState = startState;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState.State) 
        {
            case EnemyStates.WALK:
                ApplyState(InitWalk,UpdateWalk);
                break;
            case EnemyStates.FLANK:
                ApplyState(InitFlank,UpdateFlank);
                break;
            case EnemyStates.NOISE:
                ApplyState(InitNoise,UpdateNoise);
                break;
            case EnemyStates.ATTACK:
                ApplyState(InitAttack,UpdateAttack);
                break;
            case EnemyStates.FRENZY:
                ApplyState(InitFrenzy,UpdateFrenzy);
                break;
            case EnemyStates.TELEPORT:
                ApplyState(InitTp,UpdateTp);
                break;

            default: break;
        }
        
        var nextState = currentState.NextState();
        if (nextState != null)
        {
            currentState = nextState;
            isStateInitialized = false;
            EnemyTransitionConditionsContainer.Instance.Values.StateFinished = TriState.FALSE;
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
        var randomPoint = navMeshController.GetRandomPoint();
        navMeshController.SetDestination(randomPoint);
    }
    void UpdateWalk()
    {
        if (navMeshController.HasAgentReachedDestination()) EnemyTransitionConditionsContainer.Instance.Values.StateFinished = TriState.TRUE;
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
    
}
