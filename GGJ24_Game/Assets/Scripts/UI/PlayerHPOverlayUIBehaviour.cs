using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPOverlayUIBehaviour : MonoBehaviour
{
    [SerializeField] Image image;

    PlayerHP playerHP;

    private void Awake()
    {
        playerHP = Player.Instance.GetComponent<PlayerHP>();
    }

    private void Start()
    {
        playerHP.OnSufferDamage += UpdateAlpha;
        playerHP.OnRecovering += UpdateAlpha;
    }

    private void UpdateAlpha(float v)
    {
        var color = image.color;
        color.a = 1f - v;
        image.color = color;
    }
}
