using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro

public class Ui_HealthDisplay : MonoBehaviour {

    public IntValue health;
    private TextMeshProUGUI textComponent; // UI-Version von TMPro

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ChangeText(health.RuntimeValue);
    }

    private void OnEnable()
    {
        health.OnValueChanged += ChangeText;
    }

    private void OnDisable()
    {
        health.OnValueChanged -= ChangeText;
    }

    private void ChangeText(int liveCount)
    {
        textComponent.SetText(liveCount.ToString());
    }
}
