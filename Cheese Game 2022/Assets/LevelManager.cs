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
    public static Cheese CheesePrefab; // the level to be loaded

    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Transform _worldObj;

    public DateTimeOffset StartTimePlayLevel { get; private set; } = DateTimeOffset.MinValue;
    public DateTimeOffset EndTimePlayLevel { get; private set; } = DateTimeOffset.MaxValue;

    public TimeSpan TimeTakenToWin => EndTimePlayLevel - StartTimePlayLevel;

    public TimeSpan TimeTakenSoFar => DateTimeOffset.Now - StartTimePlayLevel;

    public bool IsPlayingLevel = false;

    public float MobileHorizontalInput = 0f;

    public Cheese CurCheese;


    private void OnEnable()
    {
        DiceManager.Instance.OnDiceChange += HandleDiceChange;

        if (CheesePrefab == null)
        {
            var cheese = _worldObj.GetComponentInChildren<Cheese>();
            cheese.gameObject.SetActive(true);
            CurCheese = cheese;
        }
        else
        {
            CurCheese = Instantiate(CheesePrefab, _worldObj);

        }
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

    public void StopPlayingLevelIfNeeded()
    {
        if (!CurCheese.AreThereStillAnyEnemiesWithDice())
        {
            StopPlayingLevel();
        }
    }

    public void StopPlayingLevel()
    {
        IsPlayingLevel = false;
        EndTimePlayLevel = DateTimeOffset.Now;
        WinScreenManager.TimeTaken = TimeTakenToWin;
        var curBestTime = PlayerPrefs.GetInt(CheesePrefab.GetId(), int.MaxValue);
        //Level Id: Dice-(0.35, -4.69, 0.00);Dice (1)-(5.30, 2.88, 0.00);Dice (2)-(-3.64, 4.07, 0.00);Rat (1)-(3.91, 5.93, 0.12);

        if (TimeTakenToWin.TotalMilliseconds < curBestTime)
        {
            Debug.Log("Best time beaten!");
            Debug.Log("Level Id: " + CheesePrefab.GetId());
            PlayerPrefs.SetInt(CheesePrefab.GetId(), (int)TimeTakenToWin.TotalMilliseconds);
        }

        SceneManager.LoadScene(Constants.SceneNames.WinScreen, LoadSceneMode.Additive);
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
