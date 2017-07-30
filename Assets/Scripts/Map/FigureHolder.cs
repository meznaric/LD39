using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class FigureHolder : ClickablePiece {
	// All the figures on current section, used to evaluate score
    public List<Figure> figures = new List<Figure>();

    public void RemoveFigure(Figure f) {
        figures.Remove(f);
        // Move all others
        for (int n = 0; n < figures.Count; n++) {
            figures[n].MoveTo(this, n);
        }
    }

    public void AddFigure(Figure f) {
        figures.Add(f);
    }

    public virtual Vector3 GetPosition(int _followIndex) {
        return Vector3.zero;
    }
}
