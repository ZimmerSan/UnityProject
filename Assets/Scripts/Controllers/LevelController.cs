using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current;

	public UILabel coinsLabel;
	public UILabel fruitsLabel, totalFruitsLabel;

	int coins;
	int fruits, totalFruits;

	Vector3 startingPosition;

	void Awake() {
		current = this;
		coins = 0;
	}

	void Start() {
		Fruit[] allFruits = GameObject.FindObjectsOfType<Fruit>();
		totalFruits = allFruits.Length;
		totalFruitsLabel.text = totalFruits.ToString();
	}

	public void addCoin() { coins++; }

	public void addFruit() { fruits++; }

	void Update() {
		coinsLabel.text = coins.ToString("D4");
		fruitsLabel.text = fruits.ToString();
	}

	public void setStartPosition (Vector3 position) { this.startingPosition = position; }

	public void onRabbitDeath (HeroRabbit rabbit) {
		rabbit.beRambo (false);
		rabbit.transform.position = this.startingPosition;
		LivesController.current.removeLife ();
	}

	public void addCrystal(Crystal crystal) {
		CrystalController.current.addCrystal (crystal);
	}
}
