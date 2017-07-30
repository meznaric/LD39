using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class PowerUpHolder : ClickablePiece {

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
        // TODO: Spawn powerup
    }

    private Vector3 GetSpawnLocation(int index) {
        float count = (float)index + 1;
        Vector3 newPos = spawnIdentity.localPosition + Vector3.left * 1.4f;
        Vector3 newWorldPos = spawnIdentity.TransformPoint(newPos);
        return Vector3.Lerp(spawnIdentity.position, newWorldPos, (float)count / (level + 1));
    }
}
