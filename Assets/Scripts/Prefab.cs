using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefab {

	public const string Menu_Building = "Prefab_Menu_Building";
	public const string Tile_Building = "Prefab_Tile_Building";
	public const string Tile_Dirt = "Prefab_Tile_Dirt";

	public const string Popup_BuildingDetail = "Popup_BuildingDetail";

	public static GameObject tileBuilding {
		get {
			return Resources.Load(Prefab.Tile_Building) as GameObject;
		}
	} 
}
