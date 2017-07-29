using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DPowerUp : ClickablePiece {
    public Renderer renderer;
    public Material material;

    private Color _initialColor;

    void Start() {
        _initialScale = transform.localScale;
        _material = renderer.material;
        _initialColor = _material.color;
    }

    public override void OnClick() {
    }

    public override void OnHoverExit() {
        TweenColor(_initialColor);
    }

    public override void OnHoverEnter() {
		TweenColor(Color.white);
    }
}
