using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    [SerializeField] private List<Sprite> _diceFaces;
    [SerializeField] private float _timeBetweenUpdatingDiceFace;
    [SerializeField] private DiceTracker _diceTrackerPrefab;

    private SpriteRenderer _spriteRenderer;

    private int _curDiceFace = 1;

    public bool IsDead { get; private set; } = false;

    private float _timeLastUpdatedDice = 0f;
    private float _timeHorizInputLastStarted;
    private DiceTracker _diceTracker;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        DiceManager.Instance.AddDice(this);
        _diceTracker = DiceTracker.Create(_diceTrackerPrefab, this, transform.parent.parent);


    }
    private void OnDisable()
    {
        if (DiceManager.Exists) //might not exist when leaving the scene eg going to main menu from level builder
        {
            DiceManager.Instance.RemoveDice(this);
        }

        Destroy(_diceTracker.gameObject);
    }

    public void OnOverlapDiceOnEnemy(DiceOnEnemy diceOnEnemy)
    {
        if (diceOnEnemy)
        {
            Debug.Log("dice on trigger enter with onenemy");

            if (diceOnEnemy.CurDiceFace == _curDiceFace)
            {
                //spawn another dice
                Instantiate(this, transform.parent);
            }
            else
            {
                //destroy this dice
            }
        }
    }

    private void Update()
    {

        var horizInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown("left") || Input.GetKeyDown("right"))
        {
            _timeHorizInputLastStarted = Time.time;
        }

        if ((Time.time - _timeLastUpdatedDice > _timeBetweenUpdatingDiceFace) &&
        (Time.time - _timeHorizInputLastStarted > _timeBetweenUpdatingDiceFace))
        {

            if (horizInput > 0f)
            {
                _curDiceFace = (_curDiceFace + 1) % 7;
                if (_curDiceFace == 0) _curDiceFace = 1;

                _timeLastUpdatedDice = Time.time;
            }

            if (horizInput < 0f)
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
