using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenuManager : MonoBehaviour {

	public BuildingSets buildings;

	public GameObject prefabBuildingMenuItem;

	void Awake () {
		foreach (var item in buildings.buildingSets) {
			var g = Instantiate (prefabBuildingMenuItem) as GameObject;
			g.transform.SetParent (transform);
			g.GetComponent<BuildingMenuItem> ().SetDisplay(item);
		}
	}
}
