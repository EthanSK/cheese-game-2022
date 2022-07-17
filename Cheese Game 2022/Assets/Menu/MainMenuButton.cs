using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void HandlePress()
    {
        if (gameObject.scene.name == Constants.SceneNames.WinScreen)
        {
            SceneManager.UnloadSceneAsync(gameObject.scene);

        }
        SceneManager.LoadSceneAsync(Constants.SceneNames.Menu);
    }
}
