using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Building object with visible sprite
/// </summary>
public class Building : Tile {

	public const float SURROUNDING_GAP = 0.02f;
	public const float BUILDING_GAP = 0.05f;

	private bool buildingPlaced = false;

	private BuildingData buildingData;
	private SpriteRenderer sRenderer;

	private Int2 placedPoint;

	/// <summary>
	/// Assign building data, set sprite and add trigger to catch click events.
	/// </summary>
	public void SetBuilding (BuildingData buildingData) {
		this.buildingData = buildingData;
		sRenderer = GetComponent<SpriteRenderer> ();
		sRenderer.sprite = buildingData.sprite;
		gameObject.AddComponent<PolygonCollider2D> ().isTrigger = true;
	}

	/// <summary>
	/// Move building to world position and set color to red if not buildable
	/// </summary>
	/// <returns>Returns true if position is buildable</returns>
	public bool PositionSelf (Int2 point) {
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

	/// <summary>
	/// If in buildable position place building and set building data to world tile.
	/// </summary>
	/// <returns>Returns true if building is put down</returns>
	public bool PutDownSelf (Int2 point) {
		if (!PositionSelf (point))
			return false;

		placedPoint = point;
		WorldManager.SetWorldTileData (point, buildingData.buildingIndex, buildingData.size);
		sRenderer.color = Color.white;
		buildingData.BuildingBuilt ();
		buildingPlaced = true;
		return true;
	}

	/// <summary>
	/// Place building and set building data to world tile. Used at first world load.
	/// </summary>
	public void PutDownSelfWithoutControl (Int2 point) {
		PositionSelf (point);
		placedPoint = point;
		WorldManager.SetWorldTileData (point, buildingData.buildingIndex, buildingData.size, true);
		sRenderer.color = Color.white;
		buildingData.BuildingBuilt ();
		buildingPlaced = true;
	}

	/// <summary>
	/// Destroy building and reset building data on the world tile.
	/// </summary>
	public void DestroySelf () {
		buildingData.BuildingDestroyed ();
		WorldManager.ResetWorldTileData (placedPoint, buildingData.size);
		Destroy (gameObject);
	}

	/// <summary>
	/// Show building detail popup if building is placed and game state is playing.
	/// </summary>
	void OnMouseUp () {
		if (!buildingPlaced)
			return;

		if (GameStateManager.state != GameStates.Playing)
			return;

		PopupManager.ShowBuildingDetailPopup(this, buildingData);
	}
}