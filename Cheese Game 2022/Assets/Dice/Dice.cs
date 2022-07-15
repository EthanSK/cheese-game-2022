using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    public bool IsDead { get; private set; } = false;

    private void OnEnable()
    {
        DiceManager.Instance.AddDice(this);

    }
    private void OnDisable()
    {
        if (DiceManager.Exists) //might not exist when leaving the scene eg going to main menu from level builder
        {
            DiceManager.Instance.RemoveDice(this);
        }
    }
}
