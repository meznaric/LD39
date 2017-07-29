using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class Figure : MonoBehaviour {

    private MapSection _followObject = null;
    private float followSpeed = 4.0f;

    void Start() {
        Map.instance.RegisterFigure(this);
    }

	public virtual void OnHoverEnter() {

	}

	public virtual void OnClick() {

	}

	public virtual void OnHoverExit() {

	}

    public virtual void Update() {
        if (_followObject != null) {
            Vector3 newPos = _followObject.positionPoints[0].position;
            transform.position = Vector3.Lerp(
                transform.position,
                newPos,
                followSpeed * Time.deltaTime
            );
        }
    }

    // Is put on a section and follows the position point
	public void MoveTo(MapSection mapSection, Vector3 pos) {
        _followObject = mapSection;
	}
}
