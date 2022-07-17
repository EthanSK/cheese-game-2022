using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPreview : MonoBehaviour
{

    [SerializeField]
    Cheese _cheesePrefab;

    private void Awake()
    {
        GetComponent<Image>().sprite = _cheesePrefab.GetComponent<SpriteRenderer>().sprite;
    }

    private void Start()
    {
        GetComponentInChildren<BestTime>().Init(_cheesePrefab);
    }

    public void HandleClick()
    {
        LevelManager.CheesePrefab = _cheesePrefab;
        SceneManager.LoadScene(Constants.SceneNames.Level);
    }
}
