using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour {

	// Height of dirt part under the each tile
	public const float BOTTOM_GAP = 0.15f;
	// Sprite size is set to 1 height and 1 width.
	public static Size size = new Size (1, 1);

	public TileTypes type { get; set; }

	protected SpriteRenderer spriteRender {
		get { return GetComponent<SpriteRenderer> (); }
	}

	public abstract bool PositionSelf (Int2 point);

	/// <summary>
	/// Converts tile point to world position
	/// </summary>
	/// <returns>Returns world position for the given point</returns>
	public static Vector2 GetWorldPosition (Int2 point) {
		return new Vector2 (
			point.x * size.half_width - point.y * size.half_height,
			(point.x * size.half_width + point.y * size.half_height) * 0.5f
		);
	}

	protected void SetSortingOrder () {
		spriteRender.sortingOrder = (int) -(transform.position.y * 10);
	}
}
