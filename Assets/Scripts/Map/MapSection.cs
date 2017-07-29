using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class MapSection : Figure {

	public int population = 10000;
	public int power = 5000;
	// Number of position points define how many figures can be placed on a section
	public Transform[] positionPoints;
	// All the figures on current section, used to evaluate score
	public Figure[] figures;
    public MapSectionTooltip tooltip;

	private Vector3 _initialScale;
	private Material _material;

	// Use this for initialization
	void Start () {
		power = Random.Range(0, 10000);

		_initialScale = transform.localScale;
		_material = GetComponent<Renderer> ().material;

		UpdateVisualCues ();
        Map.instance.RegisterMapSection(this);
	}

	// Updates the height and colour
	void UpdateVisualCues() {
		float percPower = (float)power / population;

		Color newColor = Color.Lerp (GameManager.instance.primaryColor, GameManager.instance.enemyColor, percPower);
		Vector3 newScale = new Vector3(0f, 0f, percPower * GameManager.instance.scaleFactor);

		TweenColor (newColor);
		TweenScale (newScale);
	}

	private void TweenScale(Vector3 scale) {
		gameObject.Tween ("ScaleZ" + gameObject.name, transform.localScale, _initialScale + scale, 1.0f, TweenScaleFunctions.QuarticEaseOut, (t) => {
			transform.localScale = t.CurrentValue;
		}, (t) => { });
	}

	private void TweenColor(Color color) {
		gameObject.Tween("ChangeColor" + gameObject.name, _material.color, color, 1.0f, TweenScaleFunctions.QuarticEaseOut, (t) => {
			_material.color = t.CurrentValue;
		}, (t) => {});
	}

	public override	void OnHoverEnter() {
		TweenScale(new Vector3(0, 0, GameManager.instance.scaleOnHover));
	}

	public override	void OnClick() {
		if (positionPoints.Length > 0) {
			GameManager.instance.player.MoveTo (this, positionPoints[0].transform.position);
		}

		TweenScale(new Vector3(0, 0, GameManager.instance.scaleOnClick));
		TweenColor (Color.white);
        tooltip.Show(false);
	}

	public override void OnHoverExit ()
	{
		UpdateVisualCues ();
        tooltip.Hide(false);
	}

	// Update is called once per frame
	void Update () {
        base.Update();
	}
}
