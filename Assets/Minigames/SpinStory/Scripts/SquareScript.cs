using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareScript : MonoBehaviour {

    private void OnMouseDown()
    {
        int rot = (int)(transform.localRotation.eulerAngles.z);

        switch (rot)
        {
            case 270: transform.localRotation = Quaternion.Euler(0, 0, 0); break;
            case 0: transform.localRotation = Quaternion.Euler(0, 0, 90); break;
            case 90: transform.localRotation = Quaternion.Euler(0, 0, 180); break;
            case 180: transform.localRotation = Quaternion.Euler(0, 0, 270); break;
            default: break;
        }
    }
}
