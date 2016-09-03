using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public const float BOTTOM_GAP = 0.25f;

	public Size size;

	public void SetTile (Int2 point) {
		var pos = new Vector2 (
			point.x * 0.5f - point.y * 0.5f,
			(point.x * 0.5f + point.y * 0.5f) / 2
		);
		transform.position = pos;
	}
}
