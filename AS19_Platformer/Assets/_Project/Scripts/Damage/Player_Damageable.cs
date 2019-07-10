using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100; // TODO: ScripableObject?
    public IntValue health;
    public IntValue lives;
    [Range(0.01f, 5)] public float secondsOfHitInvincibility = 0.5f;

    [Header("Audio")]
    public AudioSource audioSource; // TODO: Create in script
    public AudioClip hitSound;

    private bool canTakeDamage = true;
    private Animator anim;
    private Coroutine invincibilityCoroutine;

    public int MaxHealth
    {
        get { return maxHealth; }

        private set { maxHealth = value; }
    }

    //private int health;

    public int Health
    {
        get { return health.RuntimeValue; }

        set
        {
            health.RuntimeValue = value;
            if (health.RuntimeValue > MaxHealth)
                health.RuntimeValue = MaxHealth;
            else if (health.RuntimeValue <= 0)
                Kill();
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        ResetPlayerValues();
    }

    public void Heal(int amount)
    {
        Health += amount;
        // TODO: Particles, sounds, ...
    }

    public void HealCompletely()
    {
        Heal(MaxHealth);
    }

    public void Kill()
    {
        if (lives.RuntimeValue > 0)
        {
            // Teleport
            lives.RuntimeValue--;
            ResetPlayerValues();
            LevelManager.Instance.PlayerComponent.ResetPosition(true);
            // Könnten den Player auch über GetComponent holen, und es cachen. Oder ResetPosition ganz dem LevelManager überlassen.
        }
        else
        {
            GameManager.Instance.RestartGame();
        }
    }

    private void ResetPlayerValues()
    {
        HealCompletely();
        if (invincibilityCoroutine != null)
            StopCoroutine(invincibilityCoroutine);
        EndInvincibility();
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage)
            return;

        audioSource.clip = hitSound; // TODO: Add variation
        audioSource.Play();
        //audioSource.PlayOneShot(hitSound);

        anim.SetTrigger("takeDamage");
        invincibilityCoroutine = StartCoroutine(MakeInvincible());

        Health -= amount;
        // TODO: Particles, sounds, ...
    }

    private IEnumerator MakeInvincible()
    {
        canTakeDamage = false;
        anim.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(secondsOfHitInvincibility);
        EndInvincibility();
    }

    private void EndInvincibility()
    {
        anim.SetLayerWeight(1, 0);
        canTakeDamage = true;
    }
}