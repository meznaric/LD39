using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int power = 1000;
    // Each term, randomness gets pulled to negative number
    public float hardnessTermFactor = 1;
    public float negativePullFactor = 1;

    private int term = 1;
    private int step = 0;
    private bool isPlaying = false;
	// How much extra are states scaled on Z axis
	public float scaleFactor = 10.0f;
	public float scaleOnHover = 100f;
	public float scaleOnClick = 55f;

    public float figurePointsPerStep = 30f;

    public float gameStepDurationInSec = 1f;
    public int termDurationInSteps = 30;
    public float randomEventIntervalInSec = 30.3f;
    public int powerUpEveryStep = 15;

    public Vector3 tooltipOffset = Vector3.up;


	public Color primaryColor = Color.blue;
	public Color enemyColor = Color.red;
	public Player player;

	public static GameManager instance;

    private Figure selectedFigure;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

    void Start() {
        StartCoroutine("StartGame");
    }

    public int GetTerm() {
        return term;
    }

    public int GetStep() {
        return step;
    }


    // Selections and moving management

    public void OnClick(UI3DPowerUp powerUp) {
        // TODO: Implement powerup logic
    }

    public void OnClick(MapSection ms) {
        if (selectedFigure != null) {
            if (ms.positionPoints.Length > ms.figures.Count) {
                selectedFigure.MoveTo(ms, ms.figures.Count);
                selectedFigure = null;
            }
        }
    }

    public void OnClick(Player p) {
        selectedFigure = p;
    }


    // Game loop managers

    public bool getIsPlaying() {
        return isPlaying;
    }


    IEnumerator StartGame() {
        // TODO: Remove delay, wait for player to start
        yield return new WaitForSeconds(3f);
        Debug.Log("Starting the game");
        isPlaying = true;
        StartCoroutine("StartSecondlyStep");
        StartCoroutine("StartRandomEventStep");
    }

    void OnGUI() {
        // TODO: Remove this
        GUI.Label(
            new Rect (10, 10, Screen.width-10, Screen.height-10),
            "Political power: " + power
        );
    }

    IEnumerator StartRandomEventStep() {
        while (isPlaying) {
            yield return new WaitForSeconds(randomEventIntervalInSec);
        }
    }

    IEnumerator StartSecondlyStep() {
        while (isPlaying) {
            yield return new WaitForSeconds(gameStepDurationInSec);
            step += 1;
            float newPower = 0;
            float totalSize = 0;
            Map.instance.mapSections.ForEach(delegate(MapSection ms) {
                ms.MakeStep(term);
                newPower += ms.power;
                totalSize += ms.sectionSize;
            });
            if (step % termDurationInSteps == 0) {
                term += 1;
                // TODO: Do term shit
            }
            power = (int)newPower;
        }
    }
}
