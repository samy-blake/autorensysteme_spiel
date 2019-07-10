using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Die Bewegungslogik des Spielers. Wird tatsächlich bewegt durch MovementFromInput.
[RequireComponent(typeof(MovementFromInput), typeof(GroundChecker))]
public class MovementInput_Player : MonoBehaviour
{
	public BoolValue allowInput;
	public MovementData movementData;
	public bool rawInput = false;
	[Range(0, 1)] public float airControl = 0f;
	public int maxJumps = 2;

	private Rigidbody2D rb;
	private MovementFromInput movementFromInput;
	private GroundChecker groundChecker;
	
	private bool willJump;
	private int jumpCount;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		movementFromInput = GetComponent<MovementFromInput>();
		groundChecker = GetComponent<GroundChecker>();
	}
	
	private void Update()
	{
		if (!allowInput.RuntimeValue)
		{
			return;
		}

		bool isGrounded = groundChecker.IsGrounded();
		if (isGrounded)
		{
			jumpCount = 0;
		}

		if (isGrounded || (jumpCount < maxJumps))
		{
			if (Input.GetButtonDown("Jump") && !willJump)
			{
				willJump = true;
			}
		}
	}

	private void FixedUpdate()
	{
		if (!allowInput.RuntimeValue)
		{
			return;
		}

		float horizontalInput = rawInput ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
		if (groundChecker.IsGrounded())
		{
			movementFromInput.Move(horizontalInput * movementData.walkSpeed);
		}
		else
		{
			float speed = Mathf.Lerp(rb.velocity.x, horizontalInput * movementData.walkSpeed, airControl);
			movementFromInput.Move(speed);
		}

		if (willJump)
		{
			movementFromInput.Jump(movementData.jumpSpeed);
			jumpCount++;
			willJump = false;
		}
	}
}
