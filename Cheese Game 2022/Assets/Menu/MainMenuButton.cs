using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void HandleClick()
    {
        SceneManager.LoadScene(Constants.SceneNames.Menu);
    }
}
