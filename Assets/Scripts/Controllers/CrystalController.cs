using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour {

	public static CrystalController current;

	public UI2DSprite redCrystal, greenCrystal, blueCrystal;
	public Sprite redCrystalSprite, greenCrystalSprite, blueCrystalSprite;

	public List<Crystal.Type> collectedCrystals;

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

		collectedCrystals.Add (crystal.type);
	}

	void Start () {
		LevelStatsistics stats = LevelStatsistics.load (LevelController.current.level);
		collectedCrystals = stats.collectedCrystals;
	}

	void Awake () {
		current = this;
	}

}
