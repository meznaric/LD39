using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class Kavica : ClickablePiece {

    public Transform hintText;

    Vector3 _initialHintTextPos;

    void Awake() {
        _initialHintTextPos = hintText.localPosition;
        hintText.localPosition = hintText.localPosition += Vector3.back * 4;
    }

    public override void OnClick() {
		gameObject.Tween("HintTextKavica", hintText.localPosition, _initialHintTextPos, 2f, TweenScaleFunctions.CubicEaseOut, (t) => {
            hintText.localPosition = t.CurrentValue;
        }, (t) => {
            StartCoroutine("HideHintText");
        });
    }

    IEnumerator HideHintText() {
        yield return new WaitForSeconds(1f);
        gameObject.Tween("HintTextKavica", hintText.localPosition, _initialHintTextPos + Vector3.back * 4, 1f, TweenScaleFunctions.Linear, (t) => {
            hintText.localPosition = t.CurrentValue;
        }, (x) => {});
    }

}
