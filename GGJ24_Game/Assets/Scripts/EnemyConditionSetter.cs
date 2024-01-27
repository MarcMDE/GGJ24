using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConditionSetter : MonoBehaviour
{
    [SerializeField] private LayerMask noiseLayer;
    [SerializeField] private LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Init()
    {
        for (int i = 0; i < EnemyTransitionConditions.Length; i++)
        {
            EnemyTransitionConditionsContainer.Instance.Values[i] = TriState.FALSE;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            EnemyTransitionConditionsContainer.Instance.Values.Melee = TriState.TRUE;
        }
        if ((noiseLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            EnemyTransitionConditionsContainer.Instance.Values.NoiseHeard = TriState.TRUE;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            EnemyTransitionConditionsContainer.Instance.Values.Melee = TriState.FALSE;
        }
        if ((noiseLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            EnemyTransitionConditionsContainer.Instance.Values.NoiseHeard = TriState.FALSE;
        }
        
    }
}
