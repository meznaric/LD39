using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class ClickablePiece : MonoBehaviour {
	protected Material _material;
	protected Vector3 _initialScale;

    protected virtual void Start() {
        _material = GetComponent<Renderer> ().material;
		_initialScale = transform.localScale;
    }

	public virtual void OnHoverEnter() {

	}

	public virtual void OnClick() {

	}

	public virtual void OnHoverExit() {

	}

    protected void SetMaterial(Material material) {
        _material = material;
    }

    protected void TweenScale(Vector3 scale) {
        TweenScale(scale + _initialScale, transform);
    }

	protected void TweenScale(Vector3 to, Transform transform) {
		gameObject.Tween ("ScaleZ" + GetInstanceID(), transform.localScale, to, 0.8f, TweenScaleFunctions.QuarticEaseOut, (t) => {
			transform.localScale = t.CurrentValue;
		}, (t) => { });
	}

	protected void TweenColor(Color color) {
		gameObject.Tween("ChangeColor" + GetInstanceID(), _material.color, color, 0.8f, TweenScaleFunctions.QuarticEaseOut, (t) => {
			_material.color = t.CurrentValue;
		}, (t) => {});
	}

    protected void TweenEmission(Color color) {
		gameObject.Tween("ChangeEmission" + GetInstanceID(), _material.GetColor("_EmissionColor"), color, 0.3f, TweenScaleFunctions.QuarticEaseOut, (t) => {
            _material.SetColor ("_EmissionColor", t.CurrentValue);
		}, (t) => {});
    }
}
