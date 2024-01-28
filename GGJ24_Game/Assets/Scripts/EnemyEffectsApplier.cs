using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class EnemyEffectsApplier : MonoBehaviour
{
    const int NEffects = 5;

    [SerializeField] Transform leftLeg, rightLeg;

    [SerializeField] Transform hips;

    [SerializeField] Transform head;

    List<EffectsEnum> effectsLeft = new List<EffectsEnum>();

    private void Start()
    {
        for (int i=1; i<=NEffects; i++)
        {
            if (Random.Range(0, 2) > 0)
                effectsLeft.Append((EffectsEnum)i);
            else
                effectsLeft.Insert(0, (EffectsEnum)i);
        }
    }

    public void ApplyRandomEffect()
    {
        EffectsEnum effect;
        if (effectsLeft.Count == 0) effect = EffectsEnum.NONE;
        else
        {
            effect = effectsLeft.First();
            effectsLeft.RemoveAt(0);
        }

        switch (effect)
        {
            case EffectsEnum.SHORTLEGS:
                leftLeg.localScale = Vector3.one * 0.4f;
                rightLeg.localScale = Vector3.one * 0.4f;
                break;
            case EffectsEnum.NO_WEAPON_1:
                head.localScale += Vector3.one * 2f + Vector3.right * 1.2f;
                break;
            case EffectsEnum.NO_WEAPON_2:
                head.localScale += Vector3.one * 2f + Vector3.right * 1.2f;
                break;
            case EffectsEnum.RIDICUOLOUS:
                head.localScale += Vector3.one * 2f + Vector3.right * 1.2f;
                break;
            case EffectsEnum.SMOL:
                hips.localScale = Vector3.one * 0.6f;
                break;
            default:
                break;
        }
    }
}
