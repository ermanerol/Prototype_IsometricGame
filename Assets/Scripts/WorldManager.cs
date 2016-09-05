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

	private void SetLoadedBuildings () {
		for (int x = 0; x < World.SIZE_X; x++) {
			for (int y = 0; y < World.SIZE_Y; y++) {
				SetBuilding(new Int2 (x, y));
			}
		}
	}

	private void SetBuilding (Int2 point) {
		if (tileData[point.x, point.y].type != TileTypes.BuildingCore)
			return;

		var building = buildingDatas.GetBuildingData (tileData[point.x, point.y].buildingIndex);
		var clone = (Instantiate (Prefab.tileBuilding) as GameObject).GetComponent<Building> ();
		clone.SetBuilding (building);
		clone.PutDownSelfWithoutControl (point);
	}

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

	public static bool IsWorldTileBuildable (Int2 pos, Size buildingSize) {
		var buildable = true;

		if(pos.x < 0 || pos.x >= World.SIZE_X || pos.y < 0 || pos.y >= World.SIZE_Y) {
			return false;
		}
		
		buildable = buildable && tileData[pos.x, pos.y].isFree;

		if (buildingSize.width > 1) {
			buildable = buildable && tileData[pos.x + 1, pos.y].isFree;
		}

		if (buildingSize.height > 1) {
			buildable = buildable && tileData[pos.x, pos.y + 1].isFree;
			buildable = buildable && tileData[pos.x + 1, pos.y + 1].isFree;
		}

		return buildable;
	}

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