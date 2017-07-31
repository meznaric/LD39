using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFigure : Figure {
    public enum PowerUpFigureType { Speaker, Microphone, Dish };
    public PowerUpFigureType powerUpFigureType;

    private int livesForSteps;
    // Starts dying on first move
    int movedToBoardAt = 0;
    bool destroying = false;
    bool locked = false;

    void Start() {
        base.Start();
        livesForSteps = GetDuration();
    }

    private int GetDuration() {
        switch(powerUpFigureType) {
            case PowerUpFigureType.Microphone:
                return GameManager.instance.microphoneDurationSteps;
            case PowerUpFigureType.Dish:
                return GameManager.instance.dishDurationSteps;
            default:
                return GameManager.instance.speakerDurationSteps;
        }
    }

    public override int GetPowerChange() {
        switch (powerUpFigureType) {
            case PowerUpFigureType.Microphone:
                return GameManager.instance.microphonePowerChange;
            case PowerUpFigureType.Speaker:
                return GameManager.instance.speakerPowerChange;
            case PowerUpFigureType.Dish:
                return GameManager.instance.dishPowerChange;
            default:
                return base.GetPowerChange();
        }
    }

    public override void OnHoverEnter() {
        if (locked) return;
        base.OnHoverEnter();
    }

    public override void OnHoverExit() {
        if (locked) return;
        base.OnHoverExit();
    }

    public override void OnClick() {
        if (locked) return;
        GameManager.instance.OnClick(this);
        base.OnClick();
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
        if (destroying) return;
        int steps = GameManager.instance.GetStep();
        // Start dying when moved to board
        if (fh.GetComponent<MapSection>() != null) {
            movedToBoardAt = steps;
            locked = true;
        }
        base.MoveTo(fh, followIndex);
    }
}
