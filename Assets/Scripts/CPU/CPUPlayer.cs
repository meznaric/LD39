using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUPlayer : Figure {
    public override int GetPowerChange() {
        return (int)(-GameManager.instance.figurePointsPerStep * GameManager.instance.enemyFigurePower);
    }
}
