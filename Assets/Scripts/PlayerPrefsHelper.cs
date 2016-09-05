using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsHelper{

	private const string TILE_DATA_PREFIX = "TileData_";
	private const string COORD_SEPERATOR = "x";
	private const string BUILDING_INDEX_AFFIX = "_BuildingIndex";

	public static TileTypes ReadTileData (Int2 point) {
		return (TileTypes) PlayerPrefs.GetInt (TILE_DATA_PREFIX + point.x + COORD_SEPERATOR + point.y, (int) TileTypes.Free);
	}

	public static void SaveTileData (Int2 point, TileTypes type, int buildingIndex) {
		Debug.Log ("SaveTileData : " + point + " index: " + buildingIndex);
		PlayerPrefs.SetInt (TILE_DATA_PREFIX + point.x + COORD_SEPERATOR + point.y, (int) type);
		SetTileDataBuildingIndex (point, buildingIndex);
		PlayerPrefs.Save ();
	}

	public static int GetTileDataBuildingIndex (Int2 point) {
		var index = PlayerPrefs.GetInt (TILE_DATA_PREFIX + point.x + COORD_SEPERATOR + point.y + BUILDING_INDEX_AFFIX, -1);
		Debug.Log ("GetTileDataBuildingIndex : " + point + " index: " + index);
		return index;
	}

	public static void DeleteTileData (Int2 point) {
		PlayerPrefs.DeleteKey (TILE_DATA_PREFIX + point.x + COORD_SEPERATOR + point.y);
		PlayerPrefs.DeleteKey (TILE_DATA_PREFIX + point.x + COORD_SEPERATOR + point.y + BUILDING_INDEX_AFFIX);
		PlayerPrefs.Save ();
	}

	private static void SetTileDataBuildingIndex (Int2 point, int buildingIndex) {
		PlayerPrefs.SetInt (TILE_DATA_PREFIX + point.x + COORD_SEPERATOR + point.y + BUILDING_INDEX_AFFIX, buildingIndex);
	}
}
