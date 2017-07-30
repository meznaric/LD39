using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DPowerUpClock : UI3DPowerUp {

    public Transform termTick;
    private Quaternion _termInitRotation;

    public Transform powerUpTick;
    private Quaternion _powerUpInitRotation;

    void Awake() {
        _termInitRotation = termTick.rotation;
        _powerUpInitRotation = powerUpTick.rotation;
    }

    void Update() {
        float percToTerm = getPercToTerm();
        float percToPowerUp = getPercToPowerUp();
        UpdateTickRotation(percToTerm, _termInitRotation, termTick);
        UpdateTickRotation(percToPowerUp, _powerUpInitRotation, powerUpTick);
    }

    float getPercToTerm() {
        int step = GameManager.instance.GetStep();
        int termDurationInSteps = GameManager.instance.termDurationInSteps;
        int stepInTerm = (int)step % termDurationInSteps;
        return (float)stepInTerm / termDurationInSteps;
    }

    float getPercToPowerUp() {
        int step = GameManager.instance.GetStep();
        int powerUpEveryStep = GameManager.instance.powerUpEveryStep;
        int stepInTerm = (int)step % powerUpEveryStep;
        return (float)stepInTerm / powerUpEveryStep;
    }

    void UpdateTickRotation(float perc, Quaternion initRotation, Transform obj) {
        Quaternion rotR = Quaternion.AngleAxis(Mathf.Lerp(360, 0, perc), Vector3.up);
        obj.rotation = Quaternion.Lerp(obj.rotation, initRotation * rotR, Time.deltaTime * 5f);
    }
}
