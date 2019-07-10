using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject winScreen; // Das UI, das aktiviert wird
    public BoolValue allowInput; // Zugriff auf die Variable (ScriptableObject)
    public string submitButton = "Submit"; // Optional

    private Player player; // Cache
    private bool screenIsActive; // Cache

    
    private void Update()
    {
        if (Input.GetButtonDown(submitButton) && screenIsActive)
        {
            // TODO: Move to GameManager
            // Falls ein nächstes Level existiert, laden wir es. Sonst geht es zurück zum Hauptmenü.
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextIndex >= SceneManager.sceneCountInBuildSettings)
            {
                //nextIndex = 0; // MainMenu
                GameManager.Instance.RestartGame();
            }
            else
            {
                SceneManager.LoadScene(nextIndex);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        
        // Falls wir den Spieler noch nicht kennen, suchen wir ihn -> cache
        // (Das ist in diesem Fall nicht notwendig, weil wir das Goal nur ein Mal pro Level aufrufen.)
        // (Dieses Methode eignet sich allerdings für Scripte, die regelmäßig auf etwas zugreifen.)
        if (!player)
        {
            player = collision.GetComponent<Player>();
        }
        allowInput.RuntimeValue = false;
        LevelManager.Instance.UpdateTimeUi();
        winScreen.SetActive(true);
        screenIsActive = true;
    }
}
