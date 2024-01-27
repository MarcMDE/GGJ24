using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum TriState { FALSE=0, TRUE, X};

[Serializable]
public class EnemyTransitionConditions
{
    public static readonly int Length = 5;

    public TriState ViewPlayer;
    public TriState EyeContact;
    public TriState Melee;

    public TriState this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return ViewPlayer;
                case 1: 
                    return EyeContact;
                case 2:
                    return Melee;

                default: return TriState.X;
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    ViewPlayer = value;
                    break;
                case 1:
                    EyeContact = value;
                    break;
                case 2:
                    Melee = value;
                    break;

                default: break;
            }
        }
    }
}

[CreateAssetMenu(fileName = "EnemyStateTransition", menuName = "ScriptableObjects/Enemy State Transition", order = 1)]
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
