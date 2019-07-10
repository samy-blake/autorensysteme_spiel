using UnityEngine;

// Da allowInput keine lokale Variable, sondern ein allgemeines ScriptableObject ist,
// können wir nicht in einer Property Korrekturen vornehmen, wenn sie beschrieben wird.
// Stattdessen können wir über ein Event abfragen, ob sie sich ändert, und dann unsere Adjustierungen vornehmen.
public class FreezeRbWhenInputDeactivated : MonoBehaviour
{
    public BoolValue allowInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        allowInput.OnValueChanged += UpdateRigidbody;
    }

    private void OnDisable()
    {
        allowInput.OnValueChanged -= UpdateRigidbody;
    }

    private void UpdateRigidbody(bool value)
    {
        if (value)
            return;
	
        rb.velocity = Vector2.zero;
    }
}
