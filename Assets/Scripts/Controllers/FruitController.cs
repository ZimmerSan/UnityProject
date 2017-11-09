using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour {

	public static FruitController current;

	public UILabel fruitsLabel, totalFruitsLabel;

	public HashSet<int> collectedFruits;
	public int totalFruits;

	public void addFruit(int fruitId) {
		this.collectedFruits.Add (fruitId);
	}

	// Use this for initialization
	void Start () {
		Fruit[] allFruits = GameObject.FindObjectsOfType<Fruit>();
		for (int i = 0; i < allFruits.Length; i++) allFruits [i].id = i;

		int totalFruits = allFruits.Length;
		totalFruitsLabel.text = totalFruits.ToString();	
		this.totalFruits = totalFruits;

		LevelStatsistics stats = LevelStatsistics.load (LevelController.current.level);
		collectedFruits = new HashSet<int>(stats.collectedFruits);
	}

	void Update() {
		fruitsLabel.text = this.collectedFruits.Count.ToString();
	}
	
	void Awake () {
		current = this;
	}

}
