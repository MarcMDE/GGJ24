using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFun : MonoBehaviour
{

    public GameObject credits;
    public GameObject menu;
    public GameObject bgcredits;

    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");

    }

    public void Exit()
    {
        Application.Quit();

    }

    public void Credits()
    {
        bgcredits.SetActive(true);
        credits.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }

    public void CreditsExit()
    {
        bgcredits.SetActive(false);
        credits.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);

    }

}
