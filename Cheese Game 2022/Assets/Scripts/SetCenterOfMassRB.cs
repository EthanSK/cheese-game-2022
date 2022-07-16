using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETGgames.CheeseGame.Extensions;
using ETGgames.Extensions;

public class SetCenterOfMassRB : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        SetCenterOfMass();
    }


    private void HandlePlayModeChange(bool isInPlayMode)
    {
        if (isInPlayMode)
            SetCenterOfMass();
    }

    public void SetCenterOfMass()
    {
        Vector2 posToUse = transform.localPosition;
        posToUse.x *= _rigidbody.transform.IsFlippedHoriz() ? -1f : 1f; //because for some weird reason unity can't handle setting the center of mass correctly if the x is flipped...it still thinks its tryna do it to the not flipped RB...https://trello.com/c/4EWEuOa6
        _rigidbody.centerOfMass = posToUse;
    }

}
