using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    [SerializeField] private List<Sprite> _diceFaces;
    [SerializeField] private float _timeBetweenUpdatingDiceFace;
    [SerializeField] private DiceTracker _diceTrackerPrefab;
    [SerializeField] List<AudioClip> _bounceSounds;

    [SerializeField] AudioClip _deathSound;
    [SerializeField] private float _distanceAtMaxSound;
    private AudioSource _audioSource;

    private Vector2 _lastCollisionPos;

    private int _bounceSoundIdx = 0;

    private SpriteRenderer _spriteRenderer;

    private int _curDiceFace = 1;
    private float _audioSourceMaxSound;

    public bool IsDead { get; private set; } = false;

    private float _timeLastUpdatedDice = 0f;
    private float _timeHorizInputLastStarted;
    private DiceTracker _diceTracker;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _curDiceFace = Random.Range(1, 7);
        _audioSource = GetComponent<AudioSource>();
        _audioSourceMaxSound = _audioSource.volume;

    }

    private void Start()
    {
        _lastCollisionPos = transform.position;
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
        if (_diceTracker != null)
            Destroy(_diceTracker.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var distance = Vector2.Distance(transform.position, _lastCollisionPos);
        if (other.gameObject.GetComponent<Cheese>())
        {
            var vol = (distance / _distanceAtMaxSound) * _audioSourceMaxSound;
            _audioSource.PlayOneShot(_bounceSounds[_bounceSoundIdx], vol);
            _bounceSoundIdx = (_bounceSoundIdx + 1) % _bounceSounds.Count;
        }

        _lastCollisionPos = transform.position;

    }

    public void OnOverlapDiceOnEnemy(DiceOnEnemy diceOnEnemy)
    {
        if (diceOnEnemy)
        {

            if (diceOnEnemy.CurDiceFace == _curDiceFace)
            {
                //spawn another dice
                var newDice = Instantiate(this, diceOnEnemy.NewDiceSpawnPos.position, diceOnEnemy.NewDiceSpawnPos.rotation, transform.parent);
                newDice._curDiceFace = _curDiceFace;
                IncrementDiceFace();
                diceOnEnemy.IsPickedUp = true;
                Destroy(diceOnEnemy.gameObject);
                LevelManager.Instance.StopPlayingLevelIfNeeded();
            }
            else
            {
                AudioManager.Instance.AudioSource.PlayOneShot(_deathSound, 1f);
                Destroy(gameObject);

            }
        }
    }

    private void IncrementDiceFace()
    {
        _curDiceFace = (_curDiceFace + 1) % 7;
        if (_curDiceFace == 0) _curDiceFace = 1;
    }

    private void DecrementDiceFace()
    {
        _curDiceFace = (_curDiceFace - 1) % 7;
        if (_curDiceFace == 0) _curDiceFace = 6;
    }

    private void Update()
    {

        var horizInput = Input.GetAxis("Horizontal");

        if (Mathf.Approximately(horizInput, 0f))
        {
            horizInput = LevelManager.Instance.MobileHorizontalInput;
        }
        if (Input.GetKeyDown("left") || Input.GetKeyDown("right"))
        {
            _timeHorizInputLastStarted = Time.time;
        }

        if ((Time.time - _timeLastUpdatedDice > _timeBetweenUpdatingDiceFace) &&
        (Time.time - _timeHorizInputLastStarted > _timeBetweenUpdatingDiceFace))
        {

            if (horizInput > 0f)
            {
                IncrementDiceFace();

                _timeLastUpdatedDice = Time.time;
            }

            if (horizInput < 0f)
            {
                DecrementDiceFace();

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
