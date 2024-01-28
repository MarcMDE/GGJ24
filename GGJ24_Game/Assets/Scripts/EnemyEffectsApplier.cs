using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;



public class EnemyEffectsApplier : MonoBehaviour
{
    const int NEffects = 8;

    [SerializeField] private float terrorFunnyMusicChange = 0.4f;
    [SerializeField] private float funnyMusicChange = 0.8f;

    [SerializeField] Transform enemyModel;

    [SerializeField] Transform leftLeg, rightLeg;

    [SerializeField] Transform hips;

    [SerializeField] Transform head;

    [SerializeField] Transform cone;

    [SerializeField] Transform leftClaw;

    [SerializeField] Transform rightClaw;

    [SerializeField] Transform flotador;

    [SerializeField] Transform duck1, duck2;

    [SerializeField] GameObject particlesPrefab;

    [SerializeField] SoundsCollectionSO duckSounds;
    [SerializeField] RandomSoundsPlayer stepsSoudns;

    bool hasLegsEffect = false;
    bool hasSmolEffect = false;
    bool hasSalchicha = false;

    public bool HasSalchicha => hasSalchicha;

    List<EffectsEnum> effectsLeft = new List<EffectsEnum>();

    private void Start()
    {
        cone.gameObject.SetActive(false);
        leftClaw.gameObject.SetActive(false);
        rightClaw.gameObject.SetActive(false);
        flotador.gameObject.SetActive(false);
        duck1.gameObject.SetActive(false);
        duck2.gameObject.SetActive(false);

        for (int i=1; i<=NEffects; i++)
        {
            if (Random.Range(0, 2) > 0)
                effectsLeft.Add((EffectsEnum)i);
            else
                effectsLeft.Insert(0, (EffectsEnum)i);
        }
    }

    void TriggerMusicChanges()
    {
        Debug.Log(GameProgress());
        if (GameProgress() > terrorFunnyMusicChange)
        {
            if (GameProgress() < funnyMusicChange)
            {
                MusicFlow.Instance.TryPlayMusic(MusicTrackNames.TerrorFunny);
            }
            else
            {
                MusicFlow.Instance.TryPlayMusic(MusicTrackNames.Funny);
            }
        }
    }

    float GameProgress()
    {
        return 1f - (float)effectsLeft.Count / (float)NEffects;
    }

    public void ApplyRandomEffect()
    {
        var particles = Instantiate(particlesPrefab);
        particles.transform.position = transform.position;

        EffectsEnum effect;
        if (effectsLeft.Count == 0) effect = EffectsEnum.NONE;
        else
        {
            effect = effectsLeft.First();
            effectsLeft.RemoveAt(0);
        }
        
        TriggerMusicChanges();

        switch (effect)
        {
            case EffectsEnum.NONE:
                head.localScale += Vector3.one * 1.5f;
                break;
            case EffectsEnum.SHORTLEGS:
                leftLeg.localScale = Vector3.one * 0.6f;
                rightLeg.localScale = Vector3.one * 0.6f;

                hasLegsEffect = true;

                if (hasSmolEffect) enemyModel.transform.localPosition = Vector3.down * 0.63f;
                else enemyModel.transform.localPosition = Vector3.down * 0.415f;

                EnemyBehaviour.Instance.ReduceFrenzySpeed();
                // H: 0.35
                // H: 0.14
                break;
            case EffectsEnum.NO_WEAPON_1:
                leftClaw.gameObject.SetActive(true);
                hasSalchicha = true;
                EnemyBehaviour.Instance.ReduceDamage();
                break;
            case EffectsEnum.NO_WEAPON_2:
                rightClaw.gameObject.SetActive(true);
                hasSalchicha = true;
                EnemyBehaviour.Instance.ReduceDamage();
                break;
            case EffectsEnum.RIDICUOLOUS:
                head.localScale = Vector3.one * 2.5f;
                break;
            case EffectsEnum.SMOL:
                hips.localScale = Vector3.one * 0.7f;

                hasSmolEffect = true;
                if (hasLegsEffect) enemyModel.transform.localPosition = Vector3.down * 0.63f;
                else enemyModel.transform.localPosition = Vector3.down * 0.32f;

                EnemyBehaviour.Instance.ReduceFrenzySpeed();
                //EnemyBehaviour.Instance.ReduceDamage();
                // H: 0.54
                // H: 0.14
                break;
            case EffectsEnum.CONO:
                cone.gameObject.SetActive(true);
                EnemyBehaviour.Instance.ReduceFrenzySpeed();
                break;
            case EffectsEnum.FLOTADOR:
                flotador.gameObject.SetActive(true);
                break;
            case EffectsEnum.DUCK:
                duck1.gameObject.SetActive(true);
                duck2.gameObject.SetActive(true);
                stepsSoudns.SetSoundsCollection(duckSounds);
                stepsSoudns.UseMaxSoundTime = true;
                stepsSoudns.MaxSoundTime = 0.5f;
                break;
            default:
                break;
        }
    }
}
