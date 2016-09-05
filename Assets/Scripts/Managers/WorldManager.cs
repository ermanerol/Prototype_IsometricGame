using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public BuildingSets buildingDatas;

	private static WorldTileData[,] tileData = new WorldTileData[World.SIZE_X, World.SIZE_Y];

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
				var tile = Instantiate (Resources.Load (Prefab.Tile_Dirt)) as GameObject;
				tile.transform.SetParent (transform);
				tile.GetComponent<Tile> ().SetTile (point);
				tileData[point.x, point.y] = new WorldTileData (point);
			}
		}
	}

	/// <summary>
	/// Control all tiles for buildings on tiles
	/// </summary>
	private void SetLoadedBuildings () {
		for (int x = 0; x < World.SIZE_X; x++) {
			for (int y = 0; y < World.SIZE_Y; y++) {
				SetBuilding(new Int2 (x, y));
			}
		}
	}

	/// <summary>
	/// If building is loaded on point. Clone, set building data and put down building on point.
	/// </summary>
	private void SetBuilding (Int2 point) {
		if (tileData[point.x, point.y].type != TileTypes.BuildingCore)
			return;

		var building = buildingDatas.GetBuildingData (tileData[point.x, point.y].buildingIndex);
		var clone = (Instantiate (Prefab.tileBuilding) as GameObject).GetComponent<Building> ();
		clone.SetBuilding (building);
		clone.PutDownSelfWithoutControl (point);
	}

	/// <summary>
	/// Fill tile with given size with data of building type.
	/// </summary>
	public static void SetWorldTileData (Int2 point, int buildingIndex, Size buildingSize, bool overrideControl = false) {
		if (!IsWorldTileBuildable (point, buildingSize) && !overrideControl)
			return;

		tileData[point.x, point.y].type = TileTypes.BuildingCore;
		PlayerPrefsHelper.SaveTileData (point, TileTypes.BuildingCore, buildingIndex);

		// We need to change this, if we're going to handle more than size 2 buildings
		if (buildingSize.width > 1) {
			tileData[point.x + 1, point.y].type = TileTypes.BuildingPart;
		}

		if (buildingSize.height > 1) {
			tileData[point.x, point.y + 1].type = TileTypes.BuildingPart;
			tileData[point.x + 1, point.y + 1].type = TileTypes.BuildingPart;
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
		
		buildable = buildable && tileData[point.x, point.y].isFree;

		if (buildingSize.width > 1) {
			buildable = buildable && tileData[point.x + 1, point.y].isFree;
		}

		if (buildingSize.height > 1) {
			buildable = buildable && tileData[point.x, point.y + 1].isFree;
			buildable = buildable && tileData[point.x + 1, point.y + 1].isFree;
		}

		return buildable;
	}

	/// <summary>
	/// Resets values on tile
	/// </summary>
	public static void ResetWorldTileData (Int2 point, Size buildingSize) {
		tileData[point.x, point.y].type = TileTypes.Free;
		PlayerPrefsHelper.DeleteTileData (point);

		if (buildingSize.width > 1) {
			tileData[point.x + 1, point.y].type = TileTypes.Free;
		}

		if (buildingSize.height > 1) {
			tileData[point.x, point.y + 1].type = TileTypes.Free;
			tileData[point.x + 1, point.y + 1].type = TileTypes.Free;
		}
	}
}

/// <summary>
/// Holds data of tile. ie. if tile is free or building is placed on tile
/// </summary>
public class WorldTileData {
	
	public TileTypes type { get; set; }

	public bool isFree { get { return type == TileTypes.Free; } }

	public int buildingIndex { get; private set; }

	public WorldTileData (Int2 point) {
		type = PlayerPrefsHelper.ReadTileData (point);

		if (type != TileTypes.BuildingCore)
			return;

		buildingIndex = PlayerPrefsHelper.GetTileDataBuildingIndex (point);
	}
}