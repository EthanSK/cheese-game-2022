using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int CurDiceFace => _curDiceFace;

    [SerializeField] private List<Sprite> _diceFaces;
    [SerializeField] private SpriteRenderer _diceSpriteRenderer;

    private int _curDiceFace = 1;


    private void Start()
    {
        InitDice();
    }

    private void InitDice()
    {
        _curDiceFace = Random.Range(1, 7);
        SetDiceFace();
    }

    private void SetDiceFace()
    {
        _curDiceFace = (int)Mathf.Floor((transform.localRotation.eulerAngles.z % 360f) / (360f / (float)_diceFaces.Count)) + 1;
        _diceSpriteRenderer.sprite = _diceFaces[_curDiceFace - 1];
    }

}
