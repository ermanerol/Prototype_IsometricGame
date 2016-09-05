using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World {

	public const int SIZE_Y = 25;
	public const int SIZE_X = 25;

	/// <summary>
	/// Converts world position to tile point
	/// </summary>
	/// <returns>Returns tile point for the given world position</returns>
	public static Int2 GetTile (Vector2 worldPos) {
		var pos = new Int2(0, 0);
		pos.x = (int) ((2 * worldPos.y + worldPos.x));
		pos.y = (int) ((2 * worldPos.y - worldPos.x));
		return(pos);
	}
}

public enum TileTypes {
	NotDefined = 0,
	Free = 1,
	BuildingCore = 2,
	BuildingPart = 3
};
