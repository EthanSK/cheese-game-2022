using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelPreview : MonoBehaviour
{

    [SerializeField] private Cheese _cheesePrefab;
    [SerializeField] private string _name;
    [SerializeField] private TextMeshProUGUI _nameText;

    private void Awake()
    {
        GetComponent<Image>().sprite = _cheesePrefab.GetComponent<SpriteRenderer>().sprite;
    }

    private void Start()
    {
        GetComponentInChildren<BestTime>().Init(_cheesePrefab);
        _nameText.text = _name;
    }

    public void HandleClick()
    {
        LevelManager.CheesePrefab = _cheesePrefab;
        SceneManager.LoadScene(Constants.SceneNames.Level);
    }
}
