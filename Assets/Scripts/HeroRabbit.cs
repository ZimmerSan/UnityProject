using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

	public float speed = 1;
	public float maxJumpTime = 2f;
	public float jumpSpeed = 2f;

	bool isGrounded = false;
	bool jumpActive = false;
	float jumpTime = 0f;

	SpriteRenderer spriteRenderer = null;
	Rigidbody2D myBody = null;
	Animator animator = null;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		myBody = this.GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator> ();
		LevelController.current.setStartPosition (transform.position);
	}

	void FixedUpdate () {

		//Handle horizontal movement
		float value = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (value) > 0) {
			animator.SetBool ("run", true);

			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;

			if (value < 0)		spriteRenderer.flipX = true;
			else if (value > 0)	spriteRenderer.flipX = false;
		} else {
			animator.SetBool ("run", false);
		}

		// RayCast bounds
		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layerId = 1 << LayerMask.NameToLayer ("Ground");
		RaycastHit2D hit = Physics2D.Linecast (from, to, layerId);
		Debug.DrawLine (from, to, Color.red);

		if (hit) 	isGrounded = true;
		else 		isGrounded = false;

		if (Input.GetButtonDown ("Jump") && isGrounded) this.jumpActive = true;

		if (this.jumpActive) {
			// Button is still held
			if (Input.GetButton ("Jump")) {
				this.jumpTime += Time.deltaTime;
				if (this.jumpTime < this.maxJumpTime) {
					Vector2 vel = myBody.velocity;
					vel.y = jumpSpeed * (1.0f - jumpTime / maxJumpTime);
					myBody.velocity = vel;
				}
			} else {
				this.jumpActive = false;
				this.jumpTime = 0;
			}
		}

		if (this.isGrounded)	animator.SetBool ("jump", false);
		else 					animator.SetBool ("jump", true);

	}

}
