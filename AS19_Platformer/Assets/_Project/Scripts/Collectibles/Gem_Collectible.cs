using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inherited von CollectibleBase. MUSS also Pickup definieren, und KANN CanPickup überschreiben/erweitern.
public class Gem_Collectible : CollectibleBase
{
    public GameObject particles;

    // Hier drin können wir die Variablen des Parents überschreiben.
    private Gem_Collectible()
    {
        destroyOnPickup = true;
    }

    // Beispiel: Überschreibung von CanPickup() -> können es nur einsammeln, wenn wir nach rechts schauen
    //protected override bool CanPickup()
    //{
    //    return FindObjectOfType<Player>().facingRight;
    //}

    protected override void Pickup()
    {
        //Debug.Log("You picked something up.");
        Instantiate(particles, transform.position, particles.transform.rotation); // Quaternion.identity = keine Rotation
        LevelManager.Instance.ModifyGemCount(1);
    }
}
