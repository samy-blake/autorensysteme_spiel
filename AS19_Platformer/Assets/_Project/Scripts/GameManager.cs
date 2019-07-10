using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton
// Merkt sich Werte, die auch außerhalb des Levels noch gültig sind (e.g. TotalGemsCollected).
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public IntValue lives;
    public int TotalGemsCollected { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        // ^ Objekt lebt in einer seperaten Szene ("DontDestroyOnLoad) und bleibt somit auch in neuen Leveln erhalten.
        Init();
    }

    // Reset auf default values
    public void Init()
    {
        TotalGemsCollected = 0;
        lives.RuntimeValue = lives.defaultValue;
    }

    private void OnEnable()
    {
        LevelManager.OnModifiedGemCount += UpdateTotalGemCount;
    }

    private void OnDisable()
    {
        LevelManager.OnModifiedGemCount -= UpdateTotalGemCount;
    }

    private void UpdateTotalGemCount(int changedAmount)
    {
        TotalGemsCollected += changedAmount;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Init();
    }
}
