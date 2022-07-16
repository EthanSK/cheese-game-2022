using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private bool _iveGoneToTakeAPissMode;

    private void Awake()
    {

    }


    void Update()
    {
        var rotAmt = Input.GetAxis("Horizontal") * _rotateSpeed * -1f * Time.deltaTime;

        transform.Rotate(new Vector3(0f, 0f, rotAmt));

        if (_iveGoneToTakeAPissMode)
        {
            transform.Rotate(new Vector3(0f, 0f, _rotateSpeed * -1f * Time.deltaTime));

        }
    }
}
