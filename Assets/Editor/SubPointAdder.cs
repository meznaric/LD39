using UnityEngine;
using UnityEditor;
using System.Collections;

class SubPointAdder : EditorWindow {
	[MenuItem ("Utils/Add Sub Nodes to All")]

	public static void  AddSubNodesToAll () {
		int i = 0;
		foreach( GameObject go in Selection.gameObjects )
		{
			GameObject positionPoint = new GameObject ("PositionPoint"+i);
			positionPoint.transform.parent = go.transform;
			positionPoint.transform.position = go.GetComponent<Renderer>().bounds.center;
			go.GetComponent<MapSection> ().positionPoints = new Transform[]{positionPoint.transform};
			i += 1;
		}
	}


}