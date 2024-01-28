using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class EnemyEffectsApplier : MonoBehaviour
{
    const int NEffects = 5;

    [SerializeField] Transform enemyModel;

    [SerializeField] Transform leftLeg, rightLeg;

    [SerializeField] Transform hips;

    [SerializeField] Transform head;

    bool hasLegsEffect = false;
    bool hasSmolEffect = false;

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

        Debug.Log($"{effectsLeft.Count}-{effect}");

        switch (effect)
        {
            case EffectsEnum.SHORTLEGS:
                leftLeg.localScale = Vector3.one * 0.4f;
                rightLeg.localScale = Vector3.one * 0.4f;

                hasLegsEffect = true;

                if (hasSmolEffect) enemyModel.transform.position = Vector3.down * 0.86f;
                else enemyModel.transform.position = Vector3.down * 0.65f;
                // H: 0.35
                // H: 0.14
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

                hasSmolEffect = true;
                if (hasLegsEffect) enemyModel.transform.position = Vector3.down * 0.86f;
                else enemyModel.transform.position = Vector3.down * 0.46f;
                // H: 0.54
                // H: 0.14
                break;
            default:
                break;
        }
    }
}
