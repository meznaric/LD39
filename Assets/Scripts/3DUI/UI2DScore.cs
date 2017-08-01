using System.Collections;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

class UI2DScore : MonoBehaviour {
    public Text text;


    void Update() {
        text.text = "Score: " + Mathf.Round(GameManager.instance.score / 100);
    }
}

