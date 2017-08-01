using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseConfigurations;

public class GameManager : MonoBehaviour {

    public enum GameType { Normal, SpinTheStory };
    public Difficulty difficulty = Difficulty.Medium;
    private GameType currentGameType = GameType.Normal;
    public int score = 0;
    public bool inTutorial = false;

	public int power = 1000;
    // Each term, randomness gets pulled to negative number
    public Transform[] bubblePrefabs;

    public Text morePowerUpCostText;
    public Text fasterPowerUpCostText;

    private int term = 1;
    private int step = 0;
    private bool isPlaying = false;
	// How much extra are states scaled on Z axis
	public float scaleFactor = 10.0f;
	public float scaleOnHover = 100f;
	public float scaleOnClick = 55f;

    public float gameStepDurationInSec = 1f;

    private BaseConfiguration currentConfiguration;

    public int dishPowerChange {get {
        return currentConfiguration.dishPowerChange;
    }}

    public int dishDurationSteps {get {
        return currentConfiguration.dishDurationSteps;
    }}

    public int speakerPowerChange {get {
        return currentConfiguration.speakerPowerChange;
    }}

    public int speakerDurationSteps {get {
        return currentConfiguration.speakerDurationSteps;
    }}

    public int microphonePowerChange {get {
        return currentConfiguration.microphonePowerChange;
    }}

    public int microphoneDurationSteps {get {
        return currentConfiguration.microphoneDurationSteps;
    }}

    public int maxPowerUplevel {get {
        return currentConfiguration.maxPowerUplevel;
    }}

    public float startPower {get {
        return currentConfiguration.startPower;
    }}

    public float cpuStepEverySec {get {
        return currentConfiguration.cpuStepEverySec;
    }}

    public float hardnessTermFactor {get {
        return currentConfiguration.hardnessTermFactor;
    }}

    public int negativePullFactor {get {
        return currentConfiguration.negativePullFactor;
    }}

    public int figurePointsPerStep {get {
        return currentConfiguration.figurePower;
    }}

    public int termDurationInSteps {get {
        return currentConfiguration.termDurationInSteps;
    }}

    public float randomEventIntervalInSec {get {
        return currentConfiguration.randomEventIntervalInSec;
    }}

    public int powerUpEveryStep {get {
        return currentConfiguration.powerUpEveryStep;
    } set {
        currentConfiguration.powerUpEveryStep = value;
    }}

    public int moreUpgradesCost {get {
        return currentConfiguration.moreUpgradesCost;
    }}
    public float clockPercCost {get {
        return currentConfiguration.clockPercCost;
    }}
    public int spinStoryMaxWin {get {
        return currentConfiguration.spinStoryMaxWin;
    }}
    public int spinStoryMinWin {get {
        return currentConfiguration.spinStoryMinWin;
    }}
    public float enemyFigurePower  {get {
        return currentConfiguration.enemyFigurePower;
    }}
    public Vector3 tooltipOffset = Vector3.up;


	public Color primaryColor = Color.blue;
	public Color enemyColor = Color.red;

	public static GameManager instance;

    private int totalSize = 1;

	// Use this for initialization
	void Awake () {
		instance = this;
        BaseConfiguration.GenerateBaseConfigurations();
        currentConfiguration = (BaseConfiguration)BaseConfiguration.baseConfigurations[(int)difficulty].Clone();
	}

    void Start() {
        AudioManager.instance.GoToMenu();
    }

    void Update() {
        if (inTutorial && Input.GetMouseButtonDown (0)) {
            CloseTutorial();
        }
    }

    public int GetTerm() {
        return term;
    }

    public int GetStep() {
        return step;
    }


    public int GetTotalSize() {
        return totalSize;
    }

    public void OnClickGoToMenu() {
        score = 0;
        CameraManager.instance.GoToMenu();
        AudioManager.instance.GoToMenu();
        PlayerManager.instance.powerUpHolder.ResetLevel();
        Map.instance.mapSections.ForEach(delegate(MapSection ms) {
            ms.power = 0;
            if (ms.activeBubble != null) {
                ms.activeBubble.StartRemoval();
            }
            ms.figures.ForEach(delegate(Figure f) {
                f.RemoveFromGame();
            });
            ms.UpdateVisualCues();
        });
    }

    // Selections and moving management

    public void OnClick(UI3DPowerUp powerUp) {
        switch (powerUp.powerUpType) {
            case UI3DPowerUp.PowerUpType.MoreSpace:
                PowerUpHolder powerUpHolder = PlayerManager.instance.powerUpHolder;
                if (
                        powerUpHolder.CanUpgrade()
                        && TrySpend(moreUpgradesCost)
                   ) {
                    powerUpHolder.Upgrade();
                    AudioManager.instance.Play("Upgrade");
                } else {
                    AudioManager.instance.Play("Denied");
                }
                break;
            case UI3DPowerUp.PowerUpType.Clock:
                int newPowerUpInterval = Mathf.Max(6, powerUpEveryStep - 1);
                bool hadEnough = TrySpend(Mathf.FloorToInt(totalSize * clockPercCost));
                if (newPowerUpInterval != powerUpEveryStep && hadEnough) {
                    powerUpEveryStep = newPowerUpInterval;
                    AudioManager.instance.Play("Upgrade");
                } else {
                    AudioManager.instance.Play("Denied");
                }
                break;
        }
    }

    public void OnClick(MapSection ms) {
        if (Figure.selectedFigure != null) {
            if (ms.positionPoints.Length > ms.figures.Count) {
                AudioManager.instance.Play("Move");
                if (ms.activeBubble != null) {
                    if(ms.activeBubble.bubbleType == Bubble.BubbleType.Bad) {
                        ms.activeBubble.StartRemoval();
                    } else if(ms.activeBubble.bubbleType == Bubble.BubbleType.FNN) {
                        StartSpinTheStory();
                        ms.activeBubble.StartRemoval();
                    }
                }
                Figure.selectedFigure.MoveTo(ms, ms.figures.Count);
                Figure.selectedFigure.OnLostSelection();
                Figure.selectedFigure = null;
            }
        }
    }

    public void OnClick(Player p) {
        if (Figure.selectedFigure) {
            Figure.selectedFigure.OnLostSelection();
        }
        Figure.selectedFigure = p;
    }

    public void OnClick(PowerUpFigure pup) {
        if (Figure.selectedFigure) {
            Figure.selectedFigure.OnLostSelection();
        }
        Figure.selectedFigure = pup;
    }

    public void OnClickStartGame() {
        StartCoroutine("StartGame");
    }

    public void OnClickTutorial() {
        inTutorial = true;
        CameraManager.instance.GoToTutorial();
    }

    public void CloseTutorial() {
        inTutorial = false;
        CameraManager.instance.GoToMenu();
    }


    // Game loop managers

    public bool getIsPlaying() {
        return isPlaying;
    }


    IEnumerator StartGame() {
        CameraManager.instance.GoToGame();
        AudioManager.instance.GoToGame();
        currentConfiguration = (BaseConfiguration)BaseConfiguration.baseConfigurations[(int)difficulty].Clone();
        yield return new WaitForSeconds(2f);
        fasterPowerUpCostText.text = "- " + (clockPercCost * 100) + "%";
        morePowerUpCostText.text = "- " + moreUpgradesCost + " P";
        Debug.Log("Starting the game");
        totalSize = 0;
        Map.instance.mapSections.ForEach(delegate(MapSection ms) {
            totalSize += ms.sectionSize;
        });
        int totalGive = (int)(totalSize * startPower);
        GivePower(totalGive);
        PlayerManager.instance.powerUpHolder.StartGame();
        isPlaying = true;
        StartCoroutine("StartSecondlyStep");
        StartCoroutine("StartRandomEventStep");
    }

    void StartSpinTheStory() {
        currentGameType = GameType.SpinTheStory;
        CameraManager.instance.GoToSpinStory();
        StorySpinner.instance.StartGame();
        AudioManager.instance.GoToSpinStory();
    }

    public void OnFinishSpinTheStory(bool haveWon) {
        StartCoroutine(DelayedGoToStory(haveWon));
    }

    IEnumerator DelayedGoToStory(bool haveWon) {
        yield return new WaitForSeconds(1.0f);
        currentGameType = GameType.Normal;
        CameraManager.instance.GoToGame();
        AudioManager.instance.PlayTerm(term);
        yield return new WaitForSeconds(1.0f);
        if (haveWon) {
            GivePower(Random.Range(spinStoryMinWin, spinStoryMaxWin));
        }

    }

    IEnumerator StartRandomEventStep() {
        List<MapSection> mapSections = Map.instance.mapSections;
        yield return new WaitForSeconds(randomEventIntervalInSec);
        while (isPlaying) {

            int i = Random.Range(0, mapSections.Count);
            MapSection mapSection = mapSections[i];

            int bi = Random.Range(0, bubblePrefabs.Length);
            Transform eventPrefab = bubblePrefabs[bi];
            mapSection.SpawnEvent(eventPrefab);
            yield return new WaitForSeconds(randomEventIntervalInSec);
        }
    }

    IEnumerator StartSecondlyStep() {
        while (isPlaying) {
            yield return new WaitForSeconds(gameStepDurationInSec);
            float newPower = 0;
            step += 1;

            Map.instance.mapSections.ForEach(delegate(MapSection ms) {
                ms.MakeStep(term);
                newPower += ms.power;
            });
            float currentPerc = newPower / totalSize;

            // Term has ended
            if (step % termDurationInSteps == 0) {
                term += 1;
                if (currentPerc < 0.5f) {
                    // User has lost
                    isPlaying = false;
                    AudioManager.instance.PlayEndgame();
                    CameraManager.instance.GoToEndgame();
                } if (currentGameType == GameType.Normal) {
                    AudioManager.instance.PlayTerm(term);
                }

            }
            if (step % powerUpEveryStep == 0 && isPlaying) {
                PlayerManager.instance.powerUpHolder.SpawnPowerUps();
            }

            if (step % termDurationInSteps >= (termDurationInSteps -  10) && isPlaying) {
                AudioManager.instance.PlayTime();
            }


            if (currentPerc > 0.5f) {
                score += (int)(newPower - (totalSize * 0.5f));
            }

            power = (int)newPower;
        }
    }

    void GivePower(int givePower) {
        List<MapSection> mapSections = Map.instance.mapSections;

        int givePerSection = Mathf.FloorToInt(givePower / (mapSections.Count / 3));
        int newPower = 0;
        int given = 0;
        mapSections.ForEach(delegate(MapSection ms) {
            if (given < givePower) {
                int maxGive = Mathf.Min(givePerSection, ms.sectionSize - ms.power);
                ms.power += maxGive;
                given += maxGive;
            }
            newPower += ms.power;
        });
        power = newPower;
    }

    bool TrySpend(int spendPower) {
        List<MapSection> mapSections = Map.instance.mapSections;

        if (power > spendPower) {
            // Try to spend everything on 1/2 the mapsections
            int spendPerSection = Mathf.FloorToInt(spendPower / (mapSections.Count / 2));
            int newPower = 0;
            int spent = 0;
            mapSections.ForEach(delegate(MapSection ms) {
                    if (spent < spendPower) {
                        // Don't spend more than a field has
                        int maxSpend = Mathf.Min(spendPerSection, ms.power);
                        ms.power -= maxSpend;
                        spent += maxSpend;
                    }
                newPower += ms.power;
            });
            power = newPower;
            return true;
        } else {

            return false;
        }
    }
}
