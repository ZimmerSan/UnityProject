﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {

	public bool isCollected = false;

	public Type type;

	public enum Type {
		RED, GREEN, BLUE
	}

	protected override void OnRabitHit(HeroRabbit rabbit) {
		this.CollectedHide();
		LevelController.current.addCrystal (this);
	}

	void Start () {
		LevelStatsistics stats = LevelStatsistics.load(LevelController.current.level);
		isCollected = stats.collectedCrystals.Contains(type);
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
