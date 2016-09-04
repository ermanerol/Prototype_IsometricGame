﻿using System.Collections;
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

	private static bool mouseReleasedAfterBuildingSet = true;
	private static Building building = null;

	public static void SetBuilding (Building newBuilding) {
		building = newBuilding;
		mouseReleasedAfterBuildingSet = false;
	}

	private void ControlTouch () {
		if (Input.touchCount == 0)
			return;

//		var touch = Input.GetTouch (0);
	}

	private void ControlMouse () {
		if (!building)
			return;

		var point = World.GetTile (Camera.main.ScreenToWorldPoint (Input.mousePosition));

		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			if (!mouseReleasedAfterBuildingSet) { // We're preventing putting down building as soon as the building selected
				mouseReleasedAfterBuildingSet = true;
				return;
			}
			building.PutDownBuilding (point);
			building = null;
		}
		else {
			building.PositionBuilding (point);
		}
	}
}