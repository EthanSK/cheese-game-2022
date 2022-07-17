using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private void Awake()
    {
        LevelManager.CheesePrefab = null;
        HowToPlayManager.CheesePrefabForNextScene = null;
    }

    public void HandleLevelClick()
    {
        SceneManager.LoadScene(Constants.SceneNames.LevelsMenu);
    }

    public void HandleCreditsClick()
    {
        SceneManager.LoadScene(Constants.SceneNames.Credits);
    }

    public void HandleHowToPlayClick()
    {
        SceneManager.LoadScene(Constants.SceneNames.HowToPlay);
    }
}
