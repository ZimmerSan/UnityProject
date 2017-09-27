using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	protected override void OnRabitHit(HeroRabbit rabbit) {
		if (!rabbit.beRambo (false)) rabbit.die(this.transform);
		this.CollectedHide();
	}

}
