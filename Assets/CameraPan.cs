using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour {

	private float speed = 2;
	private Vector3 lastPos;
	private Vector3 startPos;

	void Update () {
		if (GameStateManager.state != GameStates.Playing)
			return;
		
		if (Input.GetMouseButtonDown (0)) {
			lastPos = transform.position;
			startPos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		}

		if (Input.GetMouseButton (0)) {
			Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition) - startPos;
			transform.position = lastPos + -pos * speed;
		}
	}
}
