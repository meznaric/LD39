using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Figure {

	// Use this for initialization
	void Start () {

    }

	// Update is called once per frame
	void Update () {
        base.Update();
	}

    public override void OnClick() {
        GameManager.instance.OnClick(this);
    }
}
