using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

	void Update () {
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_STANDALONE_OSX
		ControlMouse ();
		#else
		ControlTouch ();
		#endif
	}

	private static bool mouseReleasedAfterBuildingSet = true;
	private static Building building = null;

	/// <summary>
	/// Set Building to be positioned
	/// </summary>
	public static void SetBuilding (Building newBuilding) {
		building = newBuilding;
		mouseReleasedAfterBuildingSet = false;
		GameStateManager.SetGameState (GameStates.Building);
	}

	private void ControlTouch () {
		if (Input.touchCount == 0)
			return;

//		var touch = Input.GetTouch (0);
	}

	/// <summary>
	/// If building is set move with mouse pointer and PutDown if mouse is released
	/// </summary>
	private void ControlMouse () {
		if (!building) {
			return;
		}

		var point = World.GetTile (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		//Debug.Log ("point : " + point);

		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			if (!mouseReleasedAfterBuildingSet) { // We're preventing putting down building as soon as the building selected
				mouseReleasedAfterBuildingSet = true;
				return;
			}
			if (!building.PutDownSelf (point))
				return;
			building = null;
			GameStateManager.SetGameState (GameStates.Playing);
		}
		else {
			building.PositionSelf (point);
		}
	}
}
