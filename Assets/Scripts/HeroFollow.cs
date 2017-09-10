using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour {

	public HeroRabbit rabbit;
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform rabbitTransform = rabbit.transform;
		Transform cameraTransform = this.transform;

		Vector3 rabbitPosition = rabbitTransform.position;
		Vector3 cameraPosition = cameraTransform.position;

		cameraPosition.x = rabbitPosition.x;
		cameraPosition.y = rabbitPosition.y;

		cameraTransform.position = cameraPosition;
	}
}
