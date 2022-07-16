using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    [SerializeField] private List<Sprite> _diceFaces;

    private SpriteRenderer _spriteRenderer;

    private int _curDiceFace = 1;

    public bool IsDead { get; private set; } = false;

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
        SetDiceFace();
    }

    private void SetDiceFace()
    {
        _curDiceFace = (int)Mathf.Floor((transform.localRotation.eulerAngles.z % 360f) / (360f / (float)_diceFaces.Count)) + 1;
        _spriteRenderer.sprite = _diceFaces[_curDiceFace - 1];
    }
}
