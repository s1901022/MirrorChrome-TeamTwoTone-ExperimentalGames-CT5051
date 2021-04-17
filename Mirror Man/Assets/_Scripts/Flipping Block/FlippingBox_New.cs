using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingBox_New : MonoBehaviour {
	scrEntity m_entity;
	Rigidbody2D rb;

	public float checkFlip;
	private RaycastHit2D hitReflective;
	Transform reflectNormal;
	public bool m_grounded;
	public bool m_canFlip;

	public float initialFlipDirection = 1;

	[SerializeField]
	Sprite[] m_sprites;

	bool isMoving = false;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		m_entity = this.gameObject.GetComponent<scrEntity>();
		checkFlip = initialFlipDirection;
		rb.gravityScale = initialFlipDirection;
		if (initialFlipDirection == -1)
        {
			GetComponent<SpriteRenderer>().sprite = m_sprites[0];
		}
		else
        {
			GetComponent<SpriteRenderer>().sprite = m_sprites[1];
		}
	}

	private void Update() {
		m_grounded = m_entity.GetGrounded();
		if (m_grounded == true && m_entity.GetGameControlScript().GetMirrorStateFloat() != checkFlip) {
			DetectTerrain();
			if (hitReflective.collider != null && hitReflective.collider.tag == "Reflective") {
				m_canFlip = true;
				Flip();
			} else if (m_entity.GetGrounded() == false) {
				Debug.Log("Nope goodbye");
				checkFlip *= -1;
				rb.gravityScale *= -1;
			}
			m_canFlip = false;
		}

		// checking if it is being pushed
		if (gameObject.GetComponent<Rigidbody2D>().velocity.x != 0) {
			// if it is moving, set bool to true
			isMoving = true;
		} else {
			isMoving = false;
		}
	}

	private void DetectTerrain() {
		//Check if landed
		if (checkFlip == 1f) {
			hitReflective = Physics2D.Raycast(transform.position - new Vector3(0f, transform.localScale.y, 0f), Vector2.up);
		} else if (checkFlip == -1f) {
			hitReflective = Physics2D.Raycast(transform.position + new Vector3(0f, transform.localScale.y, 0f), Vector2.down);
		}
		reflectNormal = hitReflective.transform;
	}

	private void Flip() {
		//Flip the box
		rb.velocity = new Vector2(0.0f, 0.0f);

		float distanceBetweenReflection = transform.position.y - reflectNormal.position.y;
		float reflectiveScale = reflectNormal.localScale.y / 2;

		if (transform.position.y > reflectNormal.position.y) {
			transform.position = new Vector3(transform.position.x, (distanceBetweenReflection - reflectNormal.position.y) * -1, transform.position.z);
			GetComponent<SpriteRenderer>().sprite = m_sprites[0];
		} else if (transform.position.y < reflectNormal.position.y) {
			transform.position = new Vector3(transform.position.x, (distanceBetweenReflection - reflectNormal.position.y) * -1, transform.position.z);
			GetComponent<SpriteRenderer>().sprite = m_sprites[1];
		}
		checkFlip *= -1;
		rb.gravityScale *= -1;
	}

    public void BoxReset() {
		//Reset Boxes gravity scale
		checkFlip = initialFlipDirection;
		rb.gravityScale = initialFlipDirection;
		if (initialFlipDirection == -1) {
			GetComponent<SpriteRenderer>().sprite = m_sprites[0];
		} else {
			GetComponent<SpriteRenderer>().sprite = m_sprites[1];
		}
	}

    public bool GetMoving() { return isMoving; }
}
