using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStatistics : MonoBehaviour {

	public int level;
	public GameObject check, crystal, fruit, doorLock;
	public Sprite crystalFilled, fruitFilled;
	
	// Use this for initialization
	void Start () {
		LevelStatsistics stats = LevelStatsistics.load(level);
	
		if (level == 1 || LevelStatsistics.load(level - 1).levelPassed) Destroy(doorLock); 
	
		if (!stats.levelPassed) Destroy(check);
	
		if (stats.allFruitsCollected) {
			SpriteRenderer fruitRenderer = fruit.GetComponent<SpriteRenderer>();
			fruitRenderer.sprite = fruitFilled;
		}

		if (stats.allCrystalsCollected) {
			SpriteRenderer crystalRenderer = crystal.GetComponent<SpriteRenderer>();
			crystalRenderer.sprite = crystalFilled;
		}
	}
}
