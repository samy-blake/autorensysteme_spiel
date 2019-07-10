using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro

public class UI_LevelTime : MonoBehaviour
{

    private TextMeshProUGUI textComponent; // UI-Version von TMPro

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    //private void Start()
    //{
    //    LevelManager.OnTime += ChangeTime;
    //}

    private void OnEnable()
    {
        //LevelManager.OnTime += ChangeTime;
        string playTime = LevelManager.Instance.PlayTime.ToString();
        textComponent.SetText($"Du hast für das Level {playTime}sec Gebraucht.");
    }

    private void OnDisable()
    {
        //LevelManager.OnTime -= ChangeTime;
    }

    //private void ChangeTime()
    //{
    //    string playTime = LevelManager.Instance.PlayTime.ToString();
    //    textComponent.SetText($"Du hast für das Level {playTime}sec Gebraucht.");
    //}
}
