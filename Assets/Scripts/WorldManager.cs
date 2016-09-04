using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public GameObject tileDirtPrefab; //TODO Change this to prefabs folder
	public GameObject tileBuildingPrefab;

	public BuildingSets buildingSets;

	private static WorldTileData[,] tileData = new WorldTileData[World.SIZE_X, World.SIZE_Y];

	void Awake () {
		GenerateWorld ();
	}

	private void GenerateWorld () {
		for (int x = 0; x < World.SIZE_X; x++) {
			for (int y = 0; y < World.SIZE_Y; y++) {
				LoadWorldTileData (x,y);
				var tile = Instantiate (tileDirtPrefab) as GameObject;
				tile.transform.SetParent (transform);
				tile.GetComponent<Tile> ().SetTile (new Int2 (x, y));
			}
		}
	}

	private static void LoadWorldTileData (int x, int y) {
		tileData[x, y] = new WorldTileData ();
	}

	public static void SetWorldTileData (Int2 pos, Size buildingSize) {
		if (!IsWorldTileBuildable (pos, buildingSize))
			return;

		tileData[pos.x, pos.y].type = TileTypes.BuildingCore;

		// We need to change this, if we're going to handle more than size 2 buildings
		if (buildingSize.width > 1) {
			tileData[pos.x + 1, pos.y].type = TileTypes.BuildingPart;
		}

		if (buildingSize.height > 1) {
			tileData[pos.x, pos.y + 1].type = TileTypes.BuildingPart;
			tileData[pos.x + 1, pos.y + 1].type = TileTypes.BuildingPart;
		}
	}

	public static bool IsWorldTileBuildable (Int2 pos, Size buildingSize) {
		var buildable = true;
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
}

public class WorldTileData {
	
	public TileTypes type { get; set; }

	public bool isFree { get { return type == TileTypes.Free; } }

	public WorldTileData (TileTypes type = TileTypes.Free) { //TODO We'll be sending point and load tile data
		this.type = type;
	}
}