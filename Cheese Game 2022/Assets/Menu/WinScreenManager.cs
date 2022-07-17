using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ETGgames.CheeseGame.Extensions;
using System;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeTaken;
    [SerializeField] private BestTime _bestTime;

    public static TimeSpan TimeTaken;


    void Start()
    {
        _timeTaken.text = $"Time taken: {TimeTaken.ToHumanReadableString()}";
        _bestTime.Init(LevelManager.Instance.CurCheese);
    }

}
