using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class PowerUpHolder : FigureHolder {
    public Transform[] spawnPrefabs;
    public Transform playerPrefab;

    public Transform spawnIdentity;
    public float resizeByOnUpgrade = 0.6f;

    private int level = 3;

    // Shortcuts for power up selectors
    private KeyCode[] keyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
    };


    void Start() {
        _initialScale = transform.localScale;
    }

    void OnDrawGizmos() {
        if (Application.isEditor) {
            Gizmos.color = Color.blue;
            for (var n = 0; n < level; n++) {
                Gizmos.DrawCube(GetSpawnLocation(n), new Vector3(0.5f, 0.5f, 0.5f));
            }
        }
    }

    public bool CanUpgrade() {
        int term = GameManager.instance.GetTerm();
        int maxPowerUplevel = GameManager.instance.maxPowerUplevel;
        return level < Mathf.Min(term + 3, GameManager.instance.maxPowerUplevel);
    }

    public void Upgrade() {
        level += 1;
        Vector3 newScale = new Vector3((float)level * resizeByOnUpgrade, 0f, 0f);
        TweenScale(newScale);
    }

    public void ResetLevel() {
        level = 3;
        Vector3 newScale = new Vector3((float)level * resizeByOnUpgrade, 0f, 0f);
        TweenScale(newScale);
        figures.ForEach(delegate(Figure obj) {
            obj.RemoveFromGame();
        });
    }

    void Update() {
        for(int i = 0 ; i < keyCodes.Length; i ++ ){
            if(Input.GetKeyDown(keyCodes[i])){
                AudioManager.instance.Play("Click");
                TrySelect(i);
                break;
            }
        }
    }

    private void TrySelect(int index) {
        if (figures.Count > index) {
            figures[index].OnClick();
        }
    }

    public void SpawnPowerUps() {
        if (figures.Count < level) {
            FillPad();
        }
    }

    private Vector3 GetSpawnLocation(int index) {
        float count = (float)index + 1;
        Vector3 newPos = spawnIdentity.localPosition + Vector3.left * 1.4f;
        Vector3 newWorldPos = spawnIdentity.TransformPoint(newPos);
        return Vector3.Lerp(spawnIdentity.position, newWorldPos, (float)count / (level + 1));
    }

    public override Vector3 GetPosition(int followIndex) {
        return GetSpawnLocation(followIndex);
    }


    public void StartGame() {
        SpawnPlayer();
        FillPad();
    }

    public void FillPad() {
        while (figures.Count < level) {
            SpawnPowerUp();
        }
    }

    public void SpawnPlayer() {
        SpawnPrefab(playerPrefab);
    }

    // PLAYER specific stuff
    private void SpawnPowerUp() {
        int index = Random.Range(0, spawnPrefabs.Length - 1);
        Transform obj = spawnPrefabs[index];
        SpawnPrefab(obj);
    }

    public void SpawnPrefab(Transform prefab) {
        Transform spawn = Instantiate(prefab, GetPosition(0) + Vector3.up * 5, Quaternion.identity);
        Figure spawnFig = spawn.GetComponent<Figure>();
        spawnFig.MoveTo(this, figures.Count);
    }
}
