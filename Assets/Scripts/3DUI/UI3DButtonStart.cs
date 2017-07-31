using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DButtonStart : UI3DButton {

    public override void OnClick() {
        GameManager.instance.OnClickStartGame();
    }
}
