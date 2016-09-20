using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ground tile object
/// </summary>
public class Ground : Tile {

	public bool isFree { get { return type == TileTypes.Ground; } }

	/// <summary>
	/// Positions tile on given point
	/// </summary>
	public override bool PositionSelf (Int2 point) {
		transform.position = GetWorldPosition (point);;
		SetSortingOrder ();
		type = PlayerPrefsHelper.ReadTileData (point);
		return true;
	}
}
