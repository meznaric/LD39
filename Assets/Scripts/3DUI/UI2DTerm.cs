using System.Collections;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

class UI2DTerm : MonoBehaviour {
    public Text text;

    void Update() {
        text.text = "Terms: " + GameManager.instance.GetTerm();
    }
}

