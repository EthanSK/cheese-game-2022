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
        var horizInput = Input.GetAxis("Horizontal");
        if (Mathf.Approximately(horizInput, 0f))
        {
            horizInput = LevelManager.Instance.MobileHorizontalInput;
        }
        var rotAmt = horizInput * _rotateSpeed * -1f * Time.deltaTime;

        transform.Rotate(new Vector3(0f, 0f, rotAmt));

        if (_iveGoneToTakeAPissMode)
        {
            rotAmt = _rotateSpeed * -1f * Time.deltaTime;

            transform.Rotate(new Vector3(0f, 0f, rotAmt));

        }
    }
}
