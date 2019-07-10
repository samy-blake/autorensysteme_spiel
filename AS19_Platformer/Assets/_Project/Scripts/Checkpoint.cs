using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isEnabled;
    public bool IsEnabled
    {
        get { return  isEnabled; }

        set
        {
            if (value == isEnabled)
                return;
            
            isEnabled = value;

            if (isEnabled)
            {
                EnableCheckpoint();
            }
            else
            {
                DisableCheckpoint();
            }
        }
    }

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    
    private void Start()
    {
        DisableCheckpoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IsEnabled)
        {
            IsEnabled = true;
        }
    }

    private void EnableCheckpoint()
    {
        LevelManager.Instance.UpdateCurrentCheckpoint(this); // Deaktiviert andere Checkpoints etc.
        
        spriteRenderer.color = spriteRenderer.color.ChangeAlpha(1f);
        anim.SetTrigger("Activate");
    }

    private void DisableCheckpoint()
    {
        spriteRenderer.color = spriteRenderer.color.ChangeAlpha(0.5f);
    }
}