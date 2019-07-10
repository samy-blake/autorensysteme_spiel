using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 50;
    private Player_Damageable player; // Cache

    // Stay funktioniert nur, weil wir Invincibility/Damage-Cooldown haben.
    // Ansonsten wäre OnTriggerEnter2D besser.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (!player)
            player = collision.GetComponent<Player_Damageable>(); // Alternative: LevelManager

        player.TakeDamage(damage);
    }
}
