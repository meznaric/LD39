using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class Bubble : Figure {

    public enum BubbleType {Good, What, Bad};
    public BubbleType bubbleType;
    private MapSection mapSection;

    private int stepsToLive;

    private int startStep = 0;
    private int whatVelocity;
    protected Vector3 _is;

    void Start() {
        base.Start();
        _followIndex = 0;
        whatVelocity = Random.Range(-10, 10);
        stepsToLive = Random.Range(3, 20);
    }

    public void AttachTo(MapSection ms) {
        mapSection = ms;
        _followObject = ms;
        _is = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine(TweenScale(_is));
        ms.activeBubble = this;
        startStep = GameManager.instance.GetStep();
    }

    public void MakeStep() {
        if (startStep + stepsToLive == GameManager.instance.GetStep()) {
            StartCoroutine(TweenScale(Vector3.zero));
            mapSection.activeBubble = null;
            Destroy(gameObject, 2.0f);
        }
    }

    public int GetPowerModifier() {
        switch (bubbleType) {
            case BubbleType.Bad: return -20;
            case BubbleType.What: return whatVelocity;
            case BubbleType.Good: return 20;
            default: return 0;
        }
    }

	protected IEnumerator TweenScale(Vector3 to) {
        yield return new WaitForSeconds(0.2f);
		gameObject.Tween ("ScaleZ" + GetInstanceID(), transform.localScale, to, 1.0f, TweenScaleFunctions.QuarticEaseOut, (t) => {
			transform.localScale = t.CurrentValue;
		}, (t) => { });
	}
}
