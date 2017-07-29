﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int power = 1000;
	// How much extra are states scaled on Z axis
	public float scaleFactor = 10.0f;

	public Color primaryColor = Color.blue;
	public Color enemyColor = Color.red;

	public static GameManager instance;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {

	}
}
