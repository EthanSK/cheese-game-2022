using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateIfNotInDevMode : MonoBehaviour
{
    private void Awake()
    {
        if (!GameManager.Instance.IsDev)
        {
            gameObject.SetActive(false);
        }
    }
}
