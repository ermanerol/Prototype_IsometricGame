using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World { //TODO World constants and helper class. We might want to change this from static

	public const int SIZE_Y = 20;
	public const int SIZE_X = 20;

	public static Int2 GetTile (Vector2 worldPos) {
		var pos = new Int2(0, 0);
		pos.x = (int) ((2 * worldPos.y + worldPos.x));
		pos.y = (int) ((2 * worldPos.y - worldPos.x));
		return(pos);
	}
}
