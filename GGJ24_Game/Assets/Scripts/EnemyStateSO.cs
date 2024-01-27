using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "S_", menuName = "ScriptableObjects/Enemy State", order = 1)]
public class EnemyStateSO : ScriptableObject
{
    [SerializeField]
    EnemyStates state;

    [SerializeField] List<EnemyStateTransitionSO> transitions = new List<EnemyStateTransitionSO>();

    public EnemyStates State => state;

    public EnemyStateSO NextState()
    {
        foreach (var t in transitions)
        {
            if (t.CanTransition())
            {
                return t.NextState;
            }
        }

        return null;
    }

    public List<EnemyStates> GetPossibleStatesList()
    {
        List<EnemyStates> enemyStatesList = new List<EnemyStates>();
        foreach (var t in transitions)
        {
            enemyStatesList.Add(t.NextState.State);
        }

        return enemyStatesList;
    }
}
