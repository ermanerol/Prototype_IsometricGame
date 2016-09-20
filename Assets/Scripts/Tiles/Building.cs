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

	private Int2 placedPoint;

	/// <summary>
	/// Assign building data, set sprite and add trigger to catch click events.
	/// </summary>
	public void SetBuildingData (BuildingData buildingData) {
		this.buildingData = buildingData;
		spriteRender.sprite = buildingData.sprite;
		gameObject.AddComponent<PolygonCollider2D> ().isTrigger = true;
	}

	/// <summary>
	/// Move building to world position and set color to red if not buildable
	/// </summary>
	/// <returns>Returns true if position is buildable</returns>
	public override bool PositionSelf (Int2 point) {
		var buildable = WorldManager.IsWorldTileBuildable (point, buildingData.size);
		if (buildable) {
			spriteRender.color = Color.white;
		}
		else {
			spriteRender.color = Color.red;
		}

		var pos = GetWorldPosition (point);
		pos.y += buildingData.size.height * (SURROUNDING_GAP + BUILDING_GAP) + Tile.BOTTOM_GAP;
		SetSortingOrder ();
		transform.position = pos;
		return buildable;
	}

	/// <summary>
	/// If in buildable position place building and set building data to world tile.
	/// </summary>
	/// <param name="point">Tile position to be put down on</param>
	/// <param name="overrideControl">Should override the control of placeable tile</param>
	/// <returns>Returns true if building is put down successfully</returns>
	public bool PutDownSelf (Int2 point, bool overrideControl = false) {
		if (!PositionSelf (point) && !overrideControl)
			return false;

		placedPoint = point;
		WorldManager.SetWorldTileData (point, buildingData.buildingIndex, buildingData.size, overrideControl);
		spriteRender.color = Color.white;
		buildingData.BuildingBuilt ();
		buildingPlaced = true;
		return true;
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