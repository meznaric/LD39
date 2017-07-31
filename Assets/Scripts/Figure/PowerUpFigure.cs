using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFigure : Figure {
    public enum PowerUpFigureType { Speaker, Microphone };
    public PowerUpFigureType powerUpFigureType;

    private int livesForSteps;
    // Starts dying on first move
    int movedToBoardAt = 0;
    bool destroying = false;

    void Start() {
        base.Start();
        powerUpFigureType = (PowerUpFigureType)Random.Range(0, 1);
        livesForSteps = GetDuration();
    }

    private int GetDuration() {
        switch(powerUpFigureType) {
            case PowerUpFigureType.Microphone:
                return GameManager.instance.microphoneDurationSteps;
            default:
                return GameManager.instance.speakerDurationSteps;
        }
    }

    public override void OnClick() {
        // TODO: Disable moving once placed ?
        GameManager.instance.OnClick(this);
    }

    void Update() {
        int steps = GameManager.instance.GetStep();
        if (!destroying && movedToBoardAt != 0 && movedToBoardAt + livesForSteps == steps) {
            destroying = true;
            _followObject.RemoveFigure(this);
            TweenScale(Vector3.zero, renderer.transform);
            Destroy(gameObject, 1.1f);
        }
        base.Update();
    }

    public override void MoveTo(FigureHolder fh, int followIndex) {
        int steps = GameManager.instance.GetStep();
        // Start dying when moved to board
        if (fh.GetComponent<MapSection>() != null) {
            movedToBoardAt = steps;
        }
        base.MoveTo(fh, followIndex);
    }
}
