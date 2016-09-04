using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	// Height of ground part under the each tile
	public const float BOTTOM_GAP = 0.15f;

	// Tile size is set to 1 height and 1 width
	public static Size size = new Size (1, 1);

	public void SetTile (Int2 point) {
		var pos = GetTilePositon (point);
		transform.position = pos;
	}

	public static Vector2 GetTilePositon (Int2 point) {
		return new Vector2 (
			point.x * size.half_width - point.y * size.half_height,
			(point.x * size.half_width + point.y * size.half_height) * 0.5f
		);
	}
}
