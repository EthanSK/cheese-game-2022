using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnPlay : MonoBehaviour
{

    //convenience script to hide things I want to show in editor mode
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
