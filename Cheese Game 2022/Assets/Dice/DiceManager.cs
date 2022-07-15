using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETGgames.Extensions;
using ETGgames.Utils;

public class DiceManager : SingletonMonoBehaviour<DiceManager>
{
    [System.NonSerialized] public List<Dice> Dices = new List<Dice>();


    public void AddDice(Dice dice)
    {
        Dices.Add(dice);
    }

    public void RemoveDice(Dice dice)
    {
        Dices.Remove(dice);
    }

}
