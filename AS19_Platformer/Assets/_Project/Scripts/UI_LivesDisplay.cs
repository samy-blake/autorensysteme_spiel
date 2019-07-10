using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro

public class UI_LivesDisplay : MonoBehaviour
{
    public IntValue lives;
    private TextMeshProUGUI textComponent; // UI-Version von TMPro
    
    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ChangeText(lives.RuntimeValue);
    }

    private void OnEnable()
    {
        lives.OnValueChanged += ChangeText;
    }

    private void OnDisable()
    {
        lives.OnValueChanged -= ChangeText;
    }

    private void ChangeText(int liveCount)
    {
        // D2: 1 => 01
        textComponent.SetText(liveCount.ToString("D2"));
    }
}
