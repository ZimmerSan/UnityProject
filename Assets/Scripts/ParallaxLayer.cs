using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour {

	public float slowdown = 0.5f;

	Vector3 lastPosition;

	void Awake() {
		lastPosition = Camera.main.transform.position;	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 newPosition = Camera.main.transform.position;
		Vector3 diff = newPosition - lastPosition;
		lastPosition = newPosition;

		Vector3 myPosition = this.transform.position;
		myPosition += slowdown * diff;
		this.transform.position = myPosition;
	}
}
