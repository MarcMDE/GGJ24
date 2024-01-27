using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransitionConditionsContainer
{
    private readonly static EnemyTransitionConditionsContainer instance = new EnemyTransitionConditionsContainer();

    public static EnemyTransitionConditionsContainer Instance { get { return instance; } }

    public EnemyTransitionConditions Values;

}
