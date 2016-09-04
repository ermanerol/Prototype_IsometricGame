using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO We need to make this abstract or interface things
public class Building : Tile {

	public const float SURROUNDING_GAP = 0.04f;
	public const float BUILDING_GAP = 0.05f;

	private bool buildingPlaced = false;

	private BuildingData buildingData;
	private SpriteRenderer sRenderer;

	public void SetBuilding (BuildingData buildingData) {
		this.buildingData = buildingData;
		sRenderer = GetComponent<SpriteRenderer> ();
		sRenderer.sprite = buildingData.sprite;
		gameObject.AddComponent<PolygonCollider2D> ().isTrigger = true;
	}

	public bool PositionBuilding (Int2 point) {
		var buildable = WorldManager.IsWorldTileBuildable (point, buildingData.size);
		if (buildable) {
			sRenderer.color = Color.white;
		}
		else {
			sRenderer.color = Color.red;
		}

		var pos = GetTilePositon (point);
		pos.y += buildingData.size.height * (SURROUNDING_GAP + BUILDING_GAP) + Tile.BOTTOM_GAP;
		sRenderer.sortingOrder = (int) -(pos.y * 10);
		transform.position = pos;
		return buildable;
	}

	public void PutDownBuilding (Int2 point) {
		if (!PositionBuilding (point))
			return;
		
		WorldManager.SetWorldTileData (point, buildingData.size);
		sRenderer.color = Color.white;
		buildingData.BuildingBuilt ();
		buildingPlaced = true;
	}

	public void DestroyBuilding () {
		buildingData.BuildingDestroyed ();
		Destroy (gameObject);
	}

	void OnMouseUp () {
		if (!buildingPlaced)
			return;

		if (GameStateManager.state != GameStates.Playing)
			return;

		PopupManager.ShowBuildingDetailPopup(this, buildingData);
	}
}