using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFigure : Figure {

    public float livesForSec;
    // Starts dying on first move
    bool dying = false;

    public override void OnClick() {
        // TODO: Disable moving once placed ?
        GameManager.instance.OnClick(this);
    }

    public override void MoveTo(FigureHolder fh, int followIndex) {
        // Start dying when moved to board
        if (fh.GetComponent<MapSection>() != null) {
            if (!dying) {
                StartCoroutine("PowerUpExpired");
            }
            dying = true;
        }
        base.MoveTo(fh, followIndex);
    }

    IEnumerator PowerUpExpired() {
        // moment of silence for animations :(
        yield return new WaitForSeconds(livesForSec);
        transform.localScale = new Vector3(0.85f, 0.8f, 0.85f);
        yield return new WaitForSeconds(0.20f);
        transform.localScale = new Vector3(0.55f, 0.5f, 0.55f);
        yield return new WaitForSeconds(0.20f);
        transform.localScale = new Vector3(0.15f, 0.2f, 0.15f);
        yield return new WaitForSeconds(0.20f);
        if (_followObject != null) {
            _followObject.RemoveFigure(this);
        }
        Destroy(gameObject);
    }
}
