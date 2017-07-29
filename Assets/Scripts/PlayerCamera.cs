using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	Camera _camera;
	Collider _currentHover;


	void Awake() {
		_camera = GetComponent<Camera> ();
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;
		Physics.Raycast (ray, out hitInfo);

		if (Input.GetMouseButtonDown (0) && _currentHover != null) {
			// TODO: Call Figure.onClick
		}
		if (_currentHover != hitInfo.collider) {
			Debug.Log (_currentHover);
			Debug.Log (hitInfo.collider);

			_currentHover = hitInfo.collider;
			if (_currentHover != null) {
				// TODO: Trigger entered
				Debug.Log("Entered: ");
				Debug.Log (_currentHover.name);
			} else {
				// TODO: Trigger left
			}
		}

	}
}
