using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public List<MapSection> mapSections = new List<MapSection>();
    public static Map instance = null;

    public void RegisterMapSection(MapSection mapSection) {
        mapSections.Add(mapSection);
    }

    void Awake() {
        instance = this;
    }

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {

	}
}
