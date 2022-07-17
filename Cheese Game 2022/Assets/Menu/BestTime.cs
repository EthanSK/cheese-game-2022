using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETGgames.CheeseGame.Extensions;
using TMPro;
using System;

public class BestTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _bestTimeText;


    public void Init(Cheese cheese)
    {
        var curBestTime = PlayerPrefs.GetInt(cheese.GetId(), int.MaxValue);
        _bestTimeText.text = $"Best time: {TimeSpan.FromMilliseconds(curBestTime).ToHumanReadableString()}";

    }
}
