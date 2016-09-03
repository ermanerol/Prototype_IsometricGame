using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO We need to make this abstract or interface things
public class Building : Tile {

	private BuildingData set;

	public void SetBuilding (BuildingData set) {
		this.set = set;
		GetComponent<SpriteRenderer> ().sprite = this.set.sprite;
	}

	public void SetOnTopOfTile (Int2 point) {
//		x += 1 - set.size.width; //TODO Should we use bottom corner to place ?
//		y += 1 - set.size.height;
		var vec = new Vector3 (
			point.x * 0.5f - point.y * 0.5f,
			(point.x * 0.5f + point.y * 0.5f) / 2 + Tile.BOTTOM_GAP,
			-1
		);
		transform.position = vec;
	}
}