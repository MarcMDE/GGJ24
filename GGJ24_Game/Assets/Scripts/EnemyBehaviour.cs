using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    EnemyStateSO currentState;

    bool isStateInitialized = false;

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
                if (!isStateInitialized)
                {

                    isStateInitialized = true;
                }

                UpdateWalk();
                break;

            // TODO: Add states

            default: break;
        }
    }

    void UpdateWalk()
    {
        // TODO: Walk logic


    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyTransitionConditionsContainer.Instance.Values.Melee = TriState.TRUE;
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyTransitionConditionsContainer.Instance.Values.Melee = TriState.FALSE;
    }
}
