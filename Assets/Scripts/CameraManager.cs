using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class CameraManager : MonoBehaviour {
    public static CameraManager instance;

    public Transform gamePosition;
    public Transform spinStroyPosition;
    public Transform menuPosition;
    public GameObject camera;

    void Awake() {
        instance = this;
    }

    public void GoToMenu() {
        camera.Tween(
            "CameraMovement",
            camera.transform.position,
            menuPosition.position,
            1.0f,
            TweenScaleFunctions.CubicEaseOut,
            (t) => { camera.transform.position = t.CurrentValue; },
            (t) => {}
        );
        camera.Tween("CameraRotation",
            camera.transform.rotation,
            menuPosition.rotation,
            1.0f,
            TweenScaleFunctions.CubicEaseInOut,
            (t) => { camera.transform.rotation = t.CurrentValue; },
            (t) => {}
        );
    }

    public void GoToGame() {
        camera.Tween(
            "CameraMovement",
            camera.transform.position,
            gamePosition.position,
            1.0f,
            TweenScaleFunctions.CubicEaseOut,
            (t) => { camera.transform.position = t.CurrentValue; },
            (t) => {}
        );
        camera.Tween("CameraRotation",
            camera.transform.rotation,
            gamePosition.rotation,
            1.0f,
            TweenScaleFunctions.CubicEaseInOut,
            (t) => { camera.transform.rotation = t.CurrentValue; },
            (t) => {}
        );
    }

    public void GoToSpinStory() {
        camera.Tween(
            "CameraMovement",
            camera.transform.position,
            spinStroyPosition.position,
            2.0f,
            TweenScaleFunctions.CubicEaseOut,
            (t) => { camera.transform.position = t.CurrentValue; },
            (t) => {}
        );
        camera.Tween("CameraRotation",
            camera.transform.rotation,
            spinStroyPosition.rotation,
            1.6f,
            TweenScaleFunctions.CubicEaseInOut,
            (t) => { camera.transform.rotation = t.CurrentValue; },
            (t) => {}
        );
    }
}
