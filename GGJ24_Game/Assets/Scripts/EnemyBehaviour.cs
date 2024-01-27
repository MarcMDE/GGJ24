using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent( typeof(NavMeshController),typeof(EnemyAudioPlayer))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] 
    EnemyStateSO startState;
    EnemyStateSO currentState;

    [SerializeField] private float frenzySpeedIncrementPercent;
    [SerializeField] private float flankSpeedIncrementPercent;
    [SerializeField] private float frenzyStartSpeedIncrementPercent;
    
    private bool isStateInitialized = false;
    
    private delegate void voidDelegate();

    private NavMeshController navMeshController;
    private EnemyAudioPlayer enemyAudioPlayer;
    
    //ToDO use actual player pos
    private Vector3 playerPos = Vector3.zero;
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
            init();
        }
        update();
    }
    
    //Walk
    void InitWalk()
    {
        var randomPoint = navMeshController.GetRandomPoint();
        navMeshController.SetDestination(randomPoint);
        isStateInitialized = true;
    }
    void UpdateWalk()
    {
        if (navMeshController.HasAgentReachedDestination()) EnemyTransitionConditionsContainer.Instance.Values.StateFinished = TriState.TRUE;
    }
    
    //Frenzy
    void InitFrenzy()
    {
        StartCoroutine(InitFrenzyCR());
    }

    
    
    void UpdateFrenzy()
    {
        navMeshController.IncreaseSpeed(frenzySpeedIncrementPercent * Time.deltaTime);
        navMeshController.SetDestination(playerPos);
    }
    
    //Attack
    void InitAttack()
    {
        StartCoroutine(AttackCR());
    }
    void UpdateAttack()
    {
        
    }
    
    //Flank
    void InitFlank()
    {
        navMeshController.SetDestination(playerPos);
    }
    void UpdateFlank()
    {
        navMeshController.IncreaseSpeed(flankSpeedIncrementPercent * Time.deltaTime);
        navMeshController.SetDestination(playerPos);
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
        StartCoroutine(InitFrenzyCR());
    }
    void UpdateNoise()
    {
        if (navMeshController.HasAgentReachedDestination()) EnemyTransitionConditionsContainer.Instance.Values.StateFinished = TriState.TRUE;
    }

    IEnumerator InitFrenzyCR()
    {
        navMeshController.Stop();
        enemyAudioPlayer.PlaySound(EnemyAudio.Scream);
        
        yield return new WaitForSeconds(1f);
        navMeshController.IncreaseSpeed(frenzyStartSpeedIncrementPercent);
        navMeshController.SetDestination(playerPos);
        isStateInitialized = true;
    }

    IEnumerator AttackCR()
    {
        yield return new WaitForSeconds(2f);
        EnemyTransitionConditionsContainer.Instance.Values.StateFinished = TriState.TRUE;
    }
    
}
