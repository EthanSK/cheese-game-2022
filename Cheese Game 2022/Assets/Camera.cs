using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }


}
