using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {
	
	public int id;

	public bool isCollected = false;

	protected override void OnRabitHit(HeroRabbit rabbit) {
		Debug.Log ("Collect. Id = " +  id + ", Is collected? " + isCollected);
		if (!isCollected) {
			LevelController.current.addFruit (id);
			this.CollectedHide ();
		}
	}

	void Start () {
		LevelStatsistics stats = LevelStatsistics.load(LevelController.current.level);
		isCollected = stats.collectedFruits.Contains(id);
		if (isCollected) this.CollectedHide ();
	}

	public void CollectedHide () {
		isCollected = true;
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		Color tmp = sr.color;
		tmp.a = 0.5f;
		sr.color = tmp;
	}

}
