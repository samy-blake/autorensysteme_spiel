using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wiederverwendbarer GroundChecker
// Könnten es auch so aufsetzen, dass es ein abstrakter Parent ist, damit wir verschiedene Arten
// von GroundCheckern implementieren können.
public class GroundChecker : MonoBehaviour
{
	public Vector3 groundCheckOffset;
	public float groundCheckRadius = 0.2f;
	public LayerMask groundLayerMask = -1; // Was gilt als Boden | -1 = everything
	
	public bool IsGrounded()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + groundCheckOffset, groundCheckRadius, groundLayerMask);
		foreach (Collider2D collider in colliders)
		{
			// Nur dann IsGroundet, wenn der Kollider kein Trigger ist, und nicht das Objekt.
			// (Im Gegensatz zu Legolas kann unser Spieler keine fallenden Treppen hochlaufen.)
			if (collider.gameObject != gameObject && !collider.isTrigger)
			{
				return true;
			}
		}
		return false;
	}

	// Visualisiert unseren Kreis im Editor, wenn das GameObject selektiert ist
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);
	}
}
