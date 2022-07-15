using UnityEngine;
using System.Linq;
using System.Collections;

public class SingleplayerCameraController : MonoBehaviour
{
    public Camera Camera => _camera;
    public float ExtraSizeOffset { get; set; } = 0f;
    [SerializeField] private Vector2 _posOffset = new Vector2(0, 0.3f);
    [SerializeField] private float _followSmoothness = 0.03f;
    [SerializeField] private float _horizontalSmoothnessLookOffset = 0.04f;
    [SerializeField] private float _zoomSmoothnessFixed = 0.06f;

    private float _zoomSmoothness;
    [SerializeField] private float _horizontalPlayerLookOffset = 1.5f;
    [SerializeField] private float _zoomedOutCameraSize = 2.5f;

    [SerializeField] private float _idleCameraSize = 1.75f;
    [SerializeField] private Camera _camera; //serialize so we can drag the level build camera instead
    private float _speedConsideredIdle;
    private Rigidbody2D _mainObjectToFollow; //not set in inspector because should be used on any scene

    private void Awake()
    {
        _zoomSmoothness = _zoomSmoothnessFixed;

    }

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        _mainObjectToFollow = DiceManager.Instance.AliveDices.ElementAt(0).GetComponent<Rigidbody2D>();
        _speedConsideredIdle = 0f;
        StartCoroutine(TemporarilySpeedUpSmoothness());
    }

    private IEnumerator TemporarilySpeedUpSmoothness() //so it d oesn't take forever to reach the correct size in say level builder
    {
        _zoomSmoothness = 5f;
        yield return new WaitForSeconds(1f);
        _zoomSmoothness = _zoomSmoothnessFixed; //can't set a temp var locally becaus eif this is called while the coroutine is running it wil be set to the wrong thing
    }
    private void LateUpdate()
    {
        SetPosition();
        SetCameraSize();
    }

    float currentHorizontalLookOffset = 0;
    Vector2 velocityPos = Vector2.zero;

    private void SetPosition()
    {
        Vector2 targetPosition = (Vector2)_mainObjectToFollow.transform.position + _posOffset;
        Vector2 position = Camera.transform.position;
        position.x -= currentHorizontalLookOffset; //otherwise mad jitter

        Vector2 smoothedPosition = Vector2.SmoothDamp(position, targetPosition, ref velocityPos, _followSmoothness); //ALREADY ACCOUNTS FOR TIME.DELTATIME SEE DOCS //for position, smooth damp reduces jitter

        //apply horizontal look offset much more slowly
        float horizontalOffset = _mainObjectToFollow.transform.lossyScale.x > 0 ? _horizontalPlayerLookOffset : -_horizontalPlayerLookOffset; //we don't use trasnform.isflippeud coz we wanna check lossy scale here

        currentHorizontalLookOffset = Mathf.Lerp(currentHorizontalLookOffset, horizontalOffset, _horizontalSmoothnessLookOffset * Time.deltaTime);

        smoothedPosition.x += currentHorizontalLookOffset;

        Camera.transform.position = smoothedPosition; //not necessarily self transfr (eg in level builder)

    }


    private void SetCameraSize()
    {
        float verticalDistance = Mathf.Abs(((Vector2)_mainObjectToFollow.transform.position + _posOffset).y - Camera.transform.position.y); //cam transform not necessarily this transform (level builder eg)

        float smoothedSize;
        if (Mathf.Abs(_mainObjectToFollow.velocity.x) > _speedConsideredIdle)
        {
            smoothedSize = Mathf.Lerp(_camera.orthographicSize, ExtraSizeOffset + _zoomedOutCameraSize + verticalDistance, _zoomSmoothness * Time.deltaTime);
        }
        else
        {
            smoothedSize = Mathf.Lerp(_camera.orthographicSize, ExtraSizeOffset + _idleCameraSize + verticalDistance, _zoomSmoothness * Time.deltaTime);
        }

        _camera.orthographicSize = smoothedSize;
    }


}
