using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public GameObject tileDirtPrefab; //TODO Change this to prefabs folder
	public GameObject tileBuildingPrefab;

	public BuildingSets buildingSets;

	void Awake () {
		GenerateWorld ();
	}

	private void GenerateWorld () {
		for (int x = 0; x < World.SIZE_X; x++) {
			for (int y = 0; y < World.SIZE_Y; y++) {
				var tile = Instantiate (tileDirtPrefab) as GameObject;
				tile.transform.SetParent (transform);
				tile.GetComponent<Tile> ().SetTile (new Int2 (x, y));
			}
		}
	}
}
