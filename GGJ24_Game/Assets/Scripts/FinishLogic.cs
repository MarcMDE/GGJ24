using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLogic : MonoBehaviour
{
    [SerializeField] DoorSwitchBehaviour doorSwitch;

    private void Awake()
    {
        Player.Instance.GetComponent<PlayerHP>().OnDead += Lose;
        doorSwitch.OnComplete += Win;
    }
    
    void Lose()
    {
        Invoke("ReloadScene", 4f);
    }

    void Win()
    {
        Invoke("ReloadScene", 4f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
