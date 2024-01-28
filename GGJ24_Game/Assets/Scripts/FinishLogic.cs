using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLogic : MonoBehaviour
{
    [SerializeField] DoorSwitchBehaviour doorSwitch;

    bool hasFinished = false;

    private void Awake()
    {
        hasFinished = false;
        Player.Instance.GetComponent<PlayerHP>().OnDead += Lose;
        doorSwitch.OnComplete += Win;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void Lose()
    {
        if (hasFinished) return;

        Player.Instance.GetComponent<PlayerController>().enabled = false;
        hasFinished = true;
        Invoke("ReloadScene", 4f);
    }

    void Win()
    {
        if (hasFinished) return;
        hasFinished = true;
        EnemyBehaviour.Instance.enabled = false;
        Invoke("GoToMenu", 4f);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
