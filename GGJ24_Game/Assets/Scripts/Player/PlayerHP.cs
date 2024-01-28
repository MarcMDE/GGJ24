using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float recoveringDelay;
    [SerializeField] float recoverPerSec;

    public event UnityAction<float> OnSufferDamage;
    public event UnityAction OnDead;
    public event UnityAction<float> OnRecovering;

    float currentHP;

    public float HP => currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void SufferDamage(float d)
    {
        currentHP -= d;

        StopCoroutine(RecoverHPCoroutine());

        if (currentHP < 0f)
        {
            currentHP = 0f;
            OnDead?.Invoke();
        }
        else
        {
            StartCoroutine(RecoverHPCoroutine());
            OnSufferDamage?.Invoke(currentHP/maxHP);
        }
    }

    IEnumerator RecoverHPCoroutine()
    {
        yield return new WaitForSeconds(recoveringDelay);

        float elapsedTime = 0f;
        float recoverDuration = (maxHP - currentHP) / recoverPerSec;
        float initialHp = currentHP;

        while (elapsedTime < recoverDuration)
        {
            elapsedTime += Time.deltaTime;
            currentHP = Mathf.Lerp(initialHp, maxHP, elapsedTime / recoverDuration);
            OnRecovering?.Invoke(currentHP / maxHP);
            yield return null;
        }

    }
}
