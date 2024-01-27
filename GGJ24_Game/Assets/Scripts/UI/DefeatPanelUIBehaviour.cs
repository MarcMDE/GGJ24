using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatPanelUIBehaviour : MonoBehaviour
{
    [SerializeField] GameObject container;

    PlayerHP playerHP;

    private void Awake()
    {
        playerHP = Player.Instance.GetComponent<PlayerHP>();
    }

    void Start()
    {
        playerHP.OnDead += Show;
        container.SetActive(false);
    }

    void Show()
    {
        container.SetActive(true);
    }
}
