using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DButtonTutorial : UI3DButton {

    public override void OnClick() {
        GameManager.instance.OnClickTutorial();
    }
}
