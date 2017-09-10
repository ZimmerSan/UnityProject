using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

	public float speed = 1;
	SpriteRenderer spriteRenderer = null;
	Rigidbody2D myBody = null;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		myBody = this.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		float value = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;

			if(value < 0) {
				spriteRenderer.flipX = true;
			} else if(value > 0) {
				spriteRenderer.flipX = false;
			}
		}
	}

}
