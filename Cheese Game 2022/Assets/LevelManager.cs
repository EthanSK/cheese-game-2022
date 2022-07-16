using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETGgames.Extensions;
using ETGgames.Utils;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{

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
}
