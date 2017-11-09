﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : Collectable {
	public GameObject winPopUpPrefab;

	protected override void OnRabitHit(HeroRabbit rabbit) {
		GameObject parent = UICamera.first.transform.parent.gameObject;
		GameObject obj = NGUITools.AddChild(parent, winPopUpPrefab);
		WinPopUp winPopUp = obj.GetComponent<WinPopUp>();

		LevelStatsistics stats = LevelController.current.getStats();
		winPopUp.setStats(stats);

		stats.save();
	}
}