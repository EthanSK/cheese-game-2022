using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTracker : MonoBehaviour
{
    private Dice _dice;


    public static DiceTracker Create(DiceTracker prefab, Dice dice, Transform parent)
    {
        var diceTracker = Instantiate(prefab, parent);
        diceTracker._dice = dice;
        diceTracker.SetPose();
        return diceTracker;
    }

    void Update()
    {
        SetPose();
    }

    private void SetPose()
    {
        transform.position = _dice.transform.position;
        transform.rotation = _dice.transform.rotation;
    }





    private void OnTriggerEnter2D(Collider2D other)
    {
        var diceOnEnemy = other.GetComponent<DiceOnEnemy>();
        if (diceOnEnemy)
        {
            _dice.OnOverlapDiceOnEnemy(diceOnEnemy);

        }
    }

}
