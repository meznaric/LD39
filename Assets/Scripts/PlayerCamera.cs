using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	Camera _camera;
	Figure _currentFigure;


	void Awake() {
		_camera = GetComponent<Camera> ();
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
		Figure hitFigure = null;
		RaycastHit hitInfo;
		Physics.Raycast (ray, out hitInfo);

		if (hitInfo.collider) {
			hitFigure = hitInfo.collider.GetComponent<Figure>();
		}

		if (_currentFigure != hitFigure) {
			// Let the old one know that mouse has left
			if (_currentFigure != null) {
				_currentFigure.OnHoverExit();
			}
			if (hitFigure != null) {
				hitFigure.OnHoverEnter();
			}
		}


		if (Input.GetMouseButtonDown (0) && _currentFigure != null) {
			_currentFigure.OnClick ();
		}

		_currentFigure = hitFigure;
	}
}
