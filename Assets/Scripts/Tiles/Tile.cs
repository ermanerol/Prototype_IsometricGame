using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tile object with visible sprite
/// </summary>
public class Tile : MonoBehaviour {

	// Height of dirt part under the each tile
	public const float BOTTOM_GAP = 0.15f;

	// Sprite size is set to 1 height and 1 width.
	public static Size size = new Size (1, 1);

	/// <summary>
	/// Positions tile on given point
	/// </summary>
	public void SetTile (Int2 point) {
		transform.position = GetTilePositon (point);;
		GetComponent<SpriteRenderer>().sortingOrder = (int) -(transform.position.y * 10);
	}

	/// <summary>
	/// Converts tile point to world position
	/// </summary>
	/// <returns>Returns world position for the given point</returns>
	public static Vector2 GetTilePositon (Int2 point) {
		return new Vector2 (
			point.x * size.half_width - point.y * size.half_height,
			(point.x * size.half_width + point.y * size.half_height) * 0.5f
		);
	}
}
