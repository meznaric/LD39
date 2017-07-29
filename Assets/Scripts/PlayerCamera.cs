using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	Camera _camera;
	ClickablePiece _currentFigure;


	void Awake() {
		_camera = GetComponent<Camera> ();
	}

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {
		Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
		ClickablePiece hitFigure = null;
		RaycastHit hitInfo;
		Physics.Raycast (ray, out hitInfo);

		if (hitInfo.collider) {
            // Player has nested collider (banana)
            if (hitInfo.collider.tag == "Player") {
                hitFigure = hitInfo.collider.transform.parent.GetComponent<ClickablePiece>();
            } else {
                hitFigure = hitInfo.collider.GetComponent<ClickablePiece>();
            }
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
