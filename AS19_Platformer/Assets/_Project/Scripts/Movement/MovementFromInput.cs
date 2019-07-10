using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wiederverwendbares Component.
// Implementiert selbst keine Logik (wann soll man springen, und wie schnell).
// Wird stattdessen von anderen Components referenziert.
// Das erlaubt uns, unsere Movement-Methode später schnell zu ändern. (Physics sind doof? Nur dieses Script muss anders)
// Außerdem können wir die Input-Logik runtime ändern. (Von Player-Input zu AI zu Patrolling und wieder zurück.)
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class MovementFromInput : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator anim;
	public bool FacingRight { get; private set; } = true;

    private bool hasVelocityX;
    private bool hasVelocityY;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

        hasVelocityX = anim.HasParameter("velocityX");
        hasVelocityY = anim.HasParameter("velocityY");
	}

	private void Update()
	{
		UpdateAnimator();
	}

	public void Jump(float speed)
	{
		rb.velocity = new Vector2(rb.velocity.x, speed);
	}

	public void Move(float speed)
	{
		rb.velocity = new Vector2(speed, rb.velocity.y);
		// Abhängig von Geschwindigkeit. Test-Case: airControl 0.1 -> flippt erst spät, könnte verwirren
		if ((FacingRight && speed < 0) || (!FacingRight && speed > 0))
		{
			Flip();
		}
	}
	
	// Einzeln und public, falls wir mal in Cutscenes o.ä. darauf zugreifen wollen.
	public void Flip()
	{
		// localScale flippt auch unsere Collider (und die Kinder).
		// Wir verwenden SpriteRenderer, um sicherzugehen, dass das BaseSprite nach rechts schaut.
		gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(-1, 1, 1));
		FacingRight = !FacingRight;
	}
	
	private void UpdateAnimator()
	{
        if (hasVelocityX)
            anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));

        if (hasVelocityY)
            anim.SetFloat("velocityY", rb.velocity.y);
	}
}
