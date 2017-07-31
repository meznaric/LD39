using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class MapSection : FigureHolder {

    public Bubble activeBubble;
	public int sectionSize = 10000;
    private int _power = 1;
        get {
        return _power;
	// Number of position points define how many figures can be placed on a section
	public Transform[] positionPoints;
    public MapSectionTooltip tooltip;

    private bool inspecting;

	// Use this for initialization
	void Start () {
		sectionSize = Random.Range(100, 500);
		power = Random.Range(0, sectionSize);
        Map.instance.RegisterMapSection(this);
        base.Start();
	}

    public void MakeStep(int term) {
        // TODO: Sound tick
        // Term increases the range of randomness, and how negative it goes
        float hardnessTermFactor = GameManager.instance.hardnessTermFactor;
        float negativePullFactor = GameManager.instance.negativePullFactor;
        float from = -term * hardnessTermFactor;
        float to = term * hardnessTermFactor;
        float offset = term * negativePullFactor;

        for (int n = 0; n < figures.Count; n++) {
            Figure fig = figures[n];
            // Player is positioned on the field
            if (fig.tag == "Player") {
                offset -= GameManager.instance.figurePointsPerStep;
            }
            if (fig.tag == "CPU") {
                offset += GameManager.instance.figurePointsPerStep / 3;
            }
        }

        int adjustPowerBy = (int)((float)Random.Range(from, to) - offset);

        if (activeBubble != null) {
            adjustPowerBy += activeBubble.GetPowerModifier();
            activeBubble.MakeStep();
        }

        // Limit to up sectionSize, down to 0
        power = Mathf.Min(sectionSize, Mathf.Max(0, power + adjustPowerBy));
        UpdateVisualCues();
    }

    public void SpawnEvent(Transform eventPrefab) {
        if (activeBubble == null) {
            Transform go = Instantiate(eventPrefab, GetPosition(0), Quaternion.identity) as Transform;
            activeBubble = go.GetComponent<Bubble>();
            activeBubble.AttachTo(this);
        }
    }

	// Updates the height and colour
	void UpdateVisualCues() {
		float percPower = (float)power / sectionSize;
        tooltip.SetValues(percPower, sectionSize);


        if (inspecting) return;
		Color newColor = Color.Lerp (GameManager.instance.enemyColor, GameManager.instance.primaryColor, percPower);
		Vector3 newScale = new Vector3(0f, 0f, percPower * GameManager.instance.scaleFactor);

		TweenColor (newColor);
		TweenScale (newScale);
	}

	public override	void OnHoverEnter() {
        TweenScale(new Vector3(0, 0, GameManager.instance.scaleOnHover));
		TweenColor (Color.white);
        tooltip.Show(false);
        inspecting = true;
	}

	public override	void OnClick() {
        GameManager.instance.OnClick(this);
	}

	public override void OnHoverExit ()
	{
        inspecting = false;
		UpdateVisualCues ();
        tooltip.Hide(false);
	}

    public override Vector3 GetPosition(int followIndex) {
        return positionPoints[followIndex].position;
    }
}
