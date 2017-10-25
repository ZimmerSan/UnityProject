using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {

	public Type type;

	public enum Type {
		RED, GREEN, BLUE
	}

	protected override void OnRabitHit(HeroRabbit rabbit) {
		this.CollectedHide();
		LevelController.current.addCrystal (this);
	}

}
