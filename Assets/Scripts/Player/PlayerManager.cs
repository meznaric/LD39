using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager instance;

    public PowerUpHolder powerUpHolder;

    void Awake () {
        instance = this;
    }
}
