using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETGgames.Utils;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using System;
using ETGgames.CheeseGame.Extensions;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    [SerializeField] private TextMeshProUGUI _timerText;

    public DateTimeOffset StartTimePlayLevel { get; private set; } = DateTimeOffset.MinValue;
    public DateTimeOffset EndTimePlayLevel { get; private set; } = DateTimeOffset.MaxValue;

    public TimeSpan TimeTakenToWin => EndTimePlayLevel - StartTimePlayLevel;

    public TimeSpan TimeTakenSoFar => DateTimeOffset.Now - StartTimePlayLevel;

    public bool IsPlayingLevel = false;

    public float MobileHorizontalInput = 0f;


    private void OnEnable()
    {
        DiceManager.Instance.OnDiceChange += HandleDiceChange;
    }

    private void OnDisable()
    {
        if (DiceManager.Exists)
        {
            DiceManager.Instance.OnDiceChange -= HandleDiceChange;
        }
    }

    private void Start()
    {
        StartPlayingLevel();
    }

    public void StartPlayingLevel()
    {
        IsPlayingLevel = true;
        StartTimePlayLevel = DateTimeOffset.Now;
    }

    public void StopPlayingLevel()
    {
        IsPlayingLevel = false;
        EndTimePlayLevel = DateTimeOffset.Now;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }

    private void HandleDiceChange(Dice dice)
    {
        if (DiceManager.Instance.AliveDices.Count() == 0)
        {
            RestartLevel();
        }
    }

    public void HandleResetClick()
    {
        RestartLevel();
    }

    private void Update()
    {
        _timerText.text = TimeTakenSoFar.ToHumanReadableString();
    }

    public void HandleLeftSideOfScreenPress()
    {
        MobileHorizontalInput = -1f;
    }

    public void HandleRightSideOfScreenPress()
    {
        MobileHorizontalInput = 1f;
    }

    public void HandleLeftSideOfScreenUnPress()
    {
        if (!Mathf.Approximately(MobileHorizontalInput, 1f))
        {
            MobileHorizontalInput = 0f;
        }
    }

    public void HandleRightSideOfScreenUnPress()
    {
        if (!Mathf.Approximately(MobileHorizontalInput, -1f))
        {
            MobileHorizontalInput = 0f;
        }
    }
}
