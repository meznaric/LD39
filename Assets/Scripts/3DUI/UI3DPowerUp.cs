using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DPowerUp : ClickablePiece {
    public Renderer renderer;
    public Material material;

    public Transform helpText;
    private Vector3 _helpTextInitialScale;

    private Color _initialColor;

    void Start() {
        _initialScale = transform.localScale;
        _material = renderer.material;
        _initialColor = _material.color;
        _helpTextInitialScale = helpText.localScale;
        TweenScale(Vector3.zero, helpText);
    }

    public override void OnClick() {
    }

    public override void OnHoverExit() {
        TweenColor(_initialColor);
        TweenScale(Vector3.zero, helpText);
    }

    public override void OnHoverEnter() {
		TweenColor(Color.white);
        TweenScale(_helpTextInitialScale, helpText);
    }
}
