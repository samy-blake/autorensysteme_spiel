using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro

public class UI_GemDisplay : MonoBehaviour
{
    private TextMeshProUGUI textComponent; // UI-Version von TMPro

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ChangeText();
    }

    private void OnEnable()
    {
        LevelManager.OnChangedGemCount += ChangeText;
    }

    private void OnDisable()
    {
        LevelManager.OnChangedGemCount -= ChangeText;
    }

    private void ChangeText()
    {
        // D3: 1 => 001
        string collectedCount = LevelManager.Instance.CollectedGems.ToString("D3");
        string maxCount = LevelManager.Instance.GemCountInLevel.ToString(("D3"));
        textComponent.SetText($"{collectedCount}/{maxCount}");
    }
}
