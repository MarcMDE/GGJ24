using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransitionConditionsContainer
{
    private readonly static EnemyTransitionConditionsContainer instance = new EnemyTransitionConditionsContainer();

    private EnemyTransitionConditionsContainer()
    {
        Values = new EnemyTransitionConditions();
    }

    public static EnemyTransitionConditionsContainer Instance { get { return instance; } }

    public EnemyTransitionConditions Values;
    
    public Vector3 LastNoiseHeardPos;
}
