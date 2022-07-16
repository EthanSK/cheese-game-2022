using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETGgames.Utils;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _inGameMusic;
    public override bool IsScenePersistent => true;

    private AudioSource _audioSource;


    protected override void OnAwake()
    {
        _audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;


    }

    private void Start()
    {
        SetMusic();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Constants.SceneNames.Menu || scene.name == Constants.SceneNames.Level)
        {
            SetMusic();

        }

    }

    private void SetMusic()
    {
        if (SceneManager.GetActiveScene().name == Constants.SceneNames.Menu)
        {
            _audioSource.clip = _menuMusic;
            _audioSource.volume = 0.5f;
        }
        if (SceneManager.GetActiveScene().name == Constants.SceneNames.Level)
        {
            _audioSource.clip = _inGameMusic;
            _audioSource.volume = 0.17f;
        }

        _audioSource.Play();
    }
}
