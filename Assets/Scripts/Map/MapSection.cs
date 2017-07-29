using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSection : MonoBehaviour {

	public int population = 10000;
	public int power = 5000;
	// Number of position points define how many figures can be placed on a section
	public Transform[] positionPoints;
	// All the figures on current section, used to evaluate score
	public Figure[] figures;

	private Vector3 _initialScale;

	// Use this for initialization
	void Start () {
		power = Random.Range (0, 10000);

		_initialScale = transform.localScale;

		UpdateVisualCues ();
	}

	// Updates the height and colour
	void UpdateVisualCues() {
		float percPower = (float)power / population;

		Color newColor = Color.Lerp (GameManager.instance.primaryColor, GameManager.instance.enemyColor, percPower);
			
		transform.localScale = _initialScale + new Vector3(0f, 0f, percPower * GameManager.instance.scaleFactor);
		GetComponent<Renderer>().material.color = newColor;
	}

	// Update is called once per frame
	void Update () {
	}
}
