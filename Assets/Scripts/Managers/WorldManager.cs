using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public BuildingSets buildingDatas;

	private static Ground[,] groundTiles = new Ground[World.SIZE_X, World.SIZE_Y];

	void Awake () {
//		PlayerPrefs.DeleteAll();
		GenerateWorld ();
		SetLoadedBuildings ();
		GameStateManager.SetGameState (GameStates.Playing);
	}

	/// <summary>
	/// Clone world tiles and load world tile data
	/// </summary>
	private void GenerateWorld () {
		for (int x = 0; x < World.SIZE_X; x++) {
			for (int y = 0; y < World.SIZE_Y; y++) {
				var point = new Int2 (x, y);
				var tile = Instantiate (Prefab.tileDirt);
				tile.transform.SetParent (transform);
				groundTiles[point.x, point.y] = tile.GetComponent<Ground> ();
				groundTiles[point.x, point.y].PositionSelf (point);
			}
		}
	}

	/// <summary>
	/// Control all tiles for buildings on tiles
	/// </summary>
	private void SetLoadedBuildings () {
		for (int x = 0; x < World.SIZE_X; x++) {
			for (int y = 0; y < World.SIZE_Y; y++) {
				SetLoadedBuilding(new Int2 (x, y));
			}
		}
	}

	/// <summary>
	/// If building is loaded on point. Clone, set building data and put down building on point.
	/// </summary>
	private void SetLoadedBuilding (Int2 point) {
		if (groundTiles[point.x, point.y].type != TileTypes.BuildingCore)
			return;

		var buildingData = buildingDatas.GetBuildingData (PlayerPrefsHelper.GetTileDataBuildingIndex (point));
		var clone = (Instantiate (Prefab.tileBuilding) as GameObject).GetComponent<Building> ();
		clone.SetBuildingData (buildingData);
		clone.PutDownSelf (point, true);
	}

	/// <summary>
	/// Fill tile with given size with data of building type.
	/// </summary>
	public static void SetWorldTileData (Int2 point, int buildingIndex, Size buildingSize, bool overrideControl = false) {
		if (!IsWorldTileBuildable (point, buildingSize) && !overrideControl)
			return;

		groundTiles[point.x, point.y].type = TileTypes.BuildingCore;
		PlayerPrefsHelper.SaveTileData (point, TileTypes.BuildingCore, buildingIndex);

		// We need to change this, if we're going to handle more than size 2 buildings
		if (buildingSize.width > 1) {
			groundTiles[point.x + 1, point.y].type = TileTypes.BuildingPart;
		}

		if (buildingSize.height > 1) {
			groundTiles[point.x, point.y + 1].type = TileTypes.BuildingPart;
			groundTiles[point.x + 1, point.y + 1].type = TileTypes.BuildingPart;
		}
	}

	/// <summary>
	/// Controls if point with given size is inside world and no building is placed on tiles
	/// </summary>
	/// <returns>Return true if tile is buildable.</returns>
	public static bool IsWorldTileBuildable (Int2 point, Size buildingSize) {
		var buildable = true;

		if(point.x < 0 || point.x >= World.SIZE_X || point.y < 0 || point.y >= World.SIZE_Y) {
			return false;
		}
		
		buildable = buildable && groundTiles[point.x, point.y].isFree;

		if (buildingSize.width > 1) {
			buildable = buildable && groundTiles[point.x + 1, point.y].isFree;
		}

		if (buildingSize.height > 1) {
			buildable = buildable && groundTiles[point.x, point.y + 1].isFree;
			buildable = buildable && groundTiles[point.x + 1, point.y + 1].isFree;
		}

		return buildable;
	}

	/// <summary>
	/// Resets values on tile
	/// </summary>
	public static void ResetWorldTileData (Int2 point, Size buildingSize) {
		groundTiles[point.x, point.y].type = TileTypes.Ground;
		PlayerPrefsHelper.DeleteTileData (point);

		if (buildingSize.width > 1) {
			groundTiles[point.x + 1, point.y].type = TileTypes.Ground;
		}

		if (buildingSize.height > 1) {
			groundTiles[point.x, point.y + 1].type = TileTypes.Ground;
			groundTiles[point.x + 1, point.y + 1].type = TileTypes.Ground;
		}
	}
}