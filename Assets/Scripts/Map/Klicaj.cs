using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class Klicaj : ClickablePiece {

    int step = 0;
    float checkDuration = 0.4f;

    protected override void Start() {
        base.Start();
        StartCoroutine("AdjustScale");
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
    }

    IEnumerator AdjustScale() {
        while (true) {
            yield return new WaitForSeconds(checkDuration);
            step += 1;
            float currentPerc = (float)GameManager.instance.power/GameManager.instance.GetTotalSize();
            Debug.Log(currentPerc);
            if (currentPerc < 0.5f) {
                ScaleTo(step % 2 == 0 ? 700 : 1000);
            } else {
                ScaleTo(0);
            }
        }
    }


    public void ScaleTo(float num) {
        Vector3 to = new Vector3(transform.localScale.x, transform.localScale.y, num);
		gameObject.Tween("AdjustKlicaj", transform.localScale, to, checkDuration, TweenScaleFunctions.CubicEaseOut, (t) => {
            transform.localScale = t.CurrentValue;
        }, (t) => {});
    }
}
