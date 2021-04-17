﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerMovement : MonoBehaviour {
	//Variables for Movement and direction
	[SerializeField]
	private int jumplimit;
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float jumpForce;
	private float moveInput;
	private bool facingRight = true;
	public bool grounded = false;
	private int extraJumps;
	//Variables for collision detection
	public Transform groundCheck;
	[SerializeField]
	private float checkRadius;
	[SerializeField]
	private LayerMask ground;

	private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;
	private scrEntity m_entity;
	private scrSwitchGravity m_mirror;

	//Animation
	[SerializeField]
	private Sprite idle;
	[SerializeField]
	private Sprite run;

	// This enum is all of the animation states which can be active
	private enum AnimationStates {
		Idle,
		Running,
		Jumping,
		Falling,
		Pushing
	};
	// creating an object of the animator and the animation states enum
	private Animator anim;
	private AnimationStates animState;

	GameObject pushingBlock;


	// Start is called before the first frame update
	void Start() {
		extraJumps = jumplimit;
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		m_entity = GetComponent<scrEntity>();
		m_mirror = GetComponent<scrSwitchGravity>();
		// getting a reference to the animator
		anim = GetComponent<Animator>();
		// at the start of the level, set the animation state to idle
		animState = AnimationStates.Idle;
		pushingBlock = FindObjectOfType<FlippingBox_New>().gameObject;
	}

	void FixedUpdate() {

	}

	// Update is called once per frame
	void Update() {
		if (!m_entity.GetDead()) {
			moveInput = Input.GetAxis("Horizontal");
			rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
			if (m_entity.GetGrounded() == true && extraJumps < 0 && rb.velocity.y == 0f)
			{
				extraJumps = jumplimit;
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps >= 0 && m_entity.GetGrounded() == true && (rb.velocity.y >= -1f && rb.velocity.y <= 1f)) {
				// if the player jumps
				m_entity.GetAudioManager().PlayAudio(0, "Jump");
				animState = AnimationStates.Jumping;
				rb.velocity = Vector2.up * jumpForce;
				extraJumps -= 1;
				// set the animation state to jumping
				Debug.Log("JUMPING");
			}

			anim.SetInteger("state", (int)animState);
			StateSwitch();
		}
	}

	private void StateSwitch() {
		// change the direction of the animation depending on the direction the player is moving
		if ((facingRight == false && moveInput > 0) || (facingRight == true && moveInput < 0)) {
			AnimationControl();
		}
		// if the player is jumping
		if (animState == AnimationStates.Jumping) {
			// if the player is starting to fall
			if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0) {
				// set the animation state to falling
				animState = AnimationStates.Falling;
			}

		} else if (animState == AnimationStates.Falling) {
			// and the player is grounded
			if (m_entity.GetGrounded()) {
				animState = AnimationStates.Idle;
			}
		} else if (pushingBlock != null && pushingBlock.GetComponent<FlippingBox_New>().GetMoving() == true) {
			// if the block is moving
			// set the state to pushing
			if (m_entity.GetAudioManager().AudioIsPlaying() == false)
			{
				m_entity.GetAudioManager().PlayAudio(0, "Push");
			}
			animState = AnimationStates.Pushing;

		} else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon) {
			// if the players velocity is above epsilon (the smallest possible number)
			// and the player is not pushing a block
			// set the animation state to running
			animState = AnimationStates.Running;

			if (m_entity.GetAudioManager().AudioIsPlaying() == false && (rb.velocity.y >= -1f && rb.velocity.y <= 1f))
            {
				m_entity.GetAudioManager().PlayAudio(0, "FootStep");
            }

		} else {
			// else if the player is doing nothing
			// set the animation state to idle
			animState = AnimationStates.Idle;
			m_entity.GetAudioManager().StopAudio("FootStep");
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		//Handle Death and respawning
		if (col.gameObject.tag == "Death" && !m_entity.GetDead()) {
			// if the player is flipped 
			if (m_mirror.isFlipped % 2 != 0) {
				// reset isFlipped int
				m_mirror.isFlipped = 0;
				// reset for next try
				m_mirror.ResetForNextTry();
			}
			// reset the rotation
			m_mirror.ResetRotation();
			// reset gravity
			m_mirror.Reset();
		}
	}

	public void AnimationControl() {
		// change the direction of the bool
		facingRight = !facingRight;
		// create a new scaler variable from the localscale
		Vector3 scaler = transform.localScale;
		// flip it
		scaler.x *= -1;
		// set it to the players localScale
		transform.localScale = scaler;
	}

	//Getters
	public float GetJumpForce() { return jumpForce; }
	public bool GetDirection() { return facingRight; }
	//Setters
	public void FlipJumpGrav() { jumpForce = jumpForce * -1; }
	public void SetDirection(bool a_isRightFace) { facingRight = a_isRightFace; }
}
