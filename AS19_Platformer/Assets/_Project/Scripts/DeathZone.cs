using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private Player_Damageable player; // Cache

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!player) // Falls wir den Spieler noch nicht kennen, suchen wir ihn;
            {
                player = collision.GetComponent<Player_Damageable>();
            }
            player.Kill();
        }
    }
}
