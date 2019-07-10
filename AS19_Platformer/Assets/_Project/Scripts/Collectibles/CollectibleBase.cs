using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract bedeutet, dass wir es nicht direkt auf ein GameObject ziehen können.
// Stattdessen müssen wir die Kinder verwenden. Die Kinder müssen außerdem alle Klassen implementieren,
// die ebenfalls abstract sind.
// (Wir inheriten hier übrigens von MonoBehaviour, damit wir z.B. auf OnTriggerEnter2D zugreifen können.)
public abstract class CollectibleBase : MonoBehaviour
{
    // SerializeField ermöglicht uns, auch protected/private Variablen im Inspector zu sehen und einzustellen.
    [SerializeField] protected bool destroyOnPickup;
    
    // Wir definieren keine Funktionalität für Pickup, sondern bestehen darauf, dass das Kind es macht.
    protected abstract void Pickup();

    // Das hier ist der Standardwert für CanPickup(). Ein Kind kann ihn überschreiben oder erweitern.
    protected virtual bool CanPickup()
    {
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && CanPickup())
        {
            Pickup();
            if (destroyOnPickup)
            {
                // Destroy oder disable sind beides okay—kommt drauf an, was wir damit vorhaben.
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
