using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour {

	public static CrystalController current;

	public UI2DSprite redCrystal, greenCrystal, blueCrystal;
	public Sprite redCrystalSprite, greenCrystalSprite, blueCrystalSprite;

	public void addCrystal(Crystal crystal) {
	switch (crystal.type) {
		case Crystal.Type.BLUE:
			blueCrystal.sprite2D = blueCrystalSprite;
			break;
		case Crystal.Type.RED:
			redCrystal.sprite2D = redCrystalSprite;
			break;
		case Crystal.Type.GREEN:
			greenCrystal.sprite2D = greenCrystalSprite;
			break;
		}
	}
		
	void Awake () {
		current = this;
	}

}
