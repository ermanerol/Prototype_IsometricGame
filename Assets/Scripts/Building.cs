using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO We need to make this abstract or interface things
public class Building : Tile {

	public const float SURROUNDING_GAP = 0.04f;
	public const float BUILDING_GAP = 0f;//.05f;

	private Int2 point;

	private BuildingData set;
	private SpriteRenderer sRenderer;

	public void SetBuilding (BuildingData set) {
		this.set = set;
		sRenderer = GetComponent<SpriteRenderer> ();
		sRenderer.sprite = set.sprite;
	}

	public bool PositionBuilding (Int2 point) {
		var buildable = WorldManager.IsWorldTileBuildable (point, set.size);
		if (buildable) {
			sRenderer.color = Color.white;
		}
		else {
			sRenderer.color = Color.red;
		}

		this.point = point;
		var pos = GetTilePositon (point);
		pos.y += set.size.height * (SURROUNDING_GAP + BUILDING_GAP) + Tile.BOTTOM_GAP;
		sRenderer.sortingOrder = (int) -(pos.y * 10);
		transform.position = pos;
		return buildable;
	}

	public void PutDownBuilding (Int2 point) {
		if (!PositionBuilding (point))
			return;
		
		WorldManager.SetWorldTileData (point, set.size);
		sRenderer.color = Color.white;
		set.BuildingBuilt ();
	}
}