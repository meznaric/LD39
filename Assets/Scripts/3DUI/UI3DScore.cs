using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI3DScore : MonoBehaviour {

    public Transform fullBar;
    private float followSpeed = 10;

    public Renderer fullBarRenderer;
    private Material fullBarMaterial;

    void Start() {
        fullBarMaterial = fullBarRenderer.material;
    }

    void Update() {
        int step = GameManager.instance.GetStep();
        int power = GameManager.instance.power;
        int totalSize = GameManager.instance.GetTotalSize();
        float scorePerc = (float)power/totalSize;

        float posPerc = Mathf.Lerp(0, 10, scorePerc);

        fullBar.localScale = Vector3.Lerp(
            fullBar.localScale,
            new Vector3(posPerc, fullBar.localScale.y, fullBar.localScale.z),
            Time.deltaTime * followSpeed
        );

        // Starts at 0.5 and lerps to 0.6
        float colorPerc = Mathf.Min(1f, Mathf.Max(0f, (scorePerc - 0.5f) * 10));


        fullBarMaterial.color = Color.Lerp(
            GameManager.instance.enemyColor,
            GameManager.instance.primaryColor,
            colorPerc
        );
    }
}
