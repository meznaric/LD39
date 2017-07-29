using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class Figure : ClickablePiece {

    public MapSection _followObject = null;
    private float followSpeed = 4.0f;
    private int _followIndex = 0;

    void Start() {
        Map.instance.RegisterFigure(this);
    }

    public virtual void Update() {
        if (_followObject != null) {
            Vector3 newPos = _followObject.positionPoints[_followIndex].position;
            transform.position = Vector3.Lerp(
                transform.position,
                newPos,
                followSpeed * Time.deltaTime
            );
        }
    }

    // Is put on a section and follows the position point
	public virtual void MoveTo(MapSection mapSection, int followIndex) {
        // TODO: Spawn sound when there is no space

        bool isNewMapSectionSelected = _followObject != null && mapSection != _followObject;
        // If we are just moving to available space inside a MapSection then we don't need to remove ourselves
        if (isNewMapSectionSelected) {
            _followObject.RemoveFigure(this);
        }
        // Only if we actually moved we add a figure
        if (_followObject == null || isNewMapSectionSelected) {
            mapSection.AddFigure(this);
        }
        _followIndex = followIndex;
        _followObject = mapSection;
	}
}
