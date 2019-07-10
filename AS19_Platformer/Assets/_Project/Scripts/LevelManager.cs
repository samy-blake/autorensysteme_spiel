using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // für Events (Action)

// Singleton
// Speichert Informationen, die nur für das Level gelten:
// Gems insgesamt, eingesammelte Gems, Checkpoints, Startpunkt, Spieler, ...
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public Player PlayerComponent { get; private set; }

    public Transform startPointTransform;
    public Vector3 LastPosition { get; private set; }

    public static event Action<int> OnModifiedGemCount;
    public static event Action OnChangedGemCount;

    private Checkpoint currentCheckpoint;
    private int collectedGems;
    public int GemCountInLevel { get; private set; }

    private DateTime startTime;
    private DateTime stopTime;

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
        Init();
    }

    private void Init()
    {
        FindPlayer();

        if (!startPointTransform)
        {
            LastPosition = PlayerComponent.transform.position;
            Debug.Log("Kein Startpunkt angegeben.");
        }
        else
        {
            LastPosition = startPointTransform.position;
        }
        PlayerComponent.ResetPosition(true);

        CountCollectibles();
        SetTimeStart();
    }
    
    private void CountCollectibles()
    {
        GemCountInLevel = FindObjectsOfType<Gem_Collectible>().Length;
        // Funktioniert nicht, wenn wir unser Level erst stückchenweise laden.
        OnChangedGemCount?.Invoke();
    }

    private void FindPlayer()
    {
        PlayerComponent = FindObjectOfType<Player>();
        if (!PlayerComponent)
        {
            Debug.Log("Did you forget to add the player?");
            // TODO: Spawn player
        }
    }

    private void SetTimeStart()
    {
        startTime = System.DateTime.UtcNow;
    }

    public void UpdateTimeUi()
    {
        stopTime = System.DateTime.UtcNow;
    }

    public int PlayTime
    {
        get { return (int)(stopTime  - startTime).TotalSeconds; }
    }

    public int CollectedGems
    {
        get { return collectedGems; }

        private set
        {
            if (value == collectedGems)
                return;

            int difference = value - collectedGems;
            OnModifiedGemCount?.Invoke(difference);

            collectedGems = value;
            OnChangedGemCount?.Invoke(); // Ersetzbar durch OnModifiedGemCount
        }
    }

    public void ModifyGemCount(int change)
    {
        CollectedGems += change;
        // TODO: Play different sounds if gained or lost coins, etc.
    }
    
    // Deaktiviert den alten Checkpoint und updated LastPosition (für ResetPosition im Player)
    public void UpdateCurrentCheckpoint(Checkpoint activeCheckpoint)
    {
        if (currentCheckpoint)
        {
            currentCheckpoint.IsEnabled = false;
        }
        currentCheckpoint = activeCheckpoint;
        LastPosition = currentCheckpoint.transform.position;
    }

}
