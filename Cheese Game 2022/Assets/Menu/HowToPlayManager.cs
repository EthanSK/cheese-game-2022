using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlayManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    public static Cheese CheesePrefabForNextScene;

    private void Start()
    {
        PlayerPrefs.SetInt(Constants.PlayerPrefs.HasDefaultShownHowToPlay, 1);

        if (CheesePrefabForNextScene != null)
        {
            _playButton.gameObject.SetActive(true);
        }
        else
        {
            _playButton.gameObject.SetActive(false);
        }
    }

    public void HandlePlayButtonClick()
    {
        SceneManager.LoadScene(Constants.SceneNames.Level);

    }
}
