using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public List<MapSection> mapSections = new List<MapSection>();
    public List<Figure> figures = new List<Figure>();

    public static Map instance = null;

    public void RegisterMapSection(MapSection mapSection) {
        mapSections.Add(mapSection);
    }

    public void RegisterFigure(Figure figure) {
        figures.Add(figure);
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
