using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPanelUIBehaviour : MonoBehaviour
{
    [SerializeField] GameObject container;

    private void Awake()
    {
        // TODO: Suscribe to OnWin event
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
