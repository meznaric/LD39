using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DButton : ClickablePiece {
    public override void OnHoverEnter() {
        TweenEmission(new Color(0.4f, 0.4f, 0.4f));
    }

    public override void OnHoverExit() {
        TweenEmission(Color.black);
    }

}
