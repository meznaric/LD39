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

    public float clockPercCost = 0.1f;

    public Vector3 tooltipOffset = Vector3.up;


	public Color primaryColor = Color.blue;
	public Color enemyColor = Color.red;
	public Player player;

	public static GameManager instance;

    private Figure selectedFigure;
    private int totalSize = 1;

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
        switch (powerUp.powerUpType) {
            case UI3DPowerUp.PowerUpType.MoreSpace:
                PlayerManager.instance.powerUpHolder.Upgrade();
                break;
            case UI3DPowerUp.PowerUpType.Clock:
                int newPowerUpInterval = Mathf.Max(3, powerUpEveryStep - 1);
                bool hadEnough = TrySpend(Mathf.FloorToInt(totalSize * clockPercCost));
                if (newPowerUpInterval != powerUpEveryStep && hadEnough) {
                    powerUpEveryStep = newPowerUpInterval;
                }
                break;
        }
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
        totalSize = 0;
        Map.instance.mapSections.ForEach(delegate(MapSection ms) {
            totalSize += ms.sectionSize;
        });
        isPlaying = true;
        StartCoroutine("StartSecondlyStep");
        StartCoroutine("StartRandomEventStep");
    }

    void OnGUI() {
        // TODO: Remove this
        GUI.Label(
            new Rect (10, 10, Screen.width-10, Screen.height-10),
            "Political power: " + power + " - Total: " + totalSize
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
            Map.instance.mapSections.ForEach(delegate(MapSection ms) {
                ms.MakeStep(term);
                newPower += ms.power;
            });
            if (step % termDurationInSteps == 0) {
                term += 1;
                // TODO: Do term shit
            }
            if (step % powerUpEveryStep == 0) {
                // TODO: It's powerup time!
            }
            power = (int)newPower;
        }
    }

    bool TrySpend(int spendPower) {
        List<MapSection> mapSections = Map.instance.mapSections;

        if (power > spendPower) {
            // Try to spend everything on 1/2 the mapsections
            int spendPerSection = Mathf.FloorToInt(spendPower / (mapSections.Count / 2));
            int newPower = 0;
            int spent = 0;
            mapSections.ForEach(delegate(MapSection ms) {
                if (spent >= spendPower) return; 
                // Don't spend more than a field has
                int maxSpend = Mathf.Min(spendPerSection, ms.power);
                ms.power -= maxSpend;
                newPower += ms.power;
                spent += maxSpend;
            });
            power = newPower;
            return true;
        } else {

            return false;
        }
    }
}
