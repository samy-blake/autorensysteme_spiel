using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry_Collectible : CollectibleBase
{
    public GameObject particles;

    // Hier drin können wir die Variablen des Parents überschreiben.
    private Cherry_Collectible()
    {
        destroyOnPickup = true;
    }

    // Beispiel: Überschreibung von CanPickup() -> können es nur einsammeln, wenn wir nach rechts schauen
    protected override bool CanPickup()
    {
        Player_Damageable pDama = FindObjectOfType<Player_Damageable>();
        int health = pDama.Health;
        int maxHealth = pDama.MaxHealth;
        return health < maxHealth;
    }

    protected override void Pickup()
    {
        //Debug.Log("You picked something up.");
        Instantiate(particles, transform.position, particles.transform.rotation); // Quaternion.identity = keine Rotation
        Player_Damageable pDama = FindObjectOfType<Player_Damageable>();
        pDama.HealCompletely();
    }
}
