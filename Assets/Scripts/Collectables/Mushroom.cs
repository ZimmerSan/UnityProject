using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable {

	protected override void OnRabitHit(HeroRabbit rabbit) {
		rabbit.beRambo(true);
		this.CollectedHide();
	}

}
