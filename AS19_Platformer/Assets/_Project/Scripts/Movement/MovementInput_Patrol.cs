using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gegner patrolliert horizontal von einem Punkt zum nächsten.
// In PatrolPoints wird außerdem gespeichert, wie lange dort gewartet werden soll.
[RequireComponent(typeof(MovementFromInput))]
public class MovementInput_Patrol : MonoBehaviour
{
	public float walkSpeed = 10f;

	private MovementFromInput movementFromInput;
	private int speedMultiplicator = 1;

    public PatrolPoint[] patrollingPoints;
	private int currentPointIndex;
	private bool movingRight; // Für die Lesbarkeit. Wir könnten auch den speedMultiplicator auslesen, oder ein IsMovingRight() schreiben.

	private int CurrentPointIndex
	{
		get { return currentPointIndex; }
		set
		{
			// Falls der pointIndex größer ist als unser Array, setzen wir ihn auf 0 zurück.
			if (value >= patrollingPoints.Length)
			{
				currentPointIndex = 0;
				return;
			}
			currentPointIndex = value;
		}
	}

    // TODO: on game start: optional teleport to starting point

    private void Awake()
	{
		movementFromInput = GetComponent<MovementFromInput>();
		UpdateDirection();
	}

	private void Update()
	{
		if (speedMultiplicator == 0)
			return;
		
		if ((movingRight && transform.position.x > patrollingPoints[currentPointIndex].GetPosX()) ||
		    (!movingRight && transform.position.x < patrollingPoints[currentPointIndex].GetPosX()))
		{
			speedMultiplicator = 0; // Wir pausieren die Bewegung, indem wir die walkSpeed mit 0 multiplizieren
			Invoke(nameof(ActivateNextPoint), patrollingPoints[currentPointIndex].waitForSeconds); // Nachdem eine gewisse Zeit verstrichen ist (waitAtPoint Sekunden), aktivieren wir den nächsten Punkt
		}
	}

	private void FixedUpdate()
	{
		movementFromInput.Move(speedMultiplicator * walkSpeed);
	}

	private void ActivateNextPoint()
	{
		CurrentPointIndex++; // pointIndex + 1
		UpdateDirection(); // Wir passen unsere Richtung an den neuen Punkt an.
	}

	private void UpdateDirection()
	{
		movingRight = patrollingPoints[CurrentPointIndex].GetPosX() > transform.position.x; // Wir gehen nach rechts, wenn der Punkt weiter rechts ist als der Character. Sonst gehen wir nach links.
		speedMultiplicator = movingRight ? 1 : -1; // Wenn wir nach rechts gehen, ist der multiplicator 1, wenn wir nach links gehen -1.
	}
}
