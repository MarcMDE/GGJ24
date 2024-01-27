using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent( typeof(NavMeshController),typeof(EnemyAudioPlayer))]
public class EnemyBehaviour : SingletonMonoBehaviour<EnemyBehaviour>
{
    [SerializeField] 
    EnemyStateSO startState;
    EnemyStateSO currentState;

    [SerializeField] private float frenzySpeedIncrementPercent;
    [SerializeField] private float flankSpeedIncrementPercent;
    [SerializeField] private float frenzyStartSpeedIncrementPercent;

    [SerializeField] private float enemyFovAngle = 60;
    [SerializeField] private float enemyVisionRange = 100;
    [SerializeField] private float timeHiddenThreshold = 5; 

    public float EnemyFovAngle => enemyFovAngle;

    public float CurrentSpeed => navMeshController.CurrentSpeed;

    private bool isStateInitialized = false;
    
    private delegate void voidDelegate();


    private NavMeshController navMeshController;
    private EnemyAudioPlayer enemyAudioPlayer;

    protected override void Awake()
    {
        base.Awake();

        navMeshController = GetComponent<NavMeshController>();
    }

    void Start()
    {
        currentState = startState;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState.State);
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
        navMeshController.ResetSpeed();
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
        navMeshController.SetDestination(Player.Instance.Position);
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
        navMeshController.SetDestination(Player.Instance.Position);
    }
    void UpdateFlank()
    {
        navMeshController.IncreaseSpeed(flankSpeedIncrementPercent * Time.deltaTime);
        navMeshController.SetDestination(Player.Instance.Position);
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
        navMeshController.IsStopped = true;
        //enemyAudioPlayer.PlaySound(EnemyAudio.Scream);
        
        yield return new WaitForSeconds(1f);
        navMeshController.IncreaseSpeed(frenzyStartSpeedIncrementPercent);
        navMeshController.SetDestination(Player.Instance.Position);
        navMeshController.IsStopped = false;
        isStateInitialized = true;
    }

    IEnumerator AttackCR()
    {
        yield return new WaitForSeconds(2f);
        EnemyTransitionConditionsContainer.Instance.Values.StateFinished = TriState.TRUE;
    }
    
}
