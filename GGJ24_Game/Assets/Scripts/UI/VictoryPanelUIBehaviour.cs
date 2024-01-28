using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPanelUIBehaviour : MonoBehaviour
{
    [SerializeField] GameObject container;

    [SerializeField] DoorSwitchBehaviour switchBehaviour;

    private void Awake()
    {
        // TODO: Suscribe to OnWin event
        switchBehaviour.OnComplete += Show;
    }

    void Start()
    {
        container.SetActive(false);
    }

    void Show()
    {
        container.SetActive(true);
    }
}
