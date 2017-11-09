﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Collectable {
	public int level = 1;

	protected override void OnRabitHit(HeroRabbit rabbit) {
		if(level == 1 || LevelStatsistics.load(level-1).levelPassed)
		SceneManager.LoadScene ("Level" + level);
	}

}
