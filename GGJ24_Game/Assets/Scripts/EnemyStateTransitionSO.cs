using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "T_", menuName = "ScriptableObjects/Enemy State Transition", order = 1)]
public class EnemyStateTransitionSO : ScriptableObject
{   
    [SerializeField]
    EnemyStateSO nextState;

    [SerializeField]
    EnemyTransitionConditions transitionConditions;

    public EnemyStateSO NextState => nextState;

    public bool CanTransition()
    {
        bool t = true;
        int ci = 0;

        
        var conditionsContainer = EnemyTransitionConditionsContainer.Instance;
        
        while (t && ci < EnemyTransitionConditions.Length)
        {
            if (transitionConditions[ci] != TriState.X &&
                transitionConditions[ci] != conditionsContainer.Values[ci])
                t = false;

            ci++;
        }
        
        return t;
    }
}
