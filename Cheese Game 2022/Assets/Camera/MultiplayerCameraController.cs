
using UnityEngine;
using ETGgames.Extensions;
using System.Linq;

public class MultiplayerCameraController : MonoBehaviour
{
    public Camera Camera => _camera;
    public Vector2 ExtraSizeOffset { get; set; } = Vector2.zero;

    [SerializeField] private Vector2 _posOffset;
    [SerializeField] private Vector2 _sizeOffset;

    [SerializeField] private float _followSmoothness;
    [SerializeField] private float _cameraPadding;
    [SerializeField] private float _zoomSmoothness;
    [SerializeField] private float _minCameraSize;

    [SerializeField] private Camera _camera; //serialize for same reason as single player cam - to drag levle builder cam instead
    private Transform[] _objectsToFollow;



    private void Start()
    {
        Setup();
    }

    //OnEnable because more players can be added in level builder. its called whenever we enter playmode
    private bool _wasEnabledBeforeEnabledStateChange = true; //starts active and enabled by default
    private void OnEnable()
    {
        if (!_wasEnabledBeforeEnabledStateChange) Setup();
        _wasEnabledBeforeEnabledStateChange = true;
    }
    private void OnDisable()
    {
        _wasEnabledBeforeEnabledStateChange = false;
    }
    public void Setup()
    {
        _objectsToFollow = DiceManager.Instance.AliveDices.Select(el => el.transform).ToArray();
    }
    private void LateUpdate()
    {
        SetCamera();
    }

    private void SetCamera()
    {
        float CalcCamSizeForWidth(float width)
        {
            float heightNeededForTargetWidth = width / _camera.aspect;
            return CalcCamSizeForHeight(heightNeededForTargetWidth);
        }

        float CalcCamSizeForHeight(float height)
        {
            return height / 2f;
        }

        Rect boundingBox = _objectsToFollow.CalculateBoundingBox(_posOffset, _sizeOffset + ExtraSizeOffset);
        Vector2 targetPosition = boundingBox.position + boundingBox.size / 2;
        Camera.transform.position = Vector2.Lerp(Camera.transform.position, targetPosition, _followSmoothness * Time.deltaTime); //camera transform not necessarily this transform (eg level builder)

        //calc size when we both set it to bb height, and bb width, and choose the larger of the two sizes

        float sizeIfUsingBBWidth = CalcCamSizeForWidth(boundingBox.size.x);
        float sizeIfUsingBBHeight = CalcCamSizeForHeight(boundingBox.size.y);

        float sizeToUse = Mathf.Max(sizeIfUsingBBWidth + _cameraPadding, sizeIfUsingBBHeight + _cameraPadding, _minCameraSize);
        float smoothedSize = Mathf.Lerp(_camera.orthographicSize, sizeToUse, _zoomSmoothness * Time.deltaTime);
        _camera.orthographicSize = smoothedSize;
    }

}
