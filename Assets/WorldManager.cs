using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public GameObject tileDirtPrefab; //TODO Change this to prefabs folder
	public GameObject tileBuildingPrefab;

	public BuildingSets buildingSets;

	void Start () {
		GenerateWorld (); //TODO Move this to a event
	}

	private void GenerateWorld () {
		for (int x = 0; x < World.SIZE_X; x++) {
			for (int y = 0; y < World.SIZE_Y; y++) {
				var tile = Instantiate (tileDirtPrefab) as GameObject;
				tile.transform.SetParent (transform);
				tile.GetComponent<Tile> ().SetTile (new Int2 (x, y));
			}
		}

		var b = Instantiate (tileBuildingPrefab);
		b.GetComponent<Building> ().SetBuilding (buildingSets.GetBuildingSet ());

		TouchController.SetBuilding(b.GetComponent<Building> ());
	}
}
