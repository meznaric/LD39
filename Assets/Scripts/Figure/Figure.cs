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
	public virtual void MoveTo(MapSection mapSection) {
        // TODO: Check if there is any free space
        // TODO: Remove self from last map section
        // TODO: Set current map section
        // TODO: Remove figure from last map section
        // TODO: Add figure to new map section
        _followObject = mapSection;
	}
}
