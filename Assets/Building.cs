using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO We need to make this abstract or interface things
public class Building : Tile {

	public const float SURROUNDING_GAP = 0.025f;
	public const float BUILDING_GAP = 0.05f;

	private BuildingData set;

	public void SetBuilding (BuildingData set) {
		this.set = set;
		GetComponent<SpriteRenderer> ().sprite = this.set.sprite;
	}

	public void SetOnTopOfTile (Int2 point) {
		var pos = GetTilePositon (point);
		pos.y += set.size.height * (BUILDING_GAP + SURROUNDING_GAP) + Tile.BOTTOM_GAP;
		transform.position = pos;
	}
}