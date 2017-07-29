using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUManager : MonoBehaviour {

    public float cpuStepEverySec = 3.0f;

    public List<CPUPlayer> figures = new List<CPUPlayer>();
    public Transform mapSectionHolder;

    void Start() {
        StartCoroutine("StartPlaying");
    }

    IEnumerator StartPlaying() {
        while (true) {
            yield return new WaitForSeconds(cpuStepEverySec);
            if (GameManager.instance.getIsPlaying() && figures.Count > 0) {
                // Move figure to random position randomly
                MapSection[] mapSections = mapSectionHolder.GetComponentsInChildren<MapSection>();
                int tryMoveTo = Random.Range(0, mapSections.Length);
                MapSection ms = mapSections[tryMoveTo];
                if (ms.positionPoints.Length > ms.figures.Count) {
                    int tryMoveFigure = Random.Range(0, figures.Count - 1);
                    figures[tryMoveFigure].MoveTo(ms, ms.figures.Count);
                }
            }
        }
    }
}
