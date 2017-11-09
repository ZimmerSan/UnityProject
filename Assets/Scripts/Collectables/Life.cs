using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Collectable {

	protected override void OnRabitHit(HeroRabbit rabbit) {
		LevelController.current.addLife ();
		this.CollectedHide();
	}

}
