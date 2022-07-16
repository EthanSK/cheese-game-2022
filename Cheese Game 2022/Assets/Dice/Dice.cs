using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    [SerializeField] private List<Sprite> _diceFaces;
    [SerializeField] private float _timeBetweenUpdatingDiceFace;

    private SpriteRenderer _spriteRenderer;

    private int _curDiceFace = 1;

    public bool IsDead { get; private set; } = false;

    private float _timeLastUpdatedDice = 0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

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

    private void Update()
    {
        if (Time.time - _timeLastUpdatedDice > _timeBetweenUpdatingDiceFace)
        {
            var horizInput = Input.GetAxis("Horizontal");

            if (Mathf.Approximately(horizInput, 1f))
            {
                _curDiceFace = (_curDiceFace + 1) % 7;
                if (_curDiceFace == 0) _curDiceFace = 1;

                _timeLastUpdatedDice = Time.time;
            }

            if (Mathf.Approximately(horizInput, -1f))
            {
                _curDiceFace = (_curDiceFace - 1) % 7;
                if (_curDiceFace == 0) _curDiceFace = 6;

                _timeLastUpdatedDice = Time.time;
            }

            SetDiceFace();
        }

    }

    private void SetDiceFace()
    {
        //this is to set the dice face based on rotation of the dice
        // _curDiceFace = (int)Mathf.Floor((transform.localRotation.eulerAngles.z % 360f) / (360f / (float)_diceFaces.Count)) + 1;


        _spriteRenderer.sprite = _diceFaces[_curDiceFace - 1];
    }
}
