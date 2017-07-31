using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : ClickablePiece {

    public Renderer renderer;

    public FigureHolder _followObject = null;
    private float followSpeed = 4.0f;
    protected int _followIndex = 0;

    public virtual void Start() {
        _initialScale = transform.localScale;
        _material = renderer.material;
    }

    public virtual void Update() {
        if (_followObject != null) {
            Vector3 newPos = _followObject.GetPosition(_followIndex);
            transform.position = Vector3.Lerp(
                transform.position,
                newPos,
                followSpeed * Time.deltaTime
            );
        }
    }

    // Is put on a section and follows the position point
	public virtual void MoveTo(FigureHolder figureHolder, int followIndex) {
        // TODO: Spawn sound when there is no space
        bool isNewMapSectionSelected = _followObject != null && figureHolder != _followObject;
        // If we are just moving to available space inside a figureHolder then we don't need to remove ourselves
        if (isNewMapSectionSelected) {
            _followObject.RemoveFigure(this);
        }
        // Only if we actually moved we add a figure
        if (_followObject == null || isNewMapSectionSelected) {
            // TODO: Verify that no check is needed:
            // if (figures.Count >= positionPoints.Length)
            figureHolder.AddFigure(this);
        }
        _followIndex = followIndex;
        _followObject = figureHolder;
	}

    public override void OnHoverEnter() {
        TweenEmission(new Color(0.3f, 0.3f, 0.3f));
    }

    public override void OnHoverExit() {
        TweenEmission(Color.black);
    }
}
