using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    private void Awake()
    {

    }
    private void Start()
    {
        GetComponentInChildren<DiceOnEnemy>().InitDice();
    }


}
