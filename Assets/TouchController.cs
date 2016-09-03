using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

	void Update () {
		#if UNITY_EDITOR //TODO Add other platforms
		ControlMouse ();
		#else
		ControlTouch ();
		#endif
	}
	private static Building building = null;
	public static void SetBuilding (Building newBuilding) {
		building = newBuilding;
	}

	private void ControlTouch () {
		if (Input.touchCount == 0)
			return;

//		var touch = Input.GetTouch (0);
	}

	private void ControlMouse () {
		if (!building)
			return;
		
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			System.Console.WriteLine ("Key up");
			building = null;
		}
		else {
			Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			var point = World.GetTile (pos);
			Debug.Log ("point " + point);
			building.SetOnTopOfTile (point);
//			building.transform.position = pos;
		}
	}
}
