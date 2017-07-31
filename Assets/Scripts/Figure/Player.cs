using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Figure {
    public override void OnClick() {
        GameManager.instance.OnClick(this);
        base.OnClick();
    }
}
