using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFigure : Figure {

    public float livesForSteps;
    // Starts dying on first move
    int movedToBoardAt = 0;

    public override void OnClick() {
        // TODO: Disable moving once placed ?
        GameManager.instance.OnClick(this);
    }

    public override void MoveTo(FigureHolder fh, int followIndex) {
        int steps = GameManager.instance.GetStep();
        // Start dying when moved to board
        if (fh.GetComponent<MapSection>() != null) {
            movedToBoardAt = steps;
        }
        base.MoveTo(fh, followIndex);
        if (movedToBoardAt != 0 && movedToBoardAt + livesForSteps == steps) {
            TweenScale(Vector3.zero);
            if (_followObject != null) {
                _followObject.RemoveFigure(this);
            }
            Destroy(gameObject, 2.0f);
        }
    }

    IEnumerator PowerUpExpired() {
        yield return new WaitForSeconds(2.0f);
    }
}
