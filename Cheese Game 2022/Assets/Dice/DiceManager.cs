using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETGgames.Extensions;
using ETGgames.Utils;
using System.Linq;
using System;

public class DiceManager : SingletonMonoBehaviour<DiceManager>
{
    [System.NonSerialized] public List<Dice> Dices = new List<Dice>();
    public IEnumerable<Dice> AliveDices => Dices.Where(d => !d.IsDead);


    public event Action<Dice> OnDiceChange = delegate { }; //triggers for player added, player removed, and player death


    public void AddDice(Dice dice)
    {
        Dices.Add(dice);
        HandleDiceChange(dice);
    }

    public void RemoveDice(Dice dice)
    {
        Dices.Remove(dice);
        HandleDiceChange(dice);
    }

    private void HandleDiceChange(Dice dice)
    {
        OnDiceChange(dice);

    }


}
