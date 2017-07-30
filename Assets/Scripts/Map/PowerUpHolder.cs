using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class PowerUpHolder : FigureHolder {

    public Transform[] playerPrefabs;

    public int maxLevel = 6;
    public Transform spawnIdentity;
    public float resizeByOnUpgrade = 0.5f;

    private int level = 2;

    void Awake () {
        // TODO: Manage spawn instances
        // TODO: Extend it so it tracks placed figures
    }

    void Start() {
        // TODO: Spawn coroutine that spawns figures
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
        return level <= maxLevel;
    }

    public void Upgrade() {
        level += 1;
        Vector3 newScale = new Vector3((float)level * resizeByOnUpgrade, 0f, 0f);
        TweenScale(newScale);
    }

    public void SpawnPowerUp() {
        if (figures.Count < level) {
            SpawnPlayer();
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
    }

    // PLAYER specific stuff
    private void SpawnPlayer() {
        int index = Random.Range(0, playerPrefabs.Length - 1);
        Transform obj = playerPrefabs[index];
        Transform player = Instantiate(obj, GetPosition(0) + Vector3.up * 5, Quaternion.identity);
        Figure playerFig = player.GetComponent<Figure>();
        playerFig.MoveTo(this, figures.Count);
    }
}