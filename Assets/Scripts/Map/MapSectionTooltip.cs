using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class MapSectionTooltip : MonoBehaviour {

    private Vector3 _initialScale = Vector3.one;
    private Vector3 _initialPosition = Vector3.one;

    public void Awake() {
        _initialScale = transform.localScale;
        _initialPosition = transform.localPosition;
    }

    public void Start() {
        Hide(true);
    }

    public void Show(bool instant) {
        Vector3 newPos = _initialPosition + GameManager.instance.tooltipOffset;
        if (instant) {
            transform.localScale = _initialScale;
            transform.localPosition = newPos;
        } else {
            TweenScale(_initialScale);
            TweenPosition(newPos);
        }
    }

    public void Hide(bool instant) {
        if (instant) {
            transform.localScale = Vector3.zero;
            transform.localPosition = _initialPosition;
        } else {
            TweenScale(Vector3.zero);
            TweenPosition(_initialPosition);
        }
    }

	private void TweenPosition(Vector3 targetPos) {
		gameObject.Tween ("MoveTooltip" + GetInstanceID(), transform.localPosition, targetPos, 1.0f, TweenScaleFunctions.QuarticEaseOut, (t) => {
			transform.localPosition = t.CurrentValue;
		}, (t) => { });
	}

	private void TweenScale(Vector3 scale) {
		gameObject.Tween ("ScaleTooltip" + GetInstanceID(), transform.localScale, scale, 1.0f, TweenScaleFunctions.QuarticEaseOut, (t) => {
			transform.localScale = t.CurrentValue;
		}, (t) => { });
	}
}
