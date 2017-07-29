using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int power = 1000;
	// How much extra are states scaled on Z axis
	public float scaleFactor = 10.0f;
	public float scaleOnHover = 100f;
	public float scaleOnClick = 55f;

    public Vector3 tooltipOffset = Vector3.up;


	public Color primaryColor = Color.blue;
	public Color enemyColor = Color.red;
	public Player player;

	public static GameManager instance;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {

	}
}
