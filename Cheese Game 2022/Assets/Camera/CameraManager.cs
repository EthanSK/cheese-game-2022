using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ETGgames.Utils;
using ETGgames.YOMG2.Extensions;

public class CameraManager : SingletonMonoBehaviour<CameraManager>
{
    [SerializeField] private MultiplayerCameraController _multiplayerCam;
    [SerializeField] private SingleplayerCameraController _singlePlayerCam;


    private void Start()
    {
        SetCorrectCamera();
    }


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

    private void HandlePlayModeChange(bool isInPlayMode)
    {
        _singlePlayerCam.ExtraSizeOffset = 0f;
        _multiplayerCam.ExtraSizeOffset = Vector2.zero;
    }

    private void HandleDiceChange(Dice dice)
    {
        SetCorrectCamera();
    }

    public void SetCorrectCamera()
    {
        int numAliveDices = DiceManager.Instance.AliveDices.Count();

        if (numAliveDices > 1)
        {
            _multiplayerCam.enabled = true;
            _singlePlayerCam.enabled = false;
            _multiplayerCam.Setup();
        }
        else if (numAliveDices == 1)
        {
            _multiplayerCam.enabled = false;
            _singlePlayerCam.enabled = true;
            _singlePlayerCam.Setup();
        }
        else
        {
            //player has lost
            _multiplayerCam.enabled = false;
            _singlePlayerCam.enabled = false;
        }
    }

    public void IncreaseCameraSizeOffset(float amountToIncreaseBy) //increase and not set because other things can additively have an effect on size
    {
        _singlePlayerCam.ExtraSizeOffset += amountToIncreaseBy;
        _multiplayerCam.ExtraSizeOffset += new Vector2(amountToIncreaseBy, amountToIncreaseBy);
    }

}
