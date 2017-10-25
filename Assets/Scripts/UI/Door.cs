using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Collectable {
	public int level = 1;

	protected override void OnRabitHit(HeroRabbit rabbit) {
		Debug.Log ("hit");
		SceneManager.LoadScene ("Level" + level);
	}

}
