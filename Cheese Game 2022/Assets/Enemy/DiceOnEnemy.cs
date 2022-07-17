using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceOnEnemy : MonoBehaviour
{

    public Transform NewDiceSpawnPos => _newDiceSpawnPos;//use global position of this transform for pos
    public int CurDiceFace => _curDiceFace;
    public bool IsPickedUp = false;
    private SpriteRenderer _diceSpriteRenderer;
    [SerializeField] private List<Sprite> _diceFaces;
    [SerializeField] private Transform _newDiceSpawnPos;


    private int _curDiceFace = 1;


    private void Awake()
    {
        _diceSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void InitDice()
    {
        _curDiceFace = Random.Range(1, 7);
        SetDiceFace();
    }

    private void SetDiceFace()
    {
        _diceSpriteRenderer.sprite = _diceFaces[_curDiceFace - 1];
    }

}
