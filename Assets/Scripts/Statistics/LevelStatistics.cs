﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class LevelStatsistics {
	
	public int level;
	public bool levelPassed = false;

	public int totalFruits = -1;
	public int collectedCoins = 0;

	public List<int> collectedFruits = new List<int>();
	public List<Crystal.Type> collectedCrystals = new List<Crystal.Type>();

	public void save() {
		GameStatistics stats = GameStatistics.load();
		LevelStatsistics found = stats.levelStats.Find (p => p.level == level);
		if (found != null)
			found = this;
		else
			stats.levelStats.Add (this);
		
		stats.collectedCoins = collectedCoins;
		stats.save();
	}

	public static LevelStatsistics load(int level) {
		GameStatistics stats = GameStatistics.load();
		int tempLevel = level;
		LevelStatsistics found = stats.levelStats.Find (p => p.level == tempLevel);
		found = found != null ? found : new LevelStatsistics { level = tempLevel };
		found.collectedCoins = stats.collectedCoins;

		return found;
	}

	public bool allFruitsCollected;
	public bool allCrystalsCollected;

}