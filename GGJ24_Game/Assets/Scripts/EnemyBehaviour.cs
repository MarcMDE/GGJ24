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
    [SerializeField] private float frenzyStartSpeed = 5;
    [SerializeField] private float frenzySpeedReduction = 5;
    

    [SerializeField] private float enemyFovAngle = 60;
    [SerializeField] private float enemyVisionRange = 100;
    [SerializeField] private float timeHiddenThreshold = 5;
    [SerializeField] private float startDamage = 60;
    [SerializeField] private float damageReduction = 20;
    [SerializeField] private float minDamage = 20;
    private float enemyDamage;
    
    [SerializeField] private EnemyStates[] statesWithAggroDropDelay;
    [SerializeField] private float aggroDropDelay = 0.25f;
    
    
    [SerializeField] private Animator animator;
    public float EnemyFovAngle => enemyFovAngle;
    public float AggroDropDelay => aggroDropDelay;
    public float CurrentSpeed => navMeshController.CurrentSpeed;
    public EnemyStates CurrentState => currentState.State;
    public EnemyStates[] StatesWithAggroDropDelay => statesWithAggroDropDelay;

    private float frenzySpeed;

    private bool isStateInitialized = false;
    private bool stateInitializationStarted = false;

    EnemyEffectsApplier effectsApplier;
    
    private delegate void voidDelegate();


    private NavMeshController navMeshController;
    private EnemyAudioPlayer enemyAudioPlayer;

    protected override void Awake()
    {
        base.Awake();

        enemyAudioPlayer = GetComponent<EnemyAudioPlayer>();
        navMeshController = GetComponent<NavMeshController>();
        effectsApplier = GetComponent<EnemyEffectsApplier>();
    }

    void Start()
    {
        frenzySpeed = frenzyStartSpeed;
        enemyDamage = startDamage;
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
        if (nextState != null && isStateInitialized)
        {
            currentState = nextState;
            isStateInitialized = false;
            stateInitializationStarted = false;
            EnemyTransitionConditionsContainer.Instance.Values.StateFinished = TriState.FALSE;
            Debug.Log(currentState.State);
        }
    }

    public void ReduceDamage()
    {
        enemyDamage = Mathf.Max(enemyDamage - damageReduction, minDamage);
    }

    public void ReduceFrenzySpeed()
    {
        frenzySpeed = Mathf.Max(frenzySpeed - frenzySpeedReduction, navMeshController.BaseSpeed);
    }

    public bool CanTranisitonToState(EnemyStates state)
    {
        return currentState.GetPossibleStatesList().Contains(state);
    }

    private void ApplyState(voidDelegate init, voidDelegate update)
    {
        if (!isStateInitialized)
        {
            if (!stateInitializationStarted)
            {
                stateInitializationStarted = true;
                init();
                
            }
        }
        else
        {
            update();
        }
        
    }
    
    //Walk
    void InitWalk()
    {
        var randomPoint = navMeshController.GetRandomPoint();
        navMeshController.SetDestination(randomPoint);
        navMeshController.ResetSpeed();
        isStateInitialized = true;
        SetAnimation(AnimatorStates.Walk);
        MusicFlow.Instance.TryPlayMusic(MusicTrackNames.Ambience);
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
        //navMeshController.IncreaseSpeed(frenzySpeedIncrementPercent * Time.deltaTime);
        navMeshController.SetDestination(Player.Instance.Position);
    }
    
    //Attack
    void InitAttack()
    {
        StartCoroutine(AttackCR());
    }
    void UpdateAttack()
    {
        EnemyTransitionConditionsContainer.Instance.Values.StateFinished = TriState.TRUE;
    }
    
    //Flank
    void InitFlank()
    {
        navMeshController.SetDestination(Player.Instance.Position);
        navMeshController.ResetSpeed();
        isStateInitialized = true;
        SetAnimation(AnimatorStates.Walk);
    }
    void UpdateFlank()
    {
        //navMeshController.IncreaseSpeed(flankSpeedIncrementPercent * Time.deltaTime);
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
        EnemyTransitionConditionsContainer.Instance.Values.NoiseHeard = TriState.FALSE;
        StartCoroutine(InitFrenzyCR());
    }
    void UpdateNoise()
    {
        if (navMeshController.HasAgentReachedDestination()) EnemyTransitionConditionsContainer.Instance.Values.StateFinished = TriState.TRUE;
    }

    IEnumerator InitFrenzyCR()
    {
        //Debug.Log("CR Instance");
        
        navMeshController.IsStopped = true;

        //Debug.Log("Init frenzy CR");
        enemyAudioPlayer.PlaySound(EnemyAudio.Scream);
        MusicFlow.Instance.TryPlayMusic(MusicTrackNames.Chase);

        SetAnimation(AnimatorStates.Scream);
        
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Scream"));
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        var time = stateInfo.length;
        
        yield return new WaitForSeconds(time * 0.8f);
        
        SetAnimation(AnimatorStates.Run);

        navMeshController.ResetSpeed();
        Debug.Log(navMeshController.BaseSpeed);
        navMeshController.SetSpeed(frenzySpeed);
        navMeshController.SetDestination(Player.Instance.Position);
        navMeshController.IsStopped = false;
        isStateInitialized = true;
    }

    IEnumerator AttackCR()
    {
        navMeshController.IsStopped = true;
        
        SetAnimation(AnimatorStates.Attack);

        if (effectsApplier.HasSalchicha)
            enemyAudioPlayer.PlaySound(EnemyAudio.AttackBalloon);
        else
            enemyAudioPlayer.PlaySound(EnemyAudio.Attack);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        var time = stateInfo.length;
        
        yield return new WaitForSeconds(time * 0.15f);
        Player.Instance.GetComponent<PlayerHP>().SufferDamage(enemyDamage);
        yield return new WaitForSeconds(time * 0.75f);
        //yield return new WaitForSeconds(0.5f);
        
        isStateInitialized = true;
        navMeshController.IsStopped = false;
    }

    private void SetAnimation(AnimatorStates state)
    {
        animator.SetInteger("expectedState", (int)state);
    }
}

public enum AnimatorStates
{
    Idle,
    Walk,
    Run,
    Scream,
    Attack
}
